using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode {
    class Task7 {


        public int CountTLSSupporting(string[] adresses) {
            int result = 0;

            foreach (string adress in adresses) {
                if (SupportsTLS(adress)) {
                    result++;
                }
            }
            return result;
        }

        bool SupportsTLS(string adress) {
            string pattern = "\\[[a-zA-Z]*\\]";
            string replacement = " ";
            Regex rgx = new Regex(pattern);

            List<string> hypernets = new List<string>();
            var match = Regex.Match(adress, pattern);
            while (match.Success) {
                hypernets.Add(match.ToString());
                match = match.NextMatch();
            }

            foreach(string hypernet in hypernets.ToArray()){
                Console.WriteLine(hypernet);
                if(ContainsABBA(hypernet)){
                    return false;
                }
            }

            string[] noHypernet = rgx.Replace(adress, replacement).Split(' ');
            
            foreach (string word in noHypernet) {
                if(ContainsABBA(word)){
                    return true;
                }
            }

            return false;
        }

        bool ContainsABBA(string word) {
            for (int i = 0; i < word.Length - 3; i++) {
                if (word[i] == word[i + 3] && word[i + 1] == word[i + 2] && word[i] != word[i + 1]) {
                    Console.WriteLine("Does support TLS. " + word);
                    return true;
                }
            }
            return false;
        }
    }
}
