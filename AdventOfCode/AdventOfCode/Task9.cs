using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task9 {

        public long CalculateSize(string currentString, int start, int length, bool partB) {
            long size = 0;
            for(int i = start; i < start + length;) {
                if(currentString[i] == '(') {
                    StringBuilder mark = new StringBuilder();
                    i++;    //Advance i past the '('
                    while(currentString[i] != ')') {
                        mark.Append(currentString[i]);
                        i++;
                    }
                    i++;    //Advance i past the ')'
                    string[] markerComponents = mark.ToString().Split('x');   //Get the marker
                    //Parse the parts of the marker
                    int len = int.Parse(markerComponents[0]);
                    int reps = int.Parse(markerComponents[1]); 
                    size += reps * (partB ? CalculateSize(currentString, i, len, true) : len);  //Add the length, either recursively or not
                    i += len;
                }else {
                    size++;
                    i++;
                }
            }
            return size;
        }

    }
}
