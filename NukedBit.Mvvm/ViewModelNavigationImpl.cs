/***************************************************************************

   Copyright 2015  Sebastian Faltoni aka NukedBit

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

*****************************************************************************/

using System;
using System.Threading.Tasks;
using NukedBit.Mvvm.ViewModels;
using Xamarin.Forms;

namespace NukedBit.Mvvm
{    
    internal class ViewModelNavigationImpl : IViewModelNavigator
    {
        private readonly IMvvmContainer _container;

        public ViewModelNavigationImpl(IMvvmContainer container)
        {
            _container = container;
        }

        #region IViewModelNavigator implementation
        public async Task Navigate<T>(INavigation navigator) where T : IViewModel
        {
            var viewModel = _container.Resolve<T>();
            var view = ResolveView(viewModel);
            await navigator.PushAsync(view);
        }
        public async Task Navigate<T>(INavigation navigator, params IParameter[] args) where T : IViewModel
        {
            var viewModel = _container.Resolve<T>(args);
            var view = ResolveView(viewModel);
            await navigator.PushAsync(view);
        }
        #endregion

        private ContentPage ResolveView(IViewModel model)
        {
            var viewModelName = model.GetType().Name;
            var viewName = ExtractViewName(viewModelName);

            return (ContentPage) _container.ResolveNamed(viewName, new TypedParameter(model.GetType(), model));
        }

        private static string ExtractViewName(string viewModelName)
        {
            var viewName = viewModelName.Remove(viewModelName.IndexOf("Model", StringComparison.Ordinal));
            return viewName;
        }
    }
}
