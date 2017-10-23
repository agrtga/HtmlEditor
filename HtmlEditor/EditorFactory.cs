using System;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Telerik.Windows.Controls.RichTextBoxUI.Dialogs;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.FormatProviders.Html;
using Telerik.Windows.Documents.UI.Extensibility;
using Telerik.Windows.Documents.UI.Layers;

namespace HtmlEditor
{
    /// <summary>
    /// Creates an optimized editor for editing HTML text.
    /// </summary>
    public static class EditorFactory
    {
        static bool initialized;
        static object syncObj = new object();

        /// <summary>
        /// Creates a default HTML editor.
        /// </summary>
        /// <param name="owner">The owner window for the HTML editor.</param>
        /// <param name="htmlText">The initial HTML text to show.</param>
        /// <returns>A <see cref="EditorWindow"/> object.</returns>
        public static EditorWindow CreateEditor(Window owner, string htmlText = null)
        {
            if (!initialized) {
                InitializeForRichTextBox();
            }

            var editor = new EditorWindow() {
                Owner = owner,
                HtmlText = htmlText
            };

            editor.EditorTextBox.UILayersBuilder = new UILayersBuilder();  // loads the UI layers without using MEF

            return editor;
        }

        /// <summary>
        /// Performs initialization operations to optimize the use of the Telerik RichTextBox
        /// for use with editing HTML.
        /// </summary>
        static void InitializeForRichTextBox()
        {
            lock (syncObj) {
                if (!initialized) {
                    CreateTypeCatalog();
                    RegisterFormatProviders();

                    initialized = true;
                }
            }
        }

        /// <summary>
        /// Creates the type catalog used by the RadRichTextBox.
        /// </summary>
        /// <remarks>Tells MEF what types to load instead of having it search for new ones.</remarks>
        static void CreateTypeCatalog()
        {
            var types = new Type[] {
                typeof(HtmlFormatProvider),
                typeof(FindReplaceDialog),
                typeof(FontPropertiesDialog),
                typeof(InsertDateTimeDialog),
                typeof(InsertTableDialog),
                typeof(RadInsertHyperlinkDialog),
                typeof(RadInsertSymbolDialog),
                typeof(StyleFormattingPropertiesDialog),
                typeof(TableBordersDialog),
                typeof(TablePropertiesDialog)
            };

            RadCompositionInitializer.Catalog = new TypeCatalog(types);
        }

        /// <summary>
        /// Register format providers for the RadRichTextBox.
        /// </summary>
        /// <remarks>Manually register the format providers instead of relying on MEF.</remarks>
        static void RegisterFormatProviders()
        {
            var htmlProvider = CreateHtmlProvider();

            DocumentFormatProvidersManager.AutomaticallyLoadFormatProviders = false;
            DocumentFormatProvidersManager.RegisterFormatProvider(htmlProvider);
        }

        /// <summary>
        /// Creates an optmized HTML format provider.
        /// </summary>
        /// <returns>An object that implements <see cref="IDocumentFormatProvider"/>.</returns>
        static IDocumentFormatProvider CreateHtmlProvider()
        {
            var settings = new HtmlExportSettings() {
                DocumentExportLevel = DocumentExportLevel.Fragment,
                ExportEmptyDocumentAsEmptyString = true
            };

            settings.PropertiesToIgnore["p"].Add("margin-top");
            settings.PropertiesToIgnore["p"].Add("margin-bottom");
            settings.PropertiesToIgnore["p"].Add("margin-left");
            settings.PropertiesToIgnore["p"].Add("margin-right");
            settings.PropertiesToIgnore["p"].Add("line-height");

            return new HtmlFormatProvider() {
                ExportSettings = settings
            };
        }
    }
}
