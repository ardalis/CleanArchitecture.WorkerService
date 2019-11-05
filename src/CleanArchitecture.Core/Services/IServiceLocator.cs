using System;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Core.Services
{
    public interface IServiceLocator : IDisposable
    {
        IServiceScope CreateScope();
        void Dispose();
        T Get<T>();
    }
}
