﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task21 {

        public string ScramblePassword(string original, string[] operations) {
            string password = original;
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
                        if (components[1].Equals("based")) {
                            //Do the more advanced rotation
                            int rotateBy = password.IndexOf(components[6]);
                            if(rotateBy > 3) {
                                rotateBy++;
                            }
                            rotateBy++;
                            Rotate(ref password, false, rotateBy);
                        }else {
                            //Perform a basic rotation
                            Rotate(ref password, (components[1].Equals("left")), Int16.Parse(components[2]));
                        }
                        break;
                    case "reverse":
                        //Find the indexes to reverse between
                        int firstIndex = Int16.Parse(components[2]);
                        int secondIndex = Int16.Parse(components[4]);
                        Reverse(ref password, firstIndex, secondIndex);
                        break;
                    case "move":
                        int moveFrom = Int16.Parse(components[2]);
                        int moveTo = Int16.Parse(components[5]);
                        Move(ref password, moveFrom, moveTo);
                        break;
                }
                //Console.WriteLine(password);
                //Console.ReadKey();


            }
            return password;
        }

        /// <summary>
        /// Unscrambles the password by performing all actions in reverse.
        /// ALt: Since 8! is not that large, we could simply bruteforce instead, 
        /// by running all permutations of the letters through the scramblepassword
        /// and compare the result to our given scrambled password untill we find a
        /// match.
        /// </summary>
        /// <param name="scrambled"></param>
        /// <param name="operations"></param>
        /// <returns></returns>
        public string UnscramblePassword(string scrambled, string[] operations) {
            string password = scrambled;
            //We need to perform all the operations backwards
            for (int i = operations.Count() - 1; i >= 0; i--) {
                string operation = operations[i];
                string[] components = operation.Split(' ');
                switch (components[0]) {
                    case "swap":
                        if (components[1].Equals("position")) {
                            SwapPositions(ref password, Int16.Parse(components[5]), Int16.Parse(components[2]));
                        } else {
                            SwapLetters(ref password, components[5], components[2]);
                        }
                        break;
                    case "rotate":
                        if (components[1].Equals("based")) {
                            //Do the more advanced rotation
                            int characterIndex = password.IndexOf(components[6]);
                            //  pos     shift       newpos
                            //  0       1           1
                            //  1       2           3
                            //  2       3           5
                            //  3       4           7
                            //  4       6           2
                            //  5       7           4
                            //  6       8           6
                            //  7       9           0
                            // All odds follow one pattern and all even follow one pattern. 0 behaves like an odd index.
                            int rotateBy = characterIndex / 2 + ((characterIndex % 2 == 1 || characterIndex == 0) ? 1 : 5);
                            Rotate(ref password, true, rotateBy);
                        } else {
                            //Perform a basic rotation
                            Rotate(ref password, (!components[1].Equals("left")), Int16.Parse(components[2]));
                        }
                        break;
                    case "reverse":
                        //Find the indexes to reverse between
                        int firstIndex = Int16.Parse(components[2]);
                        int secondIndex = Int16.Parse(components[4]);
                        Reverse(ref password, firstIndex, secondIndex);
                        break;
                    case "move":
                        int moveFrom = Int16.Parse(components[2]);
                        int moveTo = Int16.Parse(components[5]);
                        Move(ref password, moveTo, moveFrom);
                        break;
                }
            }

            return password;
        }

        void SwapPositions(ref string password, int firstIndex, int secondIndex) {
            //Get the characters
            string firstCharacter = password[firstIndex].ToString();
            string secondCharacter = password[secondIndex].ToString();

            Console.WriteLine("Swapping " + firstCharacter + " and " + secondCharacter);
            //Put them back, but with swapped positions
            password = password.Remove(firstIndex, 1);
            password = password.Insert(firstIndex, secondCharacter);

            password = password.Remove(secondIndex, 1);
            password = password.Insert(secondIndex, firstCharacter);
        }

        void SwapLetters(ref string password, string firstLetter, string secondLetter) {
            int indexOfFirst = password.IndexOf(firstLetter);
            int indexOfSecond = password.IndexOf(secondLetter);

            SwapPositions(ref password, indexOfFirst, indexOfSecond);
        }

        void Rotate(ref string password, bool rotateLeft, int steps) {
            for (int stepsTaken = 0; stepsTaken < steps; stepsTaken++) {

                if (rotateLeft) {
                    //Save the letter that will end up at the end of the password
                    char tmp = password[0];
                    Console.WriteLine("TMP is: " + tmp);
                    //Rotate the password
                    for (int i = 0; i < password.Count() - 1; i++) {
                        string temp = password[i + 1].ToString();
                        password = password.Remove(i, 1);
                        password = password.Insert(i, temp);
                    }
                    password = password.Remove(password.Count() - 1, 1);
                    password = password.Insert(password.Count(), tmp.ToString());
                } else {
                    char tmp = password[password.Count() - 1];
                    for (int i = password.Count() - 1; i > 0; i--) {
                        password = password.Remove(i, 1);
                        password = password.Insert(i, password[i - 1].ToString());
                    }
                    password = password.Remove(0, 1);
                    password = password.Insert(0, tmp.ToString());
                }
            }
        }

        void Reverse(ref string password, int startIndex, int endIndex) {
            Console.WriteLine("Reversing " + startIndex + " to " + endIndex);
            string start = password.Substring(0, startIndex);
            string toReverse = password.Substring(startIndex, endIndex - startIndex + 1);
            string end = password.Substring(endIndex + 1, password.Length - endIndex - 1);

            char[] charArray = toReverse.ToCharArray();
            Array.Reverse(charArray);
            password = start + new string(charArray) + end;
        }

        void Move(ref string password, int moveFrom, int moveTo) {
            char character = password.ElementAt(moveFrom);
            
            password = password.Remove(moveFrom,1);
            password = password.Insert(moveTo, character.ToString());
        }
    }
}
