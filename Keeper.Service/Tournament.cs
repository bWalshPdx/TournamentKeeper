using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Keeper.Service
{
    public class Tournament
    {
        public List<WeightDivision> _WeightDivisions = new List<WeightDivision>();

        public Tournament()
        {
            
        }
        
        public Tournament(List<WeightDivision> weightDivisions)
        {
            _WeightDivisions = weightDivisions;
        }

        public WeightDivision GetWeightDivision(IEnumerable<Competitor> _competitors)
        {
            throw new Exception("input weight not found in current divisions");
        }


        //TODO: <NA> Create tree "Bracket" by puhsing down the tree until we get 2 or less competitors
        public class Bracket
        {
            public Match Match;
            public Bracket Right;
            public Bracket Left;

            public Bracket(Stack<Competitor> competitors)
            {
                //Push the competitors down the tree:
                if (competitors.Count() > 2)
                {
                    Stack<Competitor> compRight = new Stack<Competitor>();

                    for (int i = 0; i < competitors.Count() / 2; i++)
                    {
                        compRight.Push(competitors.Pop());
                    }

                    Right = new Bracket(compRight);
                    Left = new Bracket(competitors);
                }
                else if (competitors.Count() == 0)
                    return;

                Match = new Match(competitors);
            }
        }
    }
}
