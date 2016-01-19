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
using NukedBit.Mvvm.ViewModels;
using Xamarin.Forms;

namespace NukedBit.Mvvm.Views
{
    public class ViewBase<T> : ContentPage, IDisposable, IView where T : IViewModel
    {
        private T _viewModel;

        public T ViewModel => _viewModel;

        IViewModel IView.ViewModel => _viewModel;

        protected ViewBase(T viewmodel)
        {
            _viewModel = viewmodel;
            BindingContext = _viewModel;   
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (!(ViewModel is ViewModelBase)) return;
            var modelBase = ViewModel as ViewModelBase;
            modelBase.Navigation = Navigation;
        }

      
        public virtual void Dispose()
        {
            _viewModel = default(T);
        } 
    }
}

