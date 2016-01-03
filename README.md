# NukedBit.Mvvm

### Why another mvvm framework?

Because i wan't a simple framework that just do the basic stuff and had native support for Xamarin

# How it works

Currently there is only one implementation for IMvvmContainer 
wich is an abstraction to support containers, at the moment i only have provided
autofac because is the one i have used on my projects, but it is very easy to add support
for others.

NukedBit.Mvvm differ also from other frameworks because it use a lot dependency injection, 
you can in fact pass argument to viewmodels.

# What is implemented?

Very basic stuff IViewModelNavigator also as only what i needed at the moment, 
i hope to add in future more features, but you are welcome to make pull requests :)


Examples can be fount at [https://github.com/nukedbit/nukedbit-mvvm-examples](https://github.com/nukedbit/nukedbit-mvvm-examples)




	Install-Package NukedBit.Mvvm

	Install-Package NukedBit.Mvvm.DI.AutoFac
