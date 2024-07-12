namespace NotesGA
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NotesGA.Views.NotePageGA), typeof(NotesGA.Views.NotePageGA));
        }

    }
}
