using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U4_22
{
    class Word
    {
        public string Zodis { get; set; }
        public int Pasikartoja { get; set; }

        public Word(string zodis, int pasikartoja)
        {
            this.Zodis = zodis;
            this.Pasikartoja = pasikartoja;
        }

        public int CompareTo(Word other)
        {
            int comp = this.Zodis.Length.CompareTo(other.Zodis.Length);

            return comp;
        }
    }
}
