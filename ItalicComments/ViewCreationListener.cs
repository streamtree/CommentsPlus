namespace CommentsPlus.ItalicComments
{
    using System;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Utilities;
    using Microsoft.Win32;

    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("code")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal sealed class ViewCreationListener : IWpfTextViewCreationListener
    {
        [Import]
        IClassificationFormatMapService _formatMapService;

        [Import]
        IClassificationTypeRegistryService _typeRegistry;

        static readonly bool IsEnabled;

        static ViewCreationListener()
        {
            IsEnabled = IsExtensionEnabled();
        }

        /// <summary>
        /// When a text view is created, make all comments italicized.
        /// </summary>
        /// <param name="textView">The view to handle.</param>
        public void TextViewCreated(IWpfTextView textView)
        {
            if (IsEnabled)
                textView.Properties.GetOrCreateSingletonProperty(() => new FormatMapWatcher(textView, _formatMapService.GetClassificationFormatMap(textView), _typeRegistry));
        }

        static bool IsExtensionEnabled()
        {
            var res = true;

            try
            {
                using (var subKey = Registry.CurrentUser.OpenSubKey("Software\\CommentsPlus", false))
                {
                    if (subKey != null)
                    {
                        var value = Convert.ToInt32(subKey.GetValue("EnableItalics", 1));
                        res = value != 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed to read registry: " + ex.Message, "CommentsPlus");
            }

            return res;
        }
    }
}