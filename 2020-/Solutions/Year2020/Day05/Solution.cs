using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2020.Day05
{

    class Day05 : ASolution
    {

        public Day05() : base(05, 2020, "Binary Boarding")
        {
            // DebugInput = "FBFBBFFRLR";
        }

        protected override string SolvePartOne()
        {
            var tickets = Input.SplitByNewline();
            var highestId = 0;
            foreach (var ticket in tickets)
            {
                var rows = ticket.Substring(0, 7);
                var seats = ticket.Substring(7, 3);
                var id = GetRow(rows) * 8 + GetSeat(seats);
                if (id > highestId)
                    highestId = id;
            }
            
            return highestId.ToString();
        }

        protected override string SolvePartTwo()
        {
            var tickets = Input.SplitByNewline();
            var hashSet = new HashSet<int>();  

            var usedRows = new List<int>();
            foreach (var ticket in tickets)
            {
                var rows = ticket.Substring(0, 7);
                var seats = ticket.Substring(7, 3);
                var row = GetRow(rows);
                var seat = GetSeat(seats);

                var id = row * 8 + seat;
                usedRows.Add(row);
                hashSet.Add(id);
            }

            for (var i = usedRows.Min()+1; i < usedRows.Max()-1; i++)
            {
                for (var j = 0; j < 7; j++)
                {
                    if (hashSet.Contains(i*8+j)) continue;
                    return (i * 8 + j).ToString();
                }
            }
            
            return null;
        }

        private static int GetRow(string rows)
        {
            var min = 0;
            var max = 127;
            foreach (var row in rows)
            {
                var mid = (min + max) / 2;
                if (row.ToString() == "F")
                {
                    max = mid;
                }
                else
                {
                    min = mid;
                }
            }

            return max;
        }
        
        private static int GetSeat(string seats)
        {
            var min = 0;
            var max = 7;
            foreach (var seat in seats)
            {
                var mid = (min + max) / 2;

                if (seat.ToString() == "L")
                {
                    max = mid;
                }
                else
                {
                    min = mid;
                }
            }

            return max;
        }
    }
}
