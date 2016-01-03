using Autofac.Builder;
using NukedBit.Mvvm.Views;
using Xamarin.Forms;

namespace NukedBit.Mvvm.DI.AutoFac
{
    public static class AutoFacExtensions
    {
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> AsView<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> dep)
            where TLimit: ContentPage
        {
            return dep.Named<ContentPage>(typeof (TLimit).Name);
        }
    }
}
