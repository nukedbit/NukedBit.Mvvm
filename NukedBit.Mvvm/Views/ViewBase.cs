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

using NukedBit.Mvvm.ViewModels;
using Xamarin.Forms;

namespace NukedBit.Mvvm.Views
{
    public class ViewBase<T> : ContentPage where T : IViewModel, IView
    {
        private readonly T _viewModel;

        public T ViewModel => _viewModel;

        protected ViewBase(T viewmodel)
        {
            _viewModel = viewmodel;
            BindingContext = _viewModel;
        }
    }
}

