namespace NotesGA.Views;

public partial class AllNotesPageGA : ContentPage
{
	public AllNotesPageGA()
	{
		InitializeComponent();
	}
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }
}