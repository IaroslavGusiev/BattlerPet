using VContainer;

namespace CodeBase.Extensions
{
    public static class BuilderExtension
    {
        public static RegistrationBuilder RegisterNonLazy<T>(this IContainerBuilder builder, Lifetime lifetime = Lifetime.Singleton)
        {
            RegistrationBuilder registrationBuilder = builder.Register<T>(lifetime);
            
            builder.RegisterBuildCallback(container=> { container.Resolve<T>(); });
            return registrationBuilder;
        }
    }
}