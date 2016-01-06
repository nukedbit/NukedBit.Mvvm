using NukedBit.Mvvm.DI.AutoFac.Facts.ViewModels;
using NukedBit.Mvvm.Views;

namespace NukedBit.Mvvm.DI.AutoFac.Facts.Views
{
    public class FakeWithArgsView : ViewBase<FakeWithArgsViewModel>
    {
        public FakeWithArgsView(FakeWithArgsViewModel model)
            : base(model)
        {
            
        }
    }
}