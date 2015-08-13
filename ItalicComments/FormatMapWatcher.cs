namespace CommentsPlus.ItalicComments
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Formatting;

    #endregion Usings

    internal sealed class FormatMapWatcher : IDisposable
    {
        #region Private Fields

        private static readonly List<string> CommentTypes = new List<string>
                                                            {
                                                                "comment",
                                                                "xml doc comment",
                                                                "vb xml doc comment",
                                                                "xml comment",
                                                                "html comment",
                                                                "xaml comment"
                                                            };

        private static readonly List<string> DocTagTypes = new List<string>
                                                           {
                                                               "xml doc tag",
                                                               "vb xml doc tag",
                                                               "xml doc attribute"
                                                           };

        private IClassificationFormatMap _formatMap;
        private bool _inUpdate;
        private IClassificationTypeRegistryService _typeRegistry;
        private ITextView _view;

        #endregion Private Fields

        #region Public Constructors

        public FormatMapWatcher(ITextView view,
                                IClassificationFormatMap formatMap,
                                IClassificationTypeRegistryService typeRegistry)
        {
            _view = view;
            _formatMap = formatMap;
            _typeRegistry = typeRegistry;

            FixComments();

            //_formatMap.ClassificationFormatMappingChanged += FormatMapChanged;

            view.GotAggregateFocus += FirstGotFocus;
            view.Closed += view_Closed;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
            if (_view != null)
            {
                _view.Closed -= view_Closed;
                _view.GotAggregateFocus -= FirstGotFocus;
                _view = null;
            }

            if (_formatMap != null)
            {
                _formatMap.ClassificationFormatMappingChanged -= FormatMapChanged;
                _formatMap = null;
            }
            _typeRegistry = null;
        }

        #endregion Public Methods

        #region Private Methods

        private void FirstGotFocus(object sender, EventArgs e)
        {
            var view = sender as ITextView;
            if (view != null)
                view.GotAggregateFocus -= FirstGotFocus;

            Debug.Assert(!_inUpdate, "How can we be updating *while* the view is getting focus?");

            FixComments();
        }

        private void FixComments()
        {
            if (_inUpdate || _formatMap == null || (_view != null && _view.IsClosed))
                return;

            var batch = false;
            try
            {
                _inUpdate = true;

                if (!_formatMap.IsInBatchUpdate)
                {
                    _formatMap.BeginBatchUpdate();
                    batch = true;
                }

                // First, go through the ones we know about:

                // 1) Known comment types are italicized
                foreach (
                    IClassificationType type in
                        CommentTypes.Select(t => _typeRegistry.GetClassificationType(t)).Where(t => t != null))
                {
                    Italicize(type);
                }

                // 2) Known doc tags
                foreach (
                    IClassificationType type in
                        DocTagTypes.Select(t => _typeRegistry.GetClassificationType(t)).Where(t => t != null))
                {
                    Italicize(type);
                }

                // 3) Grab everything else that looks like a comment or doc tag
                foreach (IClassificationType classification in _formatMap.CurrentPriorityOrder.Where(c => c != null))
                {
                    var name = classification.Classification;
                    StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                    if (CommentTypes.Contains(name, comparer) || DocTagTypes.Contains(name, comparer))
                        continue;

                    if (name.IndexOf("comment", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        name.IndexOf("doc tag", StringComparison.OrdinalIgnoreCase) >= 0)
                        Italicize(classification);
                }
            }
            finally
            {
                if (batch && _formatMap.IsInBatchUpdate)
                    _formatMap.EndBatchUpdate();
                _inUpdate = false;
            }
        }

        private void FormatMapChanged(object sender, EventArgs e)
        {
            FixComments();
        }

        /// <summary>
        /// Set italics for classification.
        /// </summary>
        /// <param name="classification">The classification to process.</param>
        private void Italicize(IClassificationType classification)
        {
            /* Get the classification text properties */
            TextFormattingRunProperties properties = _formatMap.GetTextProperties(classification);

            //If italics has already been determined, skip it
            if (!properties.ItalicEmpty)
                return;

            //Add italics, new font
            properties = properties.SetTypeface(new Typeface(new FontFamily("Lucida Sans"),
                                                             FontStyles.Italic,
                                                             FontWeights.Normal,
                                                             new FontStretch()));
            properties = properties.SetFontRenderingEmSize(properties.FontRenderingEmSize - 1.0);

            // And put it back in the format map
            _formatMap.SetTextProperties(classification, properties);
        }

        private void view_Closed(object sender, EventArgs e)
        {
            var view = sender as ITextView;
            if (view != null)
                view.Closed -= view_Closed;

            Dispose();
        }

        #endregion Private Methods
    }
}