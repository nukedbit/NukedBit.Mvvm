using NukedBit.Mvvm.ViewModels;

namespace NukedBit.Mvvm.DI.AutoFac.Facts.ViewModels
{
    public class FakeWithArgsViewModel : ViewModelBase
    {
        public string Name { get; }

        public FakeWithArgsViewModel(string name)
        {
            Name = name;
        }
    }
}