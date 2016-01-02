using Autofac.Builder;
using NukedBit.Mvvm.ViewModels;

namespace NukedBit.Mvvm.DI.AutoFac
{
    public static class AutoFacExtensions
    {
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> AsView<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> dep)
            where TLimit: IViewModel
        {
            return dep.Named<IViewModel>(typeof (TLimit).Name)
                .As<IViewModel>();
        }
    }
}
