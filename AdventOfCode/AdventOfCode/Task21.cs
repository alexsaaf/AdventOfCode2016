using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task21 {

        public string ScramblePassword(string original, string[] operations) {
            char[] password = original.ToArray();
            foreach (String operation in operations) {
                string[] components = operation.Split(' ');
                switch (components[0]) {
                    case "swap":
                        if (components[1].Equals("position")) {
                            SwapPositions(ref password, Int16.Parse(components[2]), Int16.Parse(components[5]));
                        } else {
                            SwapLetters(ref password, components[2], components[5]);
                        }
                        break;
                    case "rotate":
                        break;
                    case "reverse":
                        break;
                    case "move":
                        break;
                }


            }
            return password.ToString();
        }

        void SwapPositions(ref char[] password, int firstIndex, int secondIndex) {
            //Get the characters
            char firstCharacter = password[firstIndex];
            char secondCharacter = password[secondIndex];
            //Put them back, but with swapped positions
            password[firstIndex] = secondCharacter;
            password[secondIndex] = firstCharacter;
        }

        void SwapLetters(ref char[] password, char firstLetter, char secondLetter) {



        }
    }
}
