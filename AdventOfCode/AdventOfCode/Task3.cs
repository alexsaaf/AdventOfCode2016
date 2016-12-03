using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task3 {

        public int CheckTriangles(string[] triangles) {
            //Keep count of the OK triangles
            int realTriangles = 0;

            foreach (string triangle in triangles) {
                //Split on two spaces and filter out empty elements
                string[] lengthStrings = Regex.Split(triangle, "  ").Where(x => !string.IsNullOrEmpty(x)).ToArray();
                int[] lengths = Array.ConvertAll(lengthStrings, s => Int32.Parse(s));

                //If the lengths break the rules for a triangle, dont count it
                if (IsTriangle(lengths[0], lengths[1], lengths[2])) {
                    realTriangles++;
                }
            }
            return realTriangles;
        }

        //Checks if the given lengths are a valid triangle
        public bool IsTriangle(int a, int b, int c) {
            return (a < b + c && b < a + c && c < b + a);
        }
    }
}
