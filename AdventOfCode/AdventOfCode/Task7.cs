using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode {
    class Task7 {

        //Count the adresses supporting TLS using SupportsTLS() method
        public int CountTLSSupporting(string[] adresses) {
            int result = 0;

            foreach (string adress in adresses) {
                if (SupportsTLS(adress)) {
                    result++;
                }
            }
            return result;
        }

        //Count the adresses supporting SSL using SupportsSSL() method
        public int CountSSLSupporting(string[] adresses) {
            int result = 0;

            foreach (string adress in adresses) {
                if (SupportsSSL(adress)) {
                    result++;
                }
            }
            return result;
        }

        //Lists for keeping found XYX substrings
        List<string> foundBABs;
        List<string> foundABAs;

        bool SupportsSSL(string adress) {
            string[] hypernets = GetHypernetsFromString(adress);

            foundBABs = new List<string>();

            //Find all the XYXs in the hypernets
            foreach (string hypernet in hypernets) {
                FindKeywords(hypernet, true);
            }

            //Find all the XYXs in the hypernets
            string[] noHypernets = FilterHypernets(adress);
            foundABAs = new List<string>();
            foreach(string word in noHypernets) {
                FindKeywords(word, false);
            }

            //Check for BAB which fits to a ABA. If there is one, this adress supports ssl
            foreach(string ABA in foundABAs) {
                string correspondingBAB = "" + ABA[1] + ABA[0] + ABA[1];
                if (foundBABs.Contains(correspondingBAB)) {
                    return true;
                }
            }
            return false;
        }

        //Finds any substring of shape XYX and adds it to the list specified by findBAB
        void FindKeywords(string word, bool findBAB) {
            for (int i = 0; i < word.Length - 2; i++) {
                if (word[i] == word[i+2] && word[i] != word[i+1]) {
                    if (findBAB) {
                        foundBABs.Add("" + word[i] + word[i + 1] + word[i + 2]);
                    }else {
                        foundABAs.Add("" + word[i] + word[i + 1] + word[i + 2]);
                    }
                }
            }
        }

        //Checks if given adress supports TLS
        bool SupportsTLS(string adress) {
            //Get all the hypernets
            string[] hypernets = GetHypernetsFromString(adress);
            
            //If there is an ABBA in hypernets, we do not support TLS
            foreach(string hypernet in hypernets.ToArray()){
                Console.WriteLine(hypernet);
                if(ContainsABBA(hypernet)){
                    return false;
                }
            }

            //Get everything which is not a hypernet
            string[] noHypernet = FilterHypernets(adress);
            
            //If there is an ABBA here, we support TLS
            foreach (string word in noHypernet) {
                if(ContainsABBA(word)){
                    return true;
                }
            }
            //No ABBA found, no TLS support
            return false;
        }

        //Checks if word contains substring of shape XYYX
        bool ContainsABBA(string word) {
            for (int i = 0; i < word.Length - 3; i++) {
                if (word[i] == word[i + 3] && word[i + 1] == word[i + 2] && word[i] != word[i + 1]) {
                    Console.WriteLine("Does support TLS. " + word);
                    return true;
                }
            }
            return false;
        }
        
        //Gets all the hypernets ("[...]") from the adress
        string[] GetHypernetsFromString(string adress) {
            string pattern = "\\[[a-zA-Z]*\\]";
            Regex rgx = new Regex(pattern);

            List<string> hypernets = new List<string>();
            var match = Regex.Match(adress, pattern);
            while (match.Success) {
                hypernets.Add(match.ToString());
                match = match.NextMatch();
            }
            return hypernets.ToArray();
        }

        //Gets everything which isn't a hypernet from the adress
        string[] FilterHypernets(string adress) {
            string pattern = "\\[[a-zA-Z]*\\]";
            string replacement = " ";
            Regex rgx = new Regex(pattern);
            return rgx.Replace(adress, replacement).Split(' ');
        }
    }
}
