namespace CommentsPlus.CommentClassifier
{
    #region Usings

    using System.ComponentModel.Composition;
    using System.Windows.Media;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Utilities;

    #endregion Usings

    //The quick brown fox jumps over the lazy dog
    //! Important note
    //? What's all this?
    //TODO: Work remaining
    //ToDo@MH: Work remaining
    //x object q = dt(42); //What is your question?
    //// double pi = Math.PI;
    //!? Wait, what‽

    //# Bold Test
    //¤ Removed
    //WTF I don't even?
    //‽ Not in the slightest
    //HACK Hackety hack ack!
    /////////////////////////////////////////
    ////string commentedOut = OldMethod(a++); /* an old style comment */

    /*? hallo for en kommentar!? */
    /*! A long comment - will it get bold!?
     * Should this be bold as well?
     * Another line
   */

    internal enum Classification
    {
        None,
        Important,
        Question,
        Wtf,
        Removed,
        Task
    }

    /*!? Normal comment - should be italics '*/

    public static class ClassificationDefinitions
    {
        #region Internal Fields

        [Export(typeof (ClassificationTypeDefinition))] [BaseDefinition("Comment")] [Name(Constants.ImportantComment)] internal static ClassificationTypeDefinition ImportantCommentClassificationType = null;

        [Export(typeof (ClassificationTypeDefinition))] [BaseDefinition("Comment")] [Name(Constants.QuestionComment)] internal static ClassificationTypeDefinition QuestionCommentClassificationType = null;

        [Export(typeof (ClassificationTypeDefinition))] [BaseDefinition("Comment")] [Name(Constants.RemovedComment)] internal static ClassificationTypeDefinition StrikeoutCommentClassificationType = null;

        [Export(typeof (ClassificationTypeDefinition))] [BaseDefinition("Comment")] [Name(Constants.TaskComment)] internal static ClassificationTypeDefinition TaskCommentClassificationType = null;

        [Export(typeof (ClassificationTypeDefinition))] [BaseDefinition("Comment")] [Name(Constants.WtfComment)] internal static ClassificationTypeDefinition WtfCommentClassificationType = null;

        #endregion Internal Fields

        #region HTML

        [Export(typeof (ClassificationTypeDefinition))] [BaseDefinition("HTML Comment")] [Name(Constants.ImportantHtmlComment)] internal static ClassificationTypeDefinition
            ImportantHtmlCommentClassificationType = null;

        [Export(typeof (ClassificationTypeDefinition))] [BaseDefinition("HTML Comment")] [Name(Constants.QuestionHtmlComment)] internal static ClassificationTypeDefinition
            QuestionHtmlCommentClassificationType = null;

        [Export(typeof (ClassificationTypeDefinition))] [BaseDefinition("HTML Comment")] [Name(Constants.RemovedHtmlComment)] internal static ClassificationTypeDefinition
            StrikeoutHtmlCommentClassificationType = null;

        [Export(typeof (ClassificationTypeDefinition))] [BaseDefinition("HTML Comment")] [Name(Constants.TaskHtmlComment)] internal static ClassificationTypeDefinition TaskHtmlCommentClassificationType
            = null;

        #endregion HTML

        #region XML

        [Export(typeof (ClassificationTypeDefinition))] [BaseDefinition("XML Comment")] [Name(Constants.ImportantXmlComment)] internal static ClassificationTypeDefinition
            ImportantXmlCommentClassificationType = null;

        [Export(typeof (ClassificationTypeDefinition))] [BaseDefinition("XML Comment")] [Name(Constants.QuestionXmlComment)] internal static ClassificationTypeDefinition
            QuestionXmlCommentClassificationType = null;

        [Export(typeof (ClassificationTypeDefinition))] [BaseDefinition("XML Comment")] [Name(Constants.RemovedXmlComment)] internal static ClassificationTypeDefinition
            StrikeoutXmlCommentClassificationType = null;

        [Export(typeof (ClassificationTypeDefinition))] [BaseDefinition("Xml Comment")] [Name(Constants.TaskXmlComment)] internal static ClassificationTypeDefinition TaskXmlCommentClassificationType = null;

        #endregion XML
    }

    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.ImportantComment)]
    [Name(Constants.ImportantComment)]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    public sealed class ImportantCommentFormat : ClassificationFormatDefinition
    {
        #region Public Constructors

        public ImportantCommentFormat()
        {
            DisplayName = Constants.ImportantComment + " (//!)";
            ForegroundColor = Constants.ImportantColor;
            IsBold = true;
        }

        #endregion Public Constructors
    }

    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.QuestionComment)]
    [Name(Constants.QuestionComment)]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    public sealed class QuestionCommentFormat : ClassificationFormatDefinition
    {
        #region Public Constructors

        public QuestionCommentFormat()
        {
            DisplayName = Constants.QuestionComment + " (//?)";
            ForegroundColor = Constants.QuestionColor;
        }

        #endregion Public Constructors
    }

    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.RemovedComment)]
    [Name(Constants.RemovedComment)]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    public sealed class StrikeoutCommentFormat : ClassificationFormatDefinition
    {
        #region Public Constructors

        public StrikeoutCommentFormat()
        {
            DisplayName = Constants.RemovedComment + " (//x)";
            ForegroundColor = Constants.RemovedColor;
            TextDecorations = System.Windows.TextDecorations.Strikethrough;
        }

        #endregion Public Constructors
    }

    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.TaskComment)]
    [Name(Constants.TaskComment)]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    public sealed class TaskCommentFormat : ClassificationFormatDefinition
    {
        #region Public Constructors

        public TaskCommentFormat()
        {
            DisplayName = Constants.TaskComment + " (//TODO)";
            ForegroundColor = Constants.TaskColor;
        }

        #endregion Public Constructors
    }

    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.WtfComment)]
    [Name(Constants.WtfComment)]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    public sealed class WtfCommentFormat : ClassificationFormatDefinition
    {
        #region Public Constructors

        public WtfCommentFormat()
        {
            DisplayName = Constants.WtfComment + " (//!?)";
            ForegroundColor = Constants.WtfColor;
        }

        #endregion Public Constructors
    }

    internal static class Constants
    {
        #region Public Fields

        //! Important
        public const string ImportantComment = "Comment - Important";

        public const string ImportantHtmlComment = "HTML Comment - Important";
        public const string ImportantXmlComment = "XML Comment - Important";

        //? Question
        public const string QuestionComment = "Comment - Question";

        public const string QuestionHtmlComment = "HTML Comment - Question";
        public const string QuestionXmlComment = "XML Comment - Question";

        //x Removed
        public const string RemovedComment = "Comment - Removed";

        public const string RemovedHtmlComment = "HTML Comment - Removed";

        public const string RemovedXmlComment = "XML Comment - Removed";

        //TODO: This does not need work
        public const string TaskComment = "Comment - Task";

        public const string TaskHtmlComment = "HTML Comment - Task";

        public const string TaskXmlComment = "XML Comment - Task";

        //!? WTF
        public const string WtfComment = "Comment - WAT!?";

        public static readonly Color ImportantColor = Colors.Green;
        public static readonly Color QuestionColor = Colors.Red;
        public static readonly Color RemovedColor = Colors.Gray;
        public static readonly Color TaskColor = Colors.DarkBlue;
        public static readonly Color WtfColor = Colors.Coral;

        #endregion Public Fields
    }

    #region HTML

    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.ImportantHtmlComment)]
    [Name(Constants.ImportantHtmlComment)]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    public sealed class ImportantHtmlCommentFormat : ClassificationFormatDefinition
    {
        #region Public Constructors

        public ImportantHtmlCommentFormat()
        {
            DisplayName = Constants.ImportantHtmlComment + " (<!--!)";
            ForegroundColor = Constants.ImportantColor;
            IsBold = true;
        }

        #endregion Public Constructors
    }

    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.QuestionHtmlComment)]
    [Name(Constants.QuestionHtmlComment)]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    public sealed class QuestionHtmlCommentFormat : ClassificationFormatDefinition
    {
        #region Public Constructors

        public QuestionHtmlCommentFormat()
        {
            DisplayName = Constants.QuestionHtmlComment + " (<!--?)";
            ForegroundColor = Constants.QuestionColor;
        }

        #endregion Public Constructors
    }

    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.RemovedHtmlComment)]
    [Name(Constants.RemovedHtmlComment)]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    public sealed class StrikeoutHtmlCommentFormat : ClassificationFormatDefinition
    {
        #region Public Constructors

        public StrikeoutHtmlCommentFormat()
        {
            DisplayName = Constants.RemovedHtmlComment + " (<!--x)";
            ForegroundColor = Constants.RemovedColor;
            TextDecorations = System.Windows.TextDecorations.Strikethrough;
        }

        #endregion Public Constructors
    }

    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.TaskHtmlComment)]
    [Name(Constants.TaskHtmlComment)]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    public sealed class TaskHtmlCommentFormat : ClassificationFormatDefinition
    {
        #region Public Constructors

        public TaskHtmlCommentFormat()
        {
            DisplayName = Constants.TaskHtmlComment + " (<!--TODO)";
            ForegroundColor = Constants.TaskColor;
        }

        #endregion Public Constructors
    }

    #endregion HTML

    #region XML

    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.ImportantXmlComment)]
    [Name(Constants.ImportantXmlComment)]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    public sealed class ImportantXmlCommentFormat : ClassificationFormatDefinition
    {
        #region Public Constructors

        public ImportantXmlCommentFormat()
        {
            DisplayName = Constants.ImportantXmlComment + " (<!--!)";
            ForegroundColor = Constants.ImportantColor;
            IsBold = true;
        }

        #endregion Public Constructors
    }

    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.QuestionXmlComment)]
    [Name(Constants.QuestionXmlComment)]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    public sealed class QuestionXmlCommentFormat : ClassificationFormatDefinition
    {
        #region Public Constructors

        public QuestionXmlCommentFormat()
        {
            DisplayName = Constants.QuestionXmlComment + " (<!--?)";
            ForegroundColor = Constants.QuestionColor;
        }

        #endregion Public Constructors
    }

    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.RemovedXmlComment)]
    [Name(Constants.RemovedXmlComment)]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    public sealed class StrikeoutXmlCommentFormat : ClassificationFormatDefinition
    {
        #region Public Constructors

        public StrikeoutXmlCommentFormat()
        {
            DisplayName = Constants.RemovedXmlComment + " (<!--x)";
            ForegroundColor = Constants.RemovedColor;
            TextDecorations = System.Windows.TextDecorations.Strikethrough;
        }

        #endregion Public Constructors
    }

    [Export(typeof (EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.TaskXmlComment)]
    [Name(Constants.TaskXmlComment)]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    public sealed class TaskXmlCommentFormat : ClassificationFormatDefinition
    {
        #region Public Constructors

        public TaskXmlCommentFormat()
        {
            DisplayName = Constants.TaskXmlComment + " (<!--TODO)";
            ForegroundColor = Constants.TaskColor;
        }

        #endregion Public Constructors
    }

    #endregion XML
}