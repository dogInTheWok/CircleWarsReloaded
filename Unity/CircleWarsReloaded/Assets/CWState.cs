using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class CWState<T> where T : IComparable
    {
        public delegate void Slot(T state);

        private T value;
        private List<Slot> slots;

        public CWState()
        {
            slots = new List<Slot>();
        }

        public void ConnectTo(Slot slot)
        {
            slots.Add(slot);
        }

        public T Value
        {
            get { return value; }
            set
            {
                if (value.CompareTo(this.value) == 0)
                    return;
                this.value = value;
                foreach( Slot slot in slots )
                {
                    slot(value);
                }
            }
        }
    }
}
