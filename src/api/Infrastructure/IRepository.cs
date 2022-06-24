using System;

namespace api.Infrastructure
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
    }
}
