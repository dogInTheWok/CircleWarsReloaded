using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class CWState<T> where T : IComparable
    {
        private T value;
        private List<Listener> listeners;

        public abstract class Listener
        {
            public abstract void OnStateChange(T state);
        }

        public CWState()
        {
            listeners = new List<Listener>();
        }

        public void ConnectTo(Listener listener)
        {
            listeners.Add(listener);
        }

        public T Value
        {
            get { return value; }
            set
            {
                if (value.CompareTo(this.value) == 0)
                    return;
                this.value = value;
                foreach (Listener listener in listeners)
                {
                    listener.OnStateChange(value);
                }
            }
        }
    }
}
