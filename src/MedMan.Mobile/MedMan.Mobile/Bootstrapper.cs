using Autofac;
using MedMan.Mobile.ViewModels;
using MedMan.Mobile.Services;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace MedMan.Mobile
{
    public abstract class Bootstrapper
    {
        protected ContainerBuilder ContainerBuilder
        {
            get; private set;
        }

        public Bootstrapper()
        {
            Initialize();
            FinishInitialization();
        }

        protected virtual void Initialize()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            ContainerBuilder = new ContainerBuilder();
            foreach (var type in currentAssembly.DefinedTypes
                .Where(e =>
                e.IsSubclassOf(typeof(Page)) ||
                e.IsSubclassOf(typeof(BaseViewModel))))
            {
                ContainerBuilder.RegisterType(type.AsType());
            }

            ContainerBuilder.RegisterType<AdministrationsService>().As<IAdministrationsService>().SingleInstance();
            ContainerBuilder.RegisterType<MedsService>().As<IMedicationsService>().SingleInstance();
            ContainerBuilder.RegisterType<PatientService>().As<IPatientService>().SingleInstance();
            ContainerBuilder.RegisterType<ScriptsService>().As<IPrescriptionsService>().SingleInstance();
        }

        private void FinishInitialization()
        {
            var container = ContainerBuilder.Build();
            Resolver.Initialize(container);
        }
    }
}
