using System;

namespace Managers 
{
    public interface ISingleton<T>
    {
        public static T Instance { get; }
    }
}
