using Microsoft.Extensions.DependencyInjection;

namespace Core.DI
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AutomaticRegistrationAttribute : Attribute
    {
        public ServiceLifetime Lifetime { get; init; } = ServiceLifetime.Scoped; 
    }
}
