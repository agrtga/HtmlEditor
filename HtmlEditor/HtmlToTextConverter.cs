using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace HtmlEditor
{
    /// <summary>
    /// Converts HTML text to plain text for display in WPF controls.
    /// </summary>
    /// <remarks>This converter is designed for limited use by the HTML editor in the
    /// sample application. It's not designed to be optimized nor is it designed to be safe
    /// to use to remove scripting or other potentially malicious markup.
    /// This sample design supports HTML fragments.</remarks>
    [ValueConversion(typeof(string), typeof(string))]
    internal sealed class HtmlToTextConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// Converts HTML text fragments to plain text.
        /// </summary>
        /// <param name="value">A value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use. No parameters are currently implemented.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A string value representing the plain text from an HTML fragment.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string htmlText && !string.IsNullOrEmpty(htmlText)) {
                return HtmlConverter.StripAll(htmlText);
            }

            return null;
        }

        /// <summary>
        /// Converts plain text into an HTML fragment.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A value indicating that no conversion action will be taken.</returns>
        /// <remarks>Converting back to an HTML format isn't supported.</remarks>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => Binding.DoNothing;

        /// <summary>
        /// Returns an object that is provided as the value of the target property for the
        /// markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>The object value to set on the property where the extension is applied.</returns>
        /// <remarks>This method of the MarkupExtension base class allows markup to indicate the converter directly
        /// instead of requiring explicit definition in resources.</remarks>
        public override object ProvideValue(IServiceProvider serviceProvider)
            => this;
    }
}
