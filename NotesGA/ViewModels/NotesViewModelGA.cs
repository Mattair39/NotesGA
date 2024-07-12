using CommunityToolkit.Mvvm.Input;
using NotesGA.Models;
using NotesGA.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace NotesGA.ViewModels;

internal class NotesViewModelGA : IQueryAttributable
{
    public ObservableCollection<ViewModels.NoteViewModelGA> AllNotes { get; }
    public ICommand NewCommandGA { get; }
    public ICommand SelectNoteCommandGA { get; }

    public NotesViewModelGA()
    {
        AllNotes = new ObservableCollection<ViewModels.NoteViewModelGA>(Models.NoteGA.LoadAll().Select(n => new NoteViewModelGA(n)));
        NewCommandGA = new AsyncRelayCommand(NewNoteAsync);
        SelectNoteCommandGA = new AsyncRelayCommand<ViewModels.NoteViewModelGA>(SelectNoteAsync);
    }

    private async Task NewNoteAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.NotePageGA));
    }

    private async Task SelectNoteAsync(ViewModels.NoteViewModelGA note)
    {
        if (note != null)
            await Shell.Current.GoToAsync($"{nameof(Views.NotePageGA)}?load={note.Identifier}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            string noteId = query["deleted"].ToString();
            NoteViewModelGA matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            // If note exists, delete it
            if (matchedNote != null)
                AllNotes.Remove(matchedNote);
        }
        else if (query.ContainsKey("saved"))
        {
            string noteId = query["saved"].ToString();
            NoteViewModelGA matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            // If note is found, update it
            if (matchedNote != null)
            {
                matchedNote.Reload();
                AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
            }
            // If note isn't found, it's new; add it.
            else
                AllNotes.Insert(0, new NoteViewModelGA(Models.NoteGA.Load(noteId)));
        }
    }
}
