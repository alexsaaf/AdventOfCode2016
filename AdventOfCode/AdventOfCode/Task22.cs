using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task22 {

        public struct Node {
            public string fileSys;
            public int size;
            public int used;
            public int avail;

            public Node(string _fileSys, int _size, int _used, int _avail) {
                fileSys = _fileSys;
                size = _size;
                used = _used;
                avail = _avail;
            }
        }

        List<Node> nodes;

        public int CalculateViablePairs(string[] disks) {
            nodes = new List<Node>();
            foreach (string disk in disks) {
                //Remove the extra spaces
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);
                string filteredDisk = regex.Replace(disk, " ");

                string[] components = filteredDisk.Split(' ');
                //Ignore lines that are not disks
                if (components.Count() < 5 || components[0].Equals("Filesystem")) {
                    continue;
                } else {
                    //We use remove to remove the T on all the sizes
                    Node newDisk = new Node(components[0], int.Parse(components[1].Remove(components[1].Length - 1)),
                        int.Parse(components[2].Remove(components[2].Length - 1)), int.Parse(components[3].Remove(components[3].Length - 1)));
                    nodes.Add(newDisk);
                }
            }
            //Sort the list based on the avail
            nodes = nodes.OrderBy((s => s.avail)).ToList();

            int viablePairs = 0;
            Node currentNode;
            for(int i = 0; i < nodes.Count(); i++){
                currentNode = nodes.ElementAt(i);
                if(currentNode.used == 0){
                    continue;
                }
                int requiredAvail = currentNode.used;
                //Find the index of the first node with avail big enough. 
                //If the required avail is larger than our own avail, we can start at our own index
                int index = FindLowestIndexWithAvail(requiredAvail, (requiredAvail > currentNode.avail)? i : 0);
                //All disks with a higher index can be coupled to this one
                int viablePairsForNode = nodes.Count() - index;
                //We do not want to count ourselves
                if(i > index){
                    viablePairsForNode--;
                }

                if(viablePairsForNode < 0){
                    viablePairsForNode = 0;
                }

                viablePairs += viablePairsForNode;
            }
            return viablePairs;
        }

        int FindLowestIndexWithAvail(int avail, int startIndex = 0) {
            for (int i = startIndex; i < nodes.Count(); i++) {
                if (nodes.ElementAt(i).avail > avail) {
                    return i;
                }
            }
            return 0;
        }
    }
}
