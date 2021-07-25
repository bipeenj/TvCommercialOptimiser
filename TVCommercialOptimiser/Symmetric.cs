using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TVCommercialOptimiser
{
    public class Symmetric
    {
        public List<Commercial> TotalCommercials { get; set; }

        protected Dictionary<string, int> LastAssignedIndex { get; set; }
        protected Dictionary<int, int> LastBreakIndex { get; set; }
        public List<Break> TotalBreaks { get; set; }

        public virtual int GetBreakCount(int brkNo)
        {
            return 3;
        }
        public Symmetric()
        {
            InitialiseComercials();
            InitialiseBreakTYpes();
            LastAssignedIndex = new Dictionary<string, int>();
            LastAssignedIndex.Add("Automotive", -1);
            LastAssignedIndex.Add("Travel", -1);
            LastAssignedIndex.Add("Finance", -1);
            LastBreakIndex = new Dictionary<int, int>();
            LastBreakIndex.Add(1, 1);
            LastBreakIndex.Add(2, 4);
            LastBreakIndex.Add(3, 7);
        }
        protected virtual bool KeepGoing()
        {
            return TotalCommercials.Where(i => i.Rating == 0).Any();
        }
        public virtual void AssignBreaksToCommercials()
        {
            var toiterate = TotalBreaks.OrderByDescending(i => i.Rating);

            foreach (var brk in toiterate)
            {
                int counter = 0;
                while (KeepGoing())
                {
                    var brkNo = int.Parse(Regex.Match(brk.Name, @"\d+").Value);
                    var discontinue = TotalCommercials.Where(j => j.BreakName.Equals(brk.Name))?.Count() == GetBreakCount(brkNo);
                    if (discontinue)
                        break;
                    var toProcess = TotalCommercials.Where(i => i.Rating == 0 && i.Demographic.Equals(brk.Demographic));
                    var toassign = toProcess.Count() > counter ? toProcess.ElementAt(counter) : null;
                    if (toassign != null)
                    {
                        LastBreakIndex.TryGetValue(brkNo, out int indToassign);
                        LastAssignedIndex.TryGetValue(toassign.Type, out int index);
                        if (!index.Equals(indToassign - 1))
                        {
                            toassign.BreakName = brk.Name;
                            toassign.Rating = brk.Rating;
                            toassign.Index = indToassign;
                            LastAssignedIndex[toassign.Type] = indToassign;
                            LastBreakIndex[brkNo] = indToassign + 1;
                        }
                        else
                            break;
                    }
                    else
                        break;
                }
            }
            var tmp = TotalCommercials.Where(i => i.Type == "Finance" && i.BreakName == "Break2");
            if (tmp.Count() > 0)
            {
                var toswap = tmp.FirstOrDefault();
                var tochange = TotalCommercials.Where(i => i.Demographic == toswap.Demographic && i.Type != "Finance" && i.BreakName != "Break2")
                .OrderBy(j => j.Rating).FirstOrDefault();
                if (tochange != null)
                {
                    var brkname = tochange.BreakName;
                    var rating = tochange.Rating;
                    var ind = tochange.Index;
                    tochange.BreakName = toswap.BreakName;
                    tochange.Rating = toswap.Rating;
                    tochange.Index = toswap.Index;
                    toswap.BreakName = brkname;
                    toswap.Rating = rating;
                    toswap.Index = ind;
                }
                int lastbrk = 1;
                foreach (var brk in LastBreakIndex)
                {
                    string brkName = $"Break{brk.Key}";
                    for(int counter= lastbrk;counter+1< brk.Value;counter++)
                    {
                        var current = TotalCommercials.Where(i=> i.Index==counter).FirstOrDefault();
                        var next = TotalCommercials.Where(i => i.Index == counter+1).FirstOrDefault();
                        Commercial nextToNext = null;
                        if(counter+2<=brk.Value-1)
                            nextToNext = TotalCommercials.Where(i => i.Index == counter + 2).FirstOrDefault();
                        else
                            nextToNext = TotalCommercials.Where(i => i.Index == lastbrk).FirstOrDefault();
                        if(current!=null && next!=null && nextToNext!=null)
                        {
                            if(current.Type==next.Type)
                            {
                                var indToswap = current.Index;
                                current.Index = nextToNext.Index;
                                nextToNext.Index = indToswap;
                            }
                        }
                    }
                    lastbrk = brk.Value;
                }
            }


        }
        private void InitialiseBreakTYpes()
        {
            TotalBreaks = new List<Break>();
            TotalBreaks.Add(new Break
            {
                Name = "Break1",
                Demographic = "W25-30",
                Rating = 80
            });
            TotalBreaks.Add(new Break
            {
                Name = "Break1",
                Demographic = "M18-35",
                Rating = 100
            });
            TotalBreaks.Add(new Break
            {
                Name = "Break1",
                Demographic = "T18-40",
                Rating = 250
            });
            TotalBreaks.Add(new Break
            {
                Name = "Break2",
                Demographic = "W25-30",
                Rating = 50
            });
            TotalBreaks.Add(new Break
            {
                Name = "Break2",
                Demographic = "M18-35",
                Rating = 120
            });
            TotalBreaks.Add(new Break
            {
                Name = "Break2",
                Demographic = "T18-40",
                Rating = 200
            });
            TotalBreaks.Add(new Break
            {
                Name = "Break3",
                Demographic = "W25-30",
                Rating = 350
            });
            TotalBreaks.Add(new Break
            {
                Name = "Break3",
                Demographic = "M18-35",
                Rating = 150
            });
            TotalBreaks.Add(new Break
            {
                Name = "Break3",
                Demographic = "T18-40",
                Rating = 500
            });

        }
        private void InitialiseComercials()
        {
            TotalCommercials = new List<Commercial>();
            TotalCommercials.Add(new Commercial
            {
                CommercialName = "Commercial 1",
                Type = "Automotive",
                Demographic = "W25-30"

            });
            TotalCommercials.Add(new Commercial
            {
                CommercialName = "Commercial 2",
                Type = "Travel",
                Demographic = "M18-35"

            });
            TotalCommercials.Add(new Commercial
            {
                CommercialName = "Commercial 3",
                Type = "Travel",
                Demographic = "T18-40"

            });
            TotalCommercials.Add(new Commercial
            {
                CommercialName = "Commercial 4",
                Type = "Automotive",
                Demographic = "M18-35"

            });
            TotalCommercials.Add(new Commercial
            {
                CommercialName = "Commercial 5",
                Type = "Automotive",
                Demographic = "M18-35"

            });
            TotalCommercials.Add(new Commercial
            {
                CommercialName = "Commercial 6",
                Type = "Finance",
                Demographic = "W25-30"

            });
            TotalCommercials.Add(new Commercial
            {
                CommercialName = "Commercial 7",
                Type = "Finance",
                Demographic = "M18-35"

            });
            TotalCommercials.Add(new Commercial
            {
                CommercialName = "Commercial 8",
                Type = "Automotive",
                Demographic = "T18-40"

            });
            TotalCommercials.Add(new Commercial
            {
                CommercialName = "Commercial 9",
                Type = "Travel",
                Demographic = "W25-30"

            });
        }
    }
}
