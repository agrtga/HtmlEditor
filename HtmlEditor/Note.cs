using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HtmlEditor
{
    /// <summary>
    /// Represents a sample note.
    /// </summary>
    public sealed class Note : INotifyPropertyChanged
    {
        string text;

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="text">The initial text for the note.</param>
        public Note(string text)
        {
            Created = DateTime.Now;
            Text = text;
        }

        /// <summary>
        /// Gets the date/time the note was created.
        /// </summary>
        /// <value>A <see cref="DateTime"/> value.</value>
        public DateTime Created { get; }

        /// <summary>
        /// Gets or sets the text for the note.
        /// </summary>
        /// <value>A string value. HTML is expected, but not required.</value>
        public string Text
        {
            get => text;
            set => SetAndNotify(ref text, value);
        }

        /// <summary>
        /// Occurs when a property value is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string containing a time stamp and a shortened version
        /// of the note for display purposes.</returns>
        public override string ToString()
        {
            string preview = HtmlConverter.StripAll(Text)
                .Replace(Environment.NewLine, " ")
                .Substring(0, 20);

            return $"{Created.ToShortTimeString()} {preview}";
        }

        /// <summary>
        /// Sets the field value and raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <param name="field">The field passed by reference.</param>
        /// <param name="value">The new value.</param>
        /// <param name="propertyName">The name of the property changed.</param>
        void SetAndNotify<T>(ref T field, T value, [CallerMemberName]string propertyName = null)
        {
            field = value;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property changed.</param>
        void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
