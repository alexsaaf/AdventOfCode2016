using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task14 {

        //Struct used to store the potential keys that have been found
        struct foundKey {
            public int index;
            public string hash;
            public char repeatedCharacter;

            public foundKey(int index, string hash, char repeatedCharacter) {
                this.index = index;
                this.hash = hash;
                this.repeatedCharacter = repeatedCharacter;
            }
        }

        List<foundKey> confirmedKeys;
        List<foundKey> potentialKeys;

        public int GenerateKey(int keyIndex, string salt, int stretchHashBy) {
            int index = 0;
            confirmedKeys = new List<foundKey>();
            potentialKeys = new List<foundKey>();

            //While we have not found enough keys
            while(confirmedKeys.Count < keyIndex + 1) {
                //Generate a new hash
                string hash = CalculateHashWithStretch(salt + index.ToString(), stretchHashBy);
                //Check if it contains a sequence of 5 of one character
                Match match = Regex.Match(hash, "(.)\\1{4,}");
                if (match.Success) {
                    char character = match.ToString()[0];

                    //We can't remove keys from potentialkeys while we are traversing it, so save them for later
                    HashSet<foundKey> isConfirmed = new HashSet<foundKey>();
                    HashSet<foundKey> toRemove = new HashSet<foundKey>();
                    foreach(foundKey key in potentialKeys) {
                        //If the key is too old, it can never be confirmed
                        if(key.index + 1000 < index) {
                            toRemove.Add(key);
                        }else if(key.repeatedCharacter == character) {
                            isConfirmed.Add(key);
                        }
                    }
                    //Remove all that were marked to be removed
                    foreach(foundKey key in toRemove) {
                        potentialKeys.Remove(key);
                    }

                    //Remove them from the potential keys and add them to the confirmed
                    foreach(foundKey key in isConfirmed) {
                        potentialKeys.Remove(key);
                        confirmedKeys.Add(key);
                    }
                    //This key is also a potential key
                    potentialKeys.Add(new foundKey(index, hash, character));
                } else if(Regex.IsMatch(hash, "(.)\\1{2,}")) {
                    //If it does not contain 5 but it contains 3
                    Match match2 = Regex.Match(hash, "(.)\\1{2,}");
                    potentialKeys.Add(new foundKey(index, hash, match2.ToString()[0]));
                }
                index++;
            }

            //We have found all the keys up to the key we are searching for,
            //but they may not be in order. Order them after the index we found them at.
            confirmedKeys.Sort((s1, s2) => s1.index.CompareTo(s2.index));
            return confirmedKeys[keyIndex-1].index;
        }


        public string CalculateHashWithStretch(string input, int stretchBy) {
            string toHash = input;
            int index = 0;
            while(index < stretchBy + 1) {
                toHash = CalculateMD5Hash(toHash);
                index++;
            }
            return toHash;
        }

        public string CalculateMD5Hash(string input) {
            //Calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            //We need the input in bytes
            byte[] toHash = Encoding.ASCII.GetBytes(input);
            //Get the hash
            byte[] hash = md5.ComputeHash(toHash);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();

        }
    }
}
