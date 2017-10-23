using System;
using Telerik.Windows.Controls;

namespace HtmlEditor
{
    /// <summary>
    /// The HTML editor window.
    /// </summary>
    public partial class EditorWindow : RadRibbonWindow
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public EditorWindow() 
            => InitializeComponent();

        /// <summary>
        /// Gets or sets the text in the editor.
        /// </summary>
        /// <value>A string value expected to be in HTML format. HTML
        /// fragments are supported.</value>
        public string HtmlText
        {
            get => HtmlProvider.Html;
            set => HtmlProvider.Html = value;
        }

        void OK_Click(object sender, EventArgs e)
            => Close(true);

        void Cancel_Click(object sender, EventArgs e)
            => Close(false);

        /// <summary>
        /// Close the window.
        /// </summary>
        /// <param name="result"><b>true</b> if the user clicked OK; otherwise, <b>false</b>.</param>
        void Close(bool result)
        {
            DialogResult = result;
            Close();
        }
    }
}
