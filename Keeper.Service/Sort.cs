using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Keeper.Service
{
    public class Sort
    {
        //private List<WeightDivision> _weightBrackets;
        
        public IEnumerable<IEnumerable<Competitor>> InitalSort(Stack<Competitor> competitors, List<WeightDivision> weightDivisions)
        {
            Func<IEnumerable<Competitor>, IEnumerable<Stack<Competitor>>> sortByGender = c => c.GroupBy(comp => comp.IsMale).Select(a => new Stack<Competitor>(a.ToList()));
            Func<IEnumerable<Competitor>, IEnumerable<Stack<Competitor>>> sortByWeight = c => this.seperateWeight(new Stack<Competitor>(c.ToList()), weightDivisions);
            Func<IEnumerable<Competitor>, IEnumerable<Stack<Competitor>>> sortByRank = c => c.GroupBy(comp => comp.Rank).Select(a => new Stack<Competitor>(a.ToList()));

            Stack<Func<IEnumerable<Competitor>, IEnumerable<Stack<Competitor>>>> initialSort = new Stack<Func<IEnumerable<Competitor>, IEnumerable<Stack<Competitor>>>>();

            initialSort.Push(sortByGender);
            initialSort.Push(sortByWeight);
            initialSort.Push(sortByRank);

            return Seperate(competitors, initialSort);
        }

        public List<Competitor> NextSort(Stack<Competitor> competitors)
        {
            //by Teams
            //I may have to "hard code" the brackets so we can take into account sequencing winner of (1 and 2) => 1a. (3 and 4) => 2a
            return new List<Competitor>();
        }

        public List<Stack<Competitor>> seperateWeight(Stack<Competitor> competitors, List<WeightDivision> weightDivision)
        {
            List<Stack<Competitor>> brackets = new List<Stack<Competitor>>();

            for (int i = 0; i < weightDivision.Count; i++)
                brackets.Add(new Stack<Competitor>());

            while (0 < competitors.Count())
            {
                Competitor currentComp = competitors.Pop();

                for (int i = 0; i < weightDivision.Count; i++)
                {
                    var test1 = weightDivision[i].LowestWeight < currentComp.Weight;
                    var test2 = currentComp.Weight <= weightDivision[i].HighetstWeight;

                    if (currentComp != null && test1 && test2)
                    {
                        brackets[i].Push(currentComp);
                        currentComp = null;
                        break;
                    }    
                }

                if (currentComp != null)
                    throw new Exception("The compititor did not get slotted into a weight bracket");
            }

            return brackets;
        }

        public IEnumerable<IEnumerable<Competitor>> Seperate(IEnumerable<Competitor> competitors, Stack<Func<IEnumerable<Competitor>, IEnumerable<Stack<Competitor>>>> sortCollection)
        {
            if (sortCollection.Count == 0)
            {
                yield return competitors;
            }
            else
            {
                var currentSort = sortCollection.Pop();

                foreach (var compBranch in currentSort(competitors))
                {
                    var divisions = Seperate(compBranch, sortCollection);

                    foreach (var division in divisions)
                        yield return division;
                }
            }
        }
    }

}
