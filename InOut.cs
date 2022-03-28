using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace U4_22
{
    static class InOut
    {

        /// <summary>
        /// A method that prints 10 shortes words from the text and prints longests fragment and finds his line number
        /// </summary>
        /// <param name="fd">data file name</param>
        /// <param name="fr">result file name</param>
        /// <param name="Repetition">Words without repetition</param>
        /// <param name="lineNumber">line number</param>
        public static void PrintToRodikliai(string fd, string fr, List<Word> Repetition, int lineNumber)
        {
            using (var Writer = File.CreateText(fr))
            {
                if (Repetition.Count > 10)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Writer.WriteLine("{0} | {1}", Repetition[i].Zodis, Repetition[i].Pasikartoja);
                        
                    }
                }

                else
                {
                    for (int i = 0; i < Repetition.Count; i++)
                    {
                        Writer.WriteLine("{0} | {1}", Repetition[i].Zodis, Repetition[i].Pasikartoja);
                    }
                }


                string text = TaskUtils.LongestFragment(fd, out lineNumber);
                Writer.WriteLine("Ilgiausias teksto fragmentas, kur zodzio paskutine raide sutampa su sekancio zodzio pirmaja raide: ");
                Writer.WriteLine(text);

                Writer.WriteLine("Fragmentas yra {0} eiluteje", lineNumber+1);
            }
        }
    }
}
