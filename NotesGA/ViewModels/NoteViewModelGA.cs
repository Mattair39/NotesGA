using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace NotesGA.ViewModels;

internal class NoteViewModelGA : ObservableObject, IQueryAttributable
{
    private Models.NoteGA _note;

    public string Text
    {
        get => _note.Text;
        set
        {
            if (_note.Text != value)
            {
                _note.Text = value;
                OnPropertyChanged();
            }
        }
    }

    public DateTime Date => _note.Date;

    public string Identifier => _note.Filename;

    public ICommand SaveCommandGA { get; private set; }
    public ICommand DeleteCommandGA { get; private set; }

    public NoteViewModelGA()
    {
        _note = new Models.NoteGA();
        SaveCommandGA = new AsyncRelayCommand(Save);
        DeleteCommandGA = new AsyncRelayCommand(Delete);
    }

    public NoteViewModelGA(Models.NoteGA note)
    {
        _note = note;
        SaveCommandGA = new AsyncRelayCommand(Save);
        DeleteCommandGA = new AsyncRelayCommand(Delete);
    }

    private async Task Save()
    {
        _note.Date = DateTime.Now;
        _note.Save();
        await Shell.Current.GoToAsync($"..?saved={_note.Filename}");
    }

    private async Task Delete()
    {
        _note.Delete();
        await Shell.Current.GoToAsync($"..?deleted={_note.Filename}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            _note = Models.NoteGA.Load(query["load"].ToString());
            RefreshProperties();
        }
    }

    public void Reload()
    {
        _note = Models.NoteGA.Load(_note.Filename);
        RefreshProperties();
    }

    private void RefreshProperties()
    {
        OnPropertyChanged(nameof(Text));
        OnPropertyChanged(nameof(Date));
    }
}