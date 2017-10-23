using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HtmlEditor
{
    /// <summary>
    /// The main window for the application.
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly ObservableCollection<Note> notes = new ObservableCollection<Note>();

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public MainWindow() 
            => InitializeComponent();

        /// <summary>
        /// Gets a collection of notes.
        /// </summary>
        /// <value>A collection of <see cref="Note"/> objects.</value>
        public ObservableCollection<Note> Notes 
            => notes;

        void AddButton_Click(object sender, RoutedEventArgs e) 
            => ShowHtmlEditor();

        void NoteListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (NoteListBox.SelectedItem is Note note) {
                ShowHtmlEditor(note);
            }
        }

        /// <summary>
        /// Show the editor.
        /// </summary>
        /// <param name="note">The existing note to be displayed, or a null
        /// reference to create a new note.</param>
        void ShowHtmlEditor(Note note = null)
        {
            var editor = EditorFactory.CreateEditor(this, note?.Text);

            if (editor.ShowDialog().GetValueOrDefault()) {
                if (note == null) {
                    note = new Note(editor.HtmlText);
                    Notes.Add(note);
                }
                else {
                    note.Text = editor.HtmlText;
                }
            }
        }
    }
}
