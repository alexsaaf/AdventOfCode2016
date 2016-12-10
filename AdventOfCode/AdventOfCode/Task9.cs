using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task9 {

        public int decompressedLength = 0;
        public List<string> decompressedLines;

        public void DecompressFile(string[] lines) {
            decompressedLines = new List<string>();

            string pattern = @"\(\d+x\d+\)";
            Regex regex = new Regex(pattern);

            Console.WriteLine(regex.Match("Hej").Success);
            Console.WriteLine(regex.Match("abc(10x5)aa").Success);

            decompressedLength = 0;

            foreach(string line in lines){
                string decompressedLine = "";
                int indexReadTo = 0;
                Match match = regex.Match(line.Substring(indexReadTo));
                while(match.Success){
                    //First append the characters up to the match
                    int readLength = match.Index;
                    decompressedLine += line.Substring(indexReadTo, readLength);

                    //Now handle the marker
                    int markerLength = match.ToString().Length;
                    string[] markerNumbers = match.ToString().Substring(1, match.ToString().Length - 2).Split('x');

                    string stringToRepeat = line.Substring(match.Index + markerLength, Int32.Parse(markerNumbers[0]));

                    for (int i = 0; i < Int32.Parse(markerNumbers[1]); i++) {
                        decompressedLine += stringToRepeat;
                    }
                    indexReadTo = indexReadTo + match.Index + markerLength + Int32.Parse(markerNumbers[0]);
                    match = regex.Match(line.Substring(indexReadTo));
                }
                decompressedLength += decompressedLine.Length;
                decompressedLines.Add(decompressedLine);
            }



        }

    }
}
