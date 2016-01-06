using System.Threading.Tasks;
using Autofac;
using Moq;
using NukedBit.Mvvm.DI.AutoFac.Facts.ViewModels;
using NukedBit.Mvvm.DI.AutoFac.Facts.Views;
using Xamarin.Forms;
using Xunit;

namespace NukedBit.Mvvm.DI.AutoFac.Facts
{
    public class MvvmContainerAutofacFacts
    {
        [Fact]
        public async Task ResolveNamed()
        {

            var builder = new ContainerBuilder();

            builder.RegisterType<FakeViewModel>();

            builder.RegisterType<FakeView>()
                .AsView();

            builder.Register(c =>
                ViewModelNavigation.Create(MvvmContainerAutofac.Create(c.Resolve<IComponentContext>())));

            var container = builder.Build();

            var navigationMock = new Mock<INavigation>();

            await container.Resolve<IViewModelNavigator>()
                .Navigate<FakeViewModel>(navigationMock.Object);

            navigationMock.Verify(m => m.PushAsync(It.IsAny<FakeView>()), Times.Once);
        }

        [Fact]
        public async Task ResolveNamedWithArgs()
        {

            var builder = new ContainerBuilder();

            builder.RegisterType<FakeWithArgsViewModel>();

            builder.RegisterType<FakeWithArgsView>()
                .AsView();

            builder.Register(c =>
                ViewModelNavigation.Create(MvvmContainerAutofac.Create(c.Resolve<IComponentContext>())));

            var container = builder.Build();

            var navigationMock = new Mock<INavigation>();

            await container.Resolve<IViewModelNavigator>()
                .Navigate<FakeWithArgsViewModel>(navigationMock.Object, 
                new NamedParameter("name", "pippo"));

            navigationMock.Verify(m => m.PushAsync(It.IsAny<FakeWithArgsView>()), Times.Once);
        }
    }
}
