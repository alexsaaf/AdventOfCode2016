using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task5 {


        public string FindEasyPassword(string doorID) {
            string password = "";
            int index = 0;
            //Loop until we find the entire password
            while (password.Length < 8) {
                string hash = CalculateMD5Hash(doorID + index.ToString());
                if (IsIndicating(hash)) {
                    password += hash[5];
                }
                index++;
            }
            return password;
        }

        public string FindAdvancedPassword(string doorID) {
            string password = "........";
            StringBuilder stringBuilder = new StringBuilder(password);
            int index = 0;
            int foundLetters = 0;
            //Loop until we find the entire password
            while (foundLetters < 8) {
                string hash = CalculateMD5Hash(doorID + index.ToString());
                if (IsIndicating(hash)) {
                    int pos;
                    if (Int32.TryParse(hash[5].ToString(), out pos)) {
                        if (pos >= 0 && pos < 8 && stringBuilder[pos] == '.') {
                            stringBuilder.Remove(pos, 1);
                            stringBuilder.Insert(pos, hash[6]);
                            foundLetters++;
                            Console.WriteLine("Appending " + hash[6] + " . Hash is: " + hash);
                            Console.WriteLine("String is: " + stringBuilder.ToString() + " Foundletters: " + foundLetters);
                        }
                    }
                }
                index++;
            }
            return stringBuilder.ToString();
        }

        public bool IsIndicating(string hash) {
            Regex pattern = new Regex("^00000.*");
            if (pattern.IsMatch(hash)) {
                return true;
            }
            return false;
        }

        public string CalculateMD5Hash(string input) {
            //Calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            //We need the input in bytes
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            //Get the hash
            byte[] hash = md5.ComputeHash(inputBytes);

            //Return hash as hexadecimal string
            return BitConverter.ToString(hash).Replace("-", "");

        }

    }
}
