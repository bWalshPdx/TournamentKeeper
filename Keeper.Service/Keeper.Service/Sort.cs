using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keeper.Service
{
    public class Sort
    {
        public List<Bracket> InitalSort(Stack<Competitor> competitors)
        {
            //TODO: Write this part
            //Input a Stack<competitors>
            //Output a IEnumerable<Brackets>
            //Brackets are List<Bouts>

            var genderGroups = seperateGender(competitors);




            return new List<Bracket>();
        }

        public List<Bout> NextSort(Stack<Competitor> competitors)
        {
            //by Teams   
            return new List<Bout>();
        }

        public IEnumerable<Stack<Competitor>> seperateGender(Stack<Competitor> competitors)
        {
            Stack<Competitor> sortedComps = new Stack<Competitor>(competitors.OrderByDescending(a => a.IsMale).ToList());
            bool currentSex = sortedComps.First().IsMale;
            
            Stack<Competitor> current = new Stack<Competitor>();

            while (sortedComps.Count > 0)
            {
                //TODO: Breaking due to empty stack:
                if (sortedComps.Count > 0 && currentSex == sortedComps.Peek().IsMale)
                {
                    current.Push(sortedComps.Pop());
                }
                else
                {
                    yield return current;
                    current = new Stack<Competitor>();
                    currentSex = !currentSex;
                }
            }

            yield return current;
        }


        public IEnumerable<Stack<Competitor>> seperateWeight(Stack<Competitor> competitors, List<double> weightBrackets)
        {
            //Order competitors in ascending order:
            Stack<Competitor> sortedComps = new Stack<Competitor>(competitors.OrderByDescending(a => a.Weight).ToList());

            //pinch off list at the top of each bracket
            Stack<Competitor> current = new Stack<Competitor>();

            foreach (var weightBracket in weightBrackets)
            {
                current = new Stack<Competitor>();

                while (true)
                {
                    if (sortedComps.Peek().Weight < weightBracket && competitors.Count > 0)
                    {
                        current.Push(sortedComps.Pop());
                    }
                    else
                    {
                        yield return current;
                        break;
                    }
                }
            }

            current = new Stack<Competitor>();
            while (sortedComps.Count > 0)
                current.Push(sortedComps.Pop());

            yield return current;
        }

        public IEnumerable<Stack<Competitor>> seperateRank(Stack<Competitor> competitors)
        {
            var rankGroups = competitors.GroupBy(c => c.Rank);

            foreach (var rankGroup in rankGroups)
            {
                yield return new Stack<Competitor>(rankGroup.ToList());
            }
        }

        public IEnumerable<Stack<Competitor>> seperateTeams(Stack<Competitor> competitors)
        {
            //Seperate teams in merge sort style, teams are grouped, pull each team from the "head" of each group:
            var teamGroups = competitors.GroupBy(c => c.Team);

            foreach (var team in teamGroups)
            {
                yield return new Stack<Competitor>(team.ToList());
            }
        }
    }

    public class Bracket
    {
        public double Weight;
        public bool Gender;
        public string Rank;
        public List<Bout> Bouts;
    }

    public class Bout
    {
        public List<CombantantInfo> Combatants;
        public int TimeOfBout;
        public CombantantInfo Winner;
        public Timer Timer;

        public Bout(IEnumerable<Competitor> competitors)
        {
            Combatants = competitors.Select(c => (CombantantInfo)c).ToList();
        }
    }

    public class Competitor
    {
        public string Team;
        public string FirstName;
        public string LastName;
        public string Rank;
        public int Weight;
        public bool IsMale;

    }

    public class Team
    {
        public string Name;
        public int TeamId;


    }

    //TODO: Add all the submissions:
    public enum Submission {Armbar, Kneebar, AnkleLock, Choke, Kimuara, WristLock}

    public class CombantantInfo : Competitor
    {
        public int Points;
        public bool isGreenBelt;
        public bool? IsWinner;
        public Submission? WinningSubmission;
    }

}
