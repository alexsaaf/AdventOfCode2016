using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task13 {

        Dictionary<Tuple<int, int>, char> positions;

        public int FindPosition(int x, int y, int number, bool taskB) {
            Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
            HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>();

            Tuple<int,int> targetPosition = new Tuple<int,int>(x,y);

            Tuple<int,int> startPosition = new Tuple<int,int>(1,1);
            queue.Enqueue(startPosition);
            queue.Enqueue(null);
            Dictionary<Tuple<int, int>, Tuple<int, int>> pathKeeper = new Dictionary<Tuple<int, int>, Tuple<int, int>>();

            Tuple<int, int> element = startPosition;
            visited.Add(element);

            int depth = 0;

            while (queue.Count > 0) {
                element = queue.Dequeue();
                //Using null marker after every level of depth to keep count of the depth
                if (element == null) {
                    depth++;
                    queue.Enqueue(null);
                    if (depth == 49 && taskB) {
                        return visited.Distinct().Count();
                    }
                    element = queue.Dequeue();
                }

                //If we are at the target position, we are done
                if(element.Item1 == x && element.Item2 == y && !taskB){
                    break;
                }

                for (int i = -1; i < 2; i = i + 2) {
                    if (element.Item1 + i >= 0) {

                        Tuple<int, int> newElement = new Tuple<int, int>(element.Item1 + i, element.Item2);
                        //Check if the new element is ok
                        bool isWall = IsWall(newElement, number);
                        if (!isWall && !visited.Contains(newElement)) {
                            queue.Enqueue(newElement);
                            pathKeeper.Add(newElement, element);
                            visited.Add(newElement);
                        }
                    }
                }

                for (int i = -1; i < 2; i = i + 2) {
                    if (element.Item2 + i >= 0) {

                        Tuple<int, int> newElement = new Tuple<int, int>(element.Item1, element.Item2 + i);
                        //Check if the new element is ok
                        bool isWall = IsWall(newElement, number);
                        if (!isWall && !visited.Contains(newElement)) {
                            queue.Enqueue(newElement);
                            pathKeeper.Add(newElement, element);
                            visited.Add(newElement);
                        }
                    }
                }

            }

            List<Tuple<int, int>> pathBack = BuildPathBack(element, pathKeeper);

            return pathBack.Count;
        }

        List<Tuple<int, int>> BuildPathBack(Tuple<int, int> endPos, Dictionary<Tuple<int, int>, Tuple<int, int>> pathKeeper) {
            List<Tuple<int, int>> pathBack = new List<Tuple<int, int>>();
            Tuple<int, int> element = endPos;
            while (!(element.Item1 == 1) && !(element.Item2 == 1)) {
                pathBack.Add(element);
                element = pathKeeper[element];
            }
            pathBack.Add(element);
            return pathBack;
        }

        public bool IsWall(Tuple<int, int> position, int number) {
            int x = position.Item1;
            int y = position.Item2;
            int value = x * x + 3 * x + 2 * x * y + y + y * y;
            value += number;

            string binaryNumber = Convert.ToString(value, 2);

            int count = 0;
            foreach(char digit in binaryNumber){
                if(digit == '1'){
                    count++;
                }
            }

            if (count % 2 == 0) {
                return false;
            }
            return true;
        }
    }
}
