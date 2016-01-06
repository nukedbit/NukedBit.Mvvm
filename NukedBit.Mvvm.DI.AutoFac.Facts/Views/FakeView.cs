using NukedBit.Mvvm.DI.AutoFac.Facts.ViewModels;
using NukedBit.Mvvm.Views;

namespace NukedBit.Mvvm.DI.AutoFac.Facts.Views
{
    public class FakeView : ViewBase<FakeViewModel>
    {
        public FakeView(FakeViewModel model)
            : base(model)
        {
            
        }
    }
}
