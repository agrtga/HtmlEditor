using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls.RichTextBoxUI.Dialogs;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.FormatProviders.Html;
using Telerik.Windows.Documents.UI.Extensibility;

namespace HtmlEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            TelerikStartup();
        }

        /// <summary>
        /// Perform startup operations for Telerik components.
        /// </summary>
        void TelerikStartup()
        {
            CreateRadRichTextBoxTypeCatalog();
            LoadRichTextBoxFormatProviders();
        }

        /// <summary>
        /// Create the type catalog used for the RadRichTextBox.
        /// </summary>
        /// <remarks>This method explicitly sets the necessary types used
        /// application wide for any instances of the RadRichTextBox.</remarks>
        void CreateRadRichTextBoxTypeCatalog()
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
        /// Load the format providers for the RadRichTextBox.
        /// </summary>
        /// <remarks>Turn off MEF for format provider resolution, and manually
        /// add the format provider.</remarks>
        void LoadRichTextBoxFormatProviders()
        {
            DocumentFormatProvidersManager.AutomaticallyLoadFormatProviders = false;
            DocumentFormatProvidersManager.RegisterFormatProvider(new HtmlFormatProvider());
        }
    }
}
