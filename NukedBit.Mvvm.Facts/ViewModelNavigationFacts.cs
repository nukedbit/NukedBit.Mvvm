using System;
using System.Threading.Tasks;
using Moq;
using NukedBit.Mvvm.Facts.ViewModels;
using Xamarin.Forms;
using Xunit;
using System.Linq;
using static Xunit.Assert;

namespace NukedBit.Mvvm.Facts
{
    public class ViewModelNavigationFacts
    {
        [Fact]
        public async Task ResolveNamed()
        {
            var containerMock = new Mock<IMvvmContainer>();
            var viewName = "FakeView";
            var view = new ContentPage();
            containerMock.Setup(m => m.ResolveNamed(viewName, It.IsAny<TypedParameter>()))
                .Returns(view);
            var fakeViewModel = new FakeViewModel();
            containerMock.Setup(m => m.Resolve<FakeViewModel>())
                .Returns(fakeViewModel);
            var navigationMock = new Mock<INavigation>();

            var impl = new ViewModelNavigationImpl(containerMock.Object, true);
            navigationMock.Setup(m => m.PushAsync(view)).Returns(Task.FromResult(0));

            await impl.Navigate<FakeViewModel>(navigationMock.Object);

            navigationMock.Verify(m => m.PushAsync(view), Times.Once);
            containerMock.Verify(m => m.ResolveNamed(viewName, It.Is<IParameter[]>(parameters =>
                parameters.Count(p => ((TypedParameter)p).Value == fakeViewModel) == 1)), Times.Once);
        }

        [Fact]
        public async Task ResolveNamedNotFound()
        {
            var containerMock = new Mock<IMvvmContainer>();
            var viewName = "FakeView";
            var view = new ContentPage();
            containerMock.Setup(m => m.ResolveNamed(viewName, It.IsAny<TypedParameter>()))
                .Returns(view);

            var navigationMock = new Mock<INavigation>();

            var impl = new ViewModelNavigationImpl(containerMock.Object, true);
            navigationMock.Setup(m => m.PushAsync(view)).Returns(Task.FromResult(0));
            await ThrowsAsync<NullReferenceException>(() =>
                impl.Navigate<FakeViewModel>(navigationMock.Object));

        }

        [Fact]
        public async Task ResolveNamedWithArguments()
        {
            var containerMock = new Mock<IMvvmContainer>();
            var viewName = "FakeView";
            var view = new ContentPage();
            var expectedName = "pippo";

            containerMock.Setup(m => m.ResolveNamed(viewName, It.IsAny<TypedParameter>()))
                .Returns(view);

            var fakeViewModel = new FakeViewModelWithArgs(expectedName);

            containerMock.Setup(m => m.Resolve<FakeViewModelWithArgs>(It.IsAny<NamedParameter>()))
                .Returns(fakeViewModel);
            var navigationMock = new Mock<INavigation>();

            var impl = new ViewModelNavigationImpl(containerMock.Object, true);
            navigationMock.Setup(m => m.PushAsync(view)).Returns(Task.FromResult(0));

            //---------------

            await impl.Navigate<FakeViewModelWithArgs>(navigationMock.Object,
                new NamedParameter("name", expectedName));

            //---------------

            navigationMock.Verify(m => m.PushAsync(view), Times.Once);
            containerMock.Verify(m => m.ResolveNamed(viewName, It.Is<IParameter[]>(parameters =>
                parameters.Count(p => ((TypedParameter)p).Value == fakeViewModel) == 1)), Times.Once);

            containerMock.Verify(m=> m.Resolve<FakeViewModelWithArgs>(It.Is<IParameter[]>(parameters =>
                parameters.Count(p => ((NamedParameter)p).Value == expectedName) == 1)));
        }
    }
}
