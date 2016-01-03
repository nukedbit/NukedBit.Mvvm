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
        private readonly bool _searchByName;

        public ViewModelNavigationImpl(IMvvmContainer container, bool searchByName)
        {
            _container = container;
            _searchByName = searchByName;
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
            var assemblyQualifiedName = model.GetType().AssemblyQualifiedName;
            var viewModelName = model.GetType().Name;
            var viewName = ExtractViewName(viewModelName);
            if (!_searchByName)
            {
                var viewType = GetViewType(assemblyQualifiedName, viewModelName, viewName);
                return (ContentPage) _container.Resolve(viewType,
                    new TypedParameter(model.GetType(), model));
            }
            return (ContentPage) _container.ResolveNamed(viewName, new TypedParameter(model.GetType(), model));
        }

        private static Type GetViewType(string assemblyQualifiedName, string viewModelName, string viewName)
        {
            var viewFullTypeName = assemblyQualifiedName.Replace(viewModelName, viewName);
            viewFullTypeName = viewFullTypeName.Replace(".ViewModels.", ".Views.");
            var viewType = Type.GetType(viewFullTypeName);
            return viewType;
        }

        private static string ExtractViewName(string viewModelName)
        {
            var viewName = viewModelName.Remove(viewModelName.IndexOf("Model", StringComparison.Ordinal));
            return viewName;
        }
    }
}
