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

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NukedBit.Mvvm.ViewModels {

	public abstract class ViewModelBase : IViewModel , INotifyPropertyChanged, IDisposable{
		#region IDisposable implementation

		public virtual void Dispose () {
            if (PropertyChanged == null)
                return;

            var invocation = PropertyChanged.GetInvocationList();
            foreach (var p in invocation)
                PropertyChanged -= (PropertyChangedEventHandler)p;
		    PropertyChanged = null;
		}

		#endregion

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		protected void OnNotifyPropertyChanged([CallerMemberName]string name = null) {
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (name));
		}

        public INavigation Navigation { get; internal set; }


	    public virtual Task Initialize(params IParameter[] parameters)
	    {
	        return Task.FromResult(0);
	    }

	    protected void InvokeOnMainThread(Action action)
	    {
	        Device.BeginInvokeOnMainThread(action);
	    }
    }
}
