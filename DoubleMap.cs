using System;
using System.Collections.Generic;
using System.Collections;

namespace AIEdit
{
    class DoubleMap<A, B>
    {
        private Hashtable tablea, tableb;

        public DoubleMap()
        {
            tablea = new Hashtable();
            tableb = new Hashtable();
        }

        /// <summary>
        /// Get a list of all stores values of type A.
        /// </summary>
        public ICollection ValuesA { get { return tablea.Values; } }

        /// <summary>
        /// Get a list of all stores values of type B.
        /// </summary>
        public ICollection ValuesB { get { return tableb.Values; } }

        public A this[B index]
        {
            get
            {
                return (A)tablea[index];
            }

            set
            {
                tablea[index] = (A)value;
                tableb[(A)value] = index;
            }
        }

        public B this[A index]
        {
            get
            {
                return (B)tableb[index];
            }

            set
            {
                tableb[index] = (B)value;
                tablea[(B)value] = index;
            }
        }
    }
}
