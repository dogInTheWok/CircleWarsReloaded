using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class CWState<T> where T : IComparable
    {
        public delegate void Slot0();
        public delegate void Slot1(T state);
        
        private T value;
        private List<Slot0> slot0s;
        private List<Slot1> slot1s;
        
        public CWState()
        {
            slot1s = new List<Slot1>();
            slot0s = new List<Slot0>();
        }
        public T Value
        {
            get { return value; }
            set
            {
                if (value.CompareTo(this.value) == 0)
                    return;
                this.value = value;
                foreach (Slot1 slot in slot1s)
                {
                    slot(value);
                }
                foreach (Slot0 slot in slot0s)
                {
                    slot();
                }
            }
        }

        public void ConnectTo(Slot0 slot)
        {
            slot0s.Add(slot);
        }
        public void ConnectTo(Slot1 slot)
        {
            slot1s.Add(slot);
        }
        
        
    }
}
