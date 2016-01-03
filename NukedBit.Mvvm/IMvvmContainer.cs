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

namespace NukedBit.Mvvm
{
    public interface IMvvmContainer
    {
        T Resolve<T>() where T : IViewModel;
        T Resolve<T>(params IParameter[] args) where T : IViewModel;
        object Resolve(Type viewType, params IParameter[] args);
        object ResolveNamed(string viewName, params IParameter[] args);
    }
}