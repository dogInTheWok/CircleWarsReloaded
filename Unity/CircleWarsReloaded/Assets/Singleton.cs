using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class Singleton<T> where T: new()
    {
        static public T Instance()
        {
            if (null == s_instance)
            {
                s_instance = new T();
            }

            return s_instance;
        }
        static private T s_instance;
    }
}
