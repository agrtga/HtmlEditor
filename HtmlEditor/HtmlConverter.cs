using System;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlEditor
{
    /// <summary>
    /// Strips the HTML to create plain text for HTML fragments created by the
    /// Telerik rich text box.
    /// </summary>
    internal static class HtmlConverter
    {
        const string TagPattern = @"<[^>]*>";

        /// <summary>
        /// Strips the HTML tags and formatting.
        /// </summary>
        /// <param name="htmlText">The HTML text to be converted.</param>
        /// <returns>A string representing the plain text from the HTML.</returns>
        public static string StripAll(string htmlText) 
            => htmlText.StripStyleTags()
                .ReplaceLineTags()
                .ReplaceConstants()
                .StripFormattingTags();

        /// <summary>
        /// Strips the style tags from the HTML fragment.
        /// </summary>
        /// <param name="htmlText">The HTML text.</param>
        /// <returns>The modified string.</returns>
        static string StripStyleTags(this string htmlText)
        {
            const string StyleOpenTag = "<style type=\"text/css\">";
            const string StyleCloseTag = "</style>";

            int startStyle = htmlText.IndexOf(StyleOpenTag);
            int endStyle;

            if (startStyle >= 0) {
                endStyle = htmlText.IndexOf(StyleCloseTag);

                if (startStyle > 0) {
                    string tempText = htmlText.Substring(0, startStyle - 1);
                    htmlText = tempText + htmlText.Substring(endStyle + StyleCloseTag.Length);
                }
                else {
                    htmlText = htmlText.Substring(endStyle + StyleCloseTag.Length);
                }
            }

            return htmlText;
        }

        /// <summary>
        /// Replace the line tags with a plain text representation.
        /// </summary>
        /// <param name="htmlText">The HTML text.</param>
        /// <returns>The modified string.</returns>
        static string ReplaceLineTags(this string htmlText) 
            => new StringBuilder(htmlText)
                .Replace("<br>", Environment.NewLine)
                .Replace(@"</p>", Environment.NewLine + Environment.NewLine)
                .ToString();

        /// <summary>
        /// Replace HTML constant values.
        /// </summary>
        /// <param name="htmlText">The HTML text.</param>
        /// <returns>The modified string.</returns>
        static string ReplaceConstants(this string htmlText)
            => new StringBuilder(htmlText)
                .Replace("&nbsp;", "")
                .Replace("&lt;", "<")
                .Replace("&gt;", ">")
                .ToString();

        /// <summary>
        /// Strip HTML formatting tags.
        /// </summary>
        /// <param name="htmlText">The HTML text.</param>
        /// <returns>The modified string.</returns>
        static string StripFormattingTags(this string htmlText) 
            => Regex.Replace(htmlText, TagPattern, string.Empty)
                .Trim(' ', '\r', '\n');
    }
}
