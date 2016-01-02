using NukedBit.Mvvm.ViewModels;

namespace NukedBit.Mvvm.Views
{
    public interface IView
    {
        IViewModel ViewModel { get; }
    }
}