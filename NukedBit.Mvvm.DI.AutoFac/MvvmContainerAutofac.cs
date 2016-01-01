using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using NukedBit.Mvvm.ViewModels;

namespace NukedBit.Mvvm.DI.AutoFac
{
    public class MvvmContainerAutofac : IMvvmContainer
    {
        private readonly IComponentContext _context;
        internal MvvmContainerAutofac(IComponentContext context)
        {
            _context = context;
        }

        public T Resolve<T>() where T : IViewModel
        {
            return _context.Resolve<T>();
        }

        public T Resolve<T>(params IParameter[] args) where T : IViewModel
        {
            var parameters = GetAutofacParameters(args);
            return _context.Resolve<T>(parameters);
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
            var parameters = GetAutofacParameters(args);
            return _context.Resolve(viewType, parameters);
        }

        public static IMvvmContainer Create(IComponentContext context)
        {
            return new MvvmContainerAutofac(context);
        }
    }
}