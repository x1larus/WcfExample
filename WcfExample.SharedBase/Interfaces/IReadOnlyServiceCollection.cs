using Microsoft.Extensions.DependencyInjection;

namespace WcfExample.SharedBase.Interfaces
{
    public interface IReadOnlyServiceCollection
    {
        TService GetRemoteService<TService>();
        TService Get<TService>();
    }
}
