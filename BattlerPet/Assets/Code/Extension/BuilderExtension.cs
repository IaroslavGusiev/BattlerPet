using System;
using VContainer;
using UnityEngine;
using VContainer.Unity;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CodeBase.Extensions
{
    public static class BuilderExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder RegisterNonLazy<T>(this IContainerBuilder builder, Lifetime lifetime = Lifetime.Singleton)
        {
            return RegisterNonLazy<T>(builder, null, lifetime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder RegisterNonLazy<T>(this IContainerBuilder builder, Action<T> executeAfterResolving, Lifetime lifetime = Lifetime.Singleton)
        {
            RegistrationBuilder registrationBuilder = builder.Register<T>(lifetime);
            
            builder.RegisterBuildCallback(container=>
            {
                var result = container.Resolve<T>();
                executeAfterResolving?.Invoke(result);
            });
            return registrationBuilder;
        }

        public static GameObject InstantiateAndInject([NotNull] this IObjectResolver resolver, [NotNull] GameObject prefab, Transform parent = null)
        {
            if (prefab == null)
                throw new NullReferenceException(nameof(prefab));
            
            bool prefabWasActive = prefab.activeSelf;
            prefab.SetActive(false);
            GameObject instance = resolver.Instantiate(prefab, parent);
            prefab.SetActive(prefabWasActive);
            instance.SetActive(prefabWasActive);
            return instance;
        }
    }
}