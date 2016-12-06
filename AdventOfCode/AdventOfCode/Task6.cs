using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task6 {

        Dictionary<char, int[]> letterAppearances;

        public string InterpretMessage(string[] transmissions, bool modifiedCode) {
            string result = "";
            //Keep a dictionary of the count of each letter for each column
            letterAppearances = new Dictionary<char, int[]>();

            //For every transmission, add every letter to the dictionary
            foreach(string transmission in transmissions) {
                for(int i = 0; i < transmission.Length; i++) {
                    if (letterAppearances.ContainsKey(transmission[i])) {
                        letterAppearances[transmission[i]][i]++;
                    }else {
                        int[] values = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
                        values[i]++;
                        letterAppearances.Add(transmission[i], values);
                    }
                }
            } 

            //For every column, find the most common letter (Key with the highest value in that col)
            for(int i = 0; i < transmissions[0].Length; i++) {
                if (!modifiedCode) {
                    result += GetMostCommonLetter(i);
                } else {
                    result += GetLeastCommonLetter(i);
                }
            }
            return result;
        }

        //Finds the most common letter at given index. (Key with the highest value[i])
        char GetMostCommonLetter(int index) {
            char[] keys = letterAppearances.Keys.ToArray();
            int mostAppearances = 0;
            char letter = ' ';
            foreach(char key in keys) {
                //If it is higher, we have a new mostappearances
                if (letterAppearances[key][index] > mostAppearances) {
                    mostAppearances = letterAppearances[key][index];
                    letter = key;
                }
            }
            return letter;
        }

        //Finds the least common letter at given index. (Key with the lowest value[i])
        char GetLeastCommonLetter(int index) {
            char[] keys = letterAppearances.Keys.ToArray();
            int leastAppearances = int.MaxValue;
            char letter = ' ';
            foreach (char key in keys) {
                //If it is higher, we have a new mostappearances
                if (letterAppearances[key][index] < leastAppearances) {
                    leastAppearances = letterAppearances[key][index];
                    letter = key;
                }
            }
            return letter;
        }
    }
}
