using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    class Task20 {

        List<Span> blockedSpans;

        public long FindLowestOkIP(string[] blockedList) {
            BuildSpans(blockedList);

            //Find the lowest
            for (long i = 0; i <= 4294967295; i++) {
                if (IpOk(ref i)) {
                    return i;
                }
            }
            return 0;
        }


        public long CountOpenIPs(string[] blockedList) {
            BuildSpans(blockedList);

            //Count the open ones
            long count = 0;
            for (long i = 0; i <= 4294967295; i++) {
                if (IpOk(ref i)) {
                    count++;
                }
            }
            return count;
        }


        private void BuildSpans(string[] blockedList) {
            blockedSpans = new List<Span>();
            foreach (string block in blockedList) {
                string[] splitString = block.Split('-');
                //Assuming that all strings actually hold numbers and nothing else
                long startIP = Int64.Parse(splitString[0]);
                long endIP = Int64.Parse(splitString[1]);
                blockedSpans.Add(new Span(startIP, endIP));
            }
        }

        bool IpOk(ref long ip) {
            bool ipOk = true;
            foreach (Span span in blockedSpans) {
                if (span.startIP <= ip && span.endIP >= ip) {
                    ip = span.endIP;
                    ipOk = false;
                }
            }
            return ipOk;
        }
    }

    struct Span{
        public long startIP, endIP;

        public Span(long _startIP, long _endIP) {
            startIP = _startIP;
            endIP = _endIP;
        }
    }
}
