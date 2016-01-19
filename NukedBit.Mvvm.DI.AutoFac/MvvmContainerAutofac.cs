using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Core.Lifetime;
using NukedBit.Mvvm.ViewModels;
using NukedBit.Mvvm.Views;
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

using Xamarin.Forms;

namespace NukedBit.Mvvm.DI.AutoFac
{
    public class MvvmContainerAutofac : IMvvmContainer
    {
        private readonly LifetimeScope _context;
        private Dictionary<IView, ILifetimeScope> _viewsScopes = new Dictionary<IView, ILifetimeScope>();
        private Dictionary<IViewModel, ILifetimeScope> _viewModelsScopes = new Dictionary<IViewModel, ILifetimeScope>();

        internal MvvmContainerAutofac(LifetimeScope context)
        {
            _context = context;
        }

        public void ReleaseView(IView view)
        {
            using (_viewsScopes[view])
            {
                using (_viewModelsScopes[view.ViewModel])
                {
                    _viewModelsScopes.Remove(view.ViewModel);
                    _viewsScopes.Remove(view);
                }
            }
        }

        public T Resolve<T>() where T : IViewModel
        {
            var scope = _context.BeginLifetimeScope();
            var viewModel = scope.Resolve<T>(); ;
            _viewModelsScopes.Add(viewModel, scope);
            return viewModel;
        }

        public T Resolve<T>(params IParameter[] args) where T : IViewModel
        {
            var scope = _context.BeginLifetimeScope();
            var parameters = GetAutofacParameters(args);
            var viewModel = scope.Resolve<T>(parameters); ;
            _viewModelsScopes.Add(viewModel, scope);
            return viewModel;
        }

        private static IEnumerable<Parameter> GetAutofacParameters(IParameter[] args)
        {
            var parameters = args.Select(p =>
            {
                var namedParameter = p as NamedParameter;
                if (namedParameter != null)
                {
                    var np = namedParameter;
                    return (Parameter)new Autofac.NamedParameter(np.Name, np.Value);
                }
                var tp = (TypedParameter)p;
                return new Autofac.TypedParameter(tp.Type, tp.Value);
            });
            return parameters;
        }


        public object ResolveNamed(string viewName, params IParameter[] args)
        {
            var scope = _context.BeginLifetimeScope();
            var parameters = GetAutofacParameters(args);
            var view = scope.ResolveNamed<ContentPage>(viewName, parameters);
            _viewsScopes.Add((IView)view, scope);
            return view;
        }

        public static IMvvmContainer Create(LifetimeScope context)
        {
            return new MvvmContainerAutofac(context);
        }
    }
}