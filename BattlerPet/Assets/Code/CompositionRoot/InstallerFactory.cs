using System;
using VContainer.Unity;
using System.Collections.Generic;

namespace Code.CompositionRoot
{
    public class InstallerFactory
    {
        public List<IInstaller> Installers { get; } = new();
        
        public void Create<T>() where T : IInstaller, new()
        {
            var instance = new T();
            AddInstallerToList(instance);
        }
        
        public void Create<T>(object constructorArgs) where T : IInstaller
        {
            Type type = typeof(T);
            var instance = (T)Activator.CreateInstance(type, constructorArgs);
            AddInstallerToList(instance);
        }

        private void AddInstallerToList<T>(T instance) where T : IInstaller
        {
            if (!Installers.Contains(instance))
                Installers.Add(instance);
        }
    }
}