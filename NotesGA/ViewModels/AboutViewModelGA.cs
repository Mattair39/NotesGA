using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace NotesGA.ViewModels;

internal class AboutViewModelGA
{
    public string Title => "Mateo Arguello";
    public string Version => AppInfo.VersionString;
    public string MoreInfoUrl => "https://aka.ms/maui";
    public string Message => "Soy estudiante de la carrera de Ingeniería de Software y actualmente me encuentro cursando 5to semestre en la Universidad de las Américas. Entre mis aficiones se encuentran el entrenar pádel, jugar ajedrez, el estudio matemático y la lectura. \t ";
    public ICommand ShowMoreInfoCommandGA { get; }

    public AboutViewModelGA()
    {
        ShowMoreInfoCommandGA = new AsyncRelayCommand(ShowMoreInfo);
    }

    async Task ShowMoreInfo() =>
        await Launcher.Default.OpenAsync(MoreInfoUrl);
}