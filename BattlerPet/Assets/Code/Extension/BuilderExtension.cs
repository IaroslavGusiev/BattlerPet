using System;
using System.Runtime.CompilerServices;
using VContainer;

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
            var registrationBuilder = builder.Register<T>(lifetime);
            
            builder.RegisterBuildCallback(container=>
            {
                var result = container.Resolve<T>();
                executeAfterResolving?.Invoke(result);
            });
            return registrationBuilder;
        }
    }
}