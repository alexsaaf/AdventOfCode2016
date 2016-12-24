using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task18 {

        public int CountSafeTiles(string firstRow, int numberOfRows) {
            int rowSize = firstRow.Length;
            int safeTiles = 0;
            List<char[]> rows = new List<char[]>();

            //Add the first row to both the rows list and the count
            rows.Add(firstRow.ToArray());
            safeTiles += firstRow.Count(x => x == '.');

            //Determine the next row as long as the 
            while (rows.Count < numberOfRows) {
                char[] newRow = new Char[rowSize];
                for (int i = 0; i < newRow.Length; i++) {
                    if (IsTrap(rows, rows.Count, i)) {
                        newRow[i] = '^';
                    } else {
                        newRow[i] = '.';
                    }
                }
                rows.Add(newRow);
                safeTiles += newRow.Count(x => x == '.');
            }
            Console.WriteLine(rowSize);
            /*foreach (char[] row in rows) {
                foreach (char character in row) {
                    Console.Write(character);
                }
                Console.WriteLine();
            }*/
            return safeTiles;
        }

        bool IsTrap(List<char[]> rows, int row, int index) {
            char[] previousRow = rows[row - 1];
            
            char left = '.';
            if(index != 0){
                left = previousRow[index - 1];
            }

            char middle = previousRow[index];

            char right = '.';
            if(index != previousRow.Length - 1){
                right = previousRow[index + 1];
            }

            if (left == '^' && middle == '^' && right != '^')
                return true;

            if (left != '^' && middle == '^' && right == '^')
                return true;

            if (left == '^' && middle != '^' && right != '^')
                return true;

            if (left != '^' && middle != '^' && right == '^')
                return true;

            return false;

        }
    
    
    
    
    
    
    
    }
}