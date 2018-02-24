using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FlexGrid.Core.IoC;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using UndoManager;

namespace FlexGrid.UI
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IUnityContainer _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Initialize();
        }

        private void Initialize()
        {
            _container = CreateContainer();
            PopulateContainer();
        }

        private IUnityContainer CreateContainer()
        {
            return new UnityContainer();
        }


        private void PopulateContainer()
        {
            // Here we identify services with the classes/instances that implement these services
            // IServiceLocator -> UnityAdapter
            // IEventAggregator -> EventAggregator
            // IUndoManager -> UndoManager
            // etc

            // TODO : [?] What is the difference between RegisterType and RegisterInstance

            // Service Locator
            // Set the ServiceLocator implementation type
            _container.RegisterType(typeof(IServiceLocator), typeof(UnityAdapter), new ContainerControlledLifetimeManager());
            //var serviceLocator = this._container.Resolve<IServiceLocator>();
            ServiceLocator.SetLocatorProvider(() => this._container.Resolve<IServiceLocator>()); // [?] I'm not sure why we need this .. See ans below
            // Learning
            // [?] Why we require Service Locator, and what does ServiceLocator.SetLocatorProvider() do ?
            // The ans is a bit round and round :)
            // In this project, we decided to use UnityContainer as our container for registering and resolving services (we could have used any other, or created our own).
            // Now, since we want to have the flexibility to change our container at any time in future, we want to de-couple it from the rest of the code
            // For this, we are using a class called ServiceLocator
            // {{ Imp
            // i.e. instead of using container.Resolve<ISomeService>(), we would be using ServiceLocator.Current.GetInstance<ISomeService>();
            // }} Imp
            // Notice the 'ServiceLocator.Current' - it returns the current provider, i.e. the provider that will be responsible for resolving services
            // We want to set the ServiceLocator's provider as UnityContainer. Unless we specify a provider, ServiceLocator.Current will keep returning an AccessViolation exception
            // 'ServiceLocator.Current' threw an exception of type 'System.AccessViolationException'	Microsoft.Practices.ServiceLocation.IServiceLocator {System.AccessViolationException}
            // The way to specify ServiceLocator's provider is to call ServiceLocator.SetLocatorProvider() and pass it a delegate that would return an instance of ServiceLocatorImplBase
            // So we implement this in a class UnityAdapter and the UnityAdapter uses the UnityContainer (passed by us) for doing a resolve.
            //
            // And now read again ..
            //
            // _container.RegisterType(typeof(IServiceLocator), typeof(UnityAdapter), new ContainerControlledLifetimeManager());
            // ServiceLocator.SetLocatorProvider(() => this._container.Resolve<IServiceLocator>());




            // Event Aggregator
            var eventAggregator = new EventAggregator();
            this._container.RegisterInstance(typeof(IEventAggregator), eventAggregator, new ContainerControlledLifetimeManager());

            // Undo Manager
            var undoManager = UndoManager.UndoManager.Instance;
            _container.RegisterInstance<Services.IUndoManager>(undoManager);

            //// Test
            //var x = ServiceLocator.Current.GetInstance<IUnityContainer>();
            //var y = ServiceLocator.Current.GetInstance<IUndoManager>();
        }

    }

}
