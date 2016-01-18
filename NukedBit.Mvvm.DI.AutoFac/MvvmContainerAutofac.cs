using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using NukedBit.Mvvm.ViewModels;
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
        private readonly IContainer _context;
        internal MvvmContainerAutofac(IContainer context)
        {
            _context = context;            
        }

        public T Resolve<T>() where T : IViewModel
        {
            using (var scope = _context.BeginLifetimeScope())
                return scope.Resolve<T>();
        }

        public T Resolve<T>(params IParameter[] args) where T : IViewModel
        {
            using (var scope = _context.BeginLifetimeScope())
            {
                var parameters = GetAutofacParameters(args);
                return scope.Resolve<T>(parameters);
            }
        }

        private static IEnumerable<Parameter> GetAutofacParameters(IParameter[] args)
        {
            var parameters = args.Select(p =>
            {
                var namedParameter = p as NamedParameter;
                if (namedParameter != null)
                {
                    var np = namedParameter;
                    return (Parameter) new Autofac.NamedParameter(np.Name, np.Value);
                }
                var tp = (TypedParameter) p;
                return new Autofac.TypedParameter(tp.Type, tp.Value);
            });
            return parameters;
        }

        public object Resolve(Type viewType, params IParameter[] args)
        {
            using (var scope = _context.BeginLifetimeScope())
            {
                var parameters = GetAutofacParameters(args);
                return scope.Resolve(viewType, parameters);
            }
        }

        public object ResolveNamed(string viewName, params IParameter[] args)
        {
            using (var scope = _context.BeginLifetimeScope())
            {
                var parameters = GetAutofacParameters(args);
                return scope.ResolveNamed<ContentPage>(viewName, parameters);
            }
        }

        public static IMvvmContainer Create(IContainer context)
        {
            return new MvvmContainerAutofac(context);
        }
    }
}