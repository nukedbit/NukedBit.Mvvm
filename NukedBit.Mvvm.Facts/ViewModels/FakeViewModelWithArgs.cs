using NukedBit.Mvvm.ViewModels;

namespace NukedBit.Mvvm.Facts.ViewModels
{
    public class FakeViewModelWithArgs : ViewModelBase
    {
        public string Name { get; }

        public FakeViewModelWithArgs(string name)
        {
            Name = name;
        }
    }
}