﻿using Artemis.Core.Services.Interfaces;
using Artemis.Storage;
using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace Artemis.Core.Ninject
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            // Bind all services as singletons
            Kernel.Bind(x =>
            {
                x.FromThisAssembly()
                    .SelectAllClasses()
                    .InheritedFrom<IArtemisService>()
                    .BindAllInterfaces()
                    .Configure(c => c.InSingletonScope());
            });

            Bind<StorageContext>().ToSelf().InSingletonScope();
        }
    }
}