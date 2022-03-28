using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace U4_22
{
    class TaskUtils
    {
        /// <summary>
        /// A method that splits all words to lines
        /// </summary>
        /// <param name="fd">data file</param>
        /// <param name="skyrikliai">seperators</param>
        /// <returns>AllWords list</returns>
        public static List<string> AllWordsInLines(string fd, char[] skyrikliai)
        {
            List<string> AllWords = new List<string>();
            string[] lines = File.ReadAllLines(fd, Encoding.UTF8);

            foreach (string line in lines)
            {
                string[] words = line.Split(skyrikliai, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < words.Length; i++)
                {
                    AllWords.Add(words[i].ToLower());
                }
            }
            return AllWords;
        }

        /// <summary>
        /// A method that deletes words who are written two or more times 
        /// </summary>
        /// <param name="AllWordsInLines">list</param>
        /// <returns>Words without repetition list</returns>

        public static List<Word> WordsWithoutRepetition(List<string> AllWordsInLines)
        {
            List<Word> WordsWithout = new List<Word>();

            var words = AllWordsInLines.Distinct().ToList();

            for (int i = 0; i < words.Count; i++)
            {
                int count = 0;
                for (int j = 0; j < AllWordsInLines.Count; j++)
                {

                    if (words[i].Length > 3)
                    {
                        if (words[i].Equals(AllWordsInLines[j]))
                        {
                            count++;
                        }
                    }
                }
                if (count > 0)
                {
                    WordsWithout.Add(new Word(words[i], count));
                }
            }

            return WordsWithout;
        }

        /// <summary>
        /// A method who sorts words by length
        /// </summary>
        /// <param name="WordsWithoutRepetition">Words without repetition list</param>

        public static void Sort(List<Word> WordsWithoutRepetition)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < WordsWithoutRepetition.Count - 1; i++)
                {
                    Word a = WordsWithoutRepetition[i];
                    Word b = WordsWithoutRepetition[i + 1];

                    if (a.CompareTo(b) > 0)
                    {
                        WordsWithoutRepetition[i] = b;
                        WordsWithoutRepetition[i + 1] = a;
                        flag = true;
                    }
                }
            }
        }

        /// <summary>
        /// A method that finds the longest text fragment where words start with the words behind last letter
        /// </summary>
        /// <param name="fd">data file</param>
        /// <param name="LineNumber">line number</param>
        /// <returns>longest fragment</returns>
        public static string LongestFragment(string fd, out int LineNumber)
        {
            int big = Int32.MinValue;

            List<string> fragments = FindFragments(fd);
            string[] lines = File.ReadAllLines(fd);

            LineNumber=0;

            if (fragments.Count == 0)
                return "Nera ieskomo fragmento!";
            else
            {
                string longest = fragments[0];
                for (int i = 0; i < fragments.Count; i++)
                {
                    if (fragments[i].Length > big)
                    {
                        big = fragments[i].Length;
                        longest = fragments[i];
                    }
                }

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].IndexOf(longest) > -1)
                    {
                        LineNumber = i;
                    }

                }

                return longest;
            }

        }

        /// <summary>
        /// A method that finds all required text fragments
        /// </summary>
        /// <param name="fd">data file</param>
        /// <returns>returns fragments</returns>
        public static List<string> FindFragments(string fd)
        {
            List<string> fragment = new List<string>();

            string pattern = @"(?:\b\w+(\w)\b[\W]*(?=\1))*\1\w+";

            Regex words = new Regex(pattern, RegexOptions.IgnoreCase);

            string[] lines = File.ReadAllLines(fd);

            foreach (string line in lines)
            {
                foreach (Match regex in words.Matches(line))
                {
                    //Console.WriteLine(regex);
                    fragment.Add(regex.Value);
                }
            }

            return fragment;
        }

        /// <summary>
        /// A method that finds how many positions should every word of the line take and prints all words in their position
        /// </summary>
        /// <param name="fd">data file</param>
        /// <param name="fa">data file</param>
        public static void Positions(string fd, string fa)
        {
            string[] lines = File.ReadAllLines(fd);
            int[] positions = new int[80];

            Regex regex = new Regex("[A-Za-z]+[ ;.,'!?:]*");

            foreach (string line in lines)
            {
                int k = 0;
                string[] words = new string[80];
                foreach (Match match in regex.Matches(line))
                {
                    words[k] = match.ToString();
                    k++;
                }

                for (int i = 0; i < words.Count(); i++)
                {
                    if (words[i] == null)
                    {
                        break;
                    }
                    if (words[i].Length > positions[i])
                    {
                        positions[i] = words[i].Length;
                    }
                }
            }

            using (var Writer = File.CreateText(fa))
            {
                foreach (string line in lines)
                {
                    int k = 0;
                    string[] words = new string[80];
                    foreach (Match match in regex.Matches(line))
                    {

                        words[k] = match.ToString();
                        k++;
                    }
                    StringBuilder newLine = new StringBuilder();
                    for (int i = 0; i < words.Count(); i++)
                    {
                        if (words[i] == null)
                        {
                            break;
                        }

                        newLine.Append(words[i]);
                        int space = positions[i] - words[i].Length;
                        newLine.Append(' ', space + 2);
                    }
                    Writer.WriteLine(newLine.ToString());
                }
            }
        }


    }
}
