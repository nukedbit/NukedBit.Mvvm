using Autofac.Builder;
using NukedBit.Mvvm.Views;

namespace NukedBit.Mvvm.DI.AutoFac
{
    public static class AutoFacExtensions
    {
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> AsView<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> dep)
            where TLimit: class
        {
            return dep.Named<IView>(typeof (TLimit).Name);
        }
    }
}
