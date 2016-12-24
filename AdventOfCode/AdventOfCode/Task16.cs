using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task16 {

        public string FillDisk(string input, int diskSize) {
            string disk = input;

            //First, we fill the disk
            while (disk.Length < diskSize) {
                string a = disk;
                char[] charArray = a.ToArray();
                Array.Reverse(charArray);
                string tmp = new string(charArray);
                StringBuilder sb = new StringBuilder();
                foreach (char character in tmp) {
                    if (character == '1') {
                        sb.Append("0");
                    } else {
                        sb.Append("1");
                    }
                }
                disk = a + "0" + sb.ToString();
            }

            //Then, get the checksum which we will return
            string checkSum = GetCheckSum(disk.Substring(0, diskSize));
            while (checkSum.Length % 2 == 0) {
                checkSum = GetCheckSum(checkSum);
            }

            return checkSum;
        }

        string GetCheckSum(string input) {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < input.Length; i += 2) {
                if (input[i] == input[i + 1]) {
                    result.Append("1");
                } else {
                    result.Append("0");
                }
            }
            return result.ToString();
        }
    }
}
