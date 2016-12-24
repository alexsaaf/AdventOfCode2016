using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task19 {

        /// <summary>
        /// Using trick from numberphile video about josephus problem
        /// </summary>
        /// <param name="nrOfElves"></param>
        /// <returns></returns>
        public int Winner(int nrOfElves) {
            string nBinary = Convert.ToString(nrOfElves, 2);
            string winnerBinary = nBinary.Substring(1) + nBinary[0];
            return Convert.ToInt32(winnerBinary, 2);
        }
    }
}
