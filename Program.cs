using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U4_22
{
    class Program
    {
        static void Main(string[] args)
        {
            const string fd = "Knyga.txt";
            const string fr = "Rodikliai.txt";
            const string fa = "ManoKnyga.txt";

            char[] skyrikliai = { '.', ',', ';', ':', ' ', '!', '?', '(', ')', '\t', '"' };
            int lineNumber=0;

            List<string> AllWords = TaskUtils.AllWordsInLines(fd, skyrikliai);
            List<Word> filtered = TaskUtils.WordsWithoutRepetition(AllWords);
            TaskUtils.Sort(filtered);

            InOut.PrintToRodikliai(fd, fr, filtered, lineNumber);
            TaskUtils.Positions(fd, fa);
           
           
            
        }
    }
}
