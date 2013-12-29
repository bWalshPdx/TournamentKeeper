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
        public List<Bout> InitalSort(Stack<Competitor> competitors)
        {
            //by Wieght then teams
            return new List<Bout>();
        }

        public List<Bout> NextSort(Stack<Competitor> competitors)
        {
            //by Teams   
            return new List<Bout>();
        }

        //http://www.londoninternationalbjj.com/files/65971007.jpg
        //TODO: Converting from list to stack is jumbling the sort:
        public IEnumerable<Stack<Competitor>> seperateWeight(Stack<Competitor> competitors, List<double> weightBrackets)
        {
            //Order competitors in ascending order:
            Stack<Competitor> sortedComps = new Stack<Competitor>(competitors.OrderByDescending(a => a.Weight).ToList());

            //TODO: Order the stack without the dumb temp list:
            
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

        public List<Bout> seperateTeams(Stack<Competitor> competitors)
        {
            //Seperate teams in merge sort style, teams are grouped, pull each team from the "head" of each group.
            return new List<Bout>();

        }
    }

    public class Bout
    {
        public List<CombantantInfo> Combabtants;
        public int TimeOfBout;
        public CombantantInfo Winner;
        public Timer Timer;



        public Bout(IEnumerable<Competitor> competitors)
        {
            Combabtants = competitors.Select(c => (CombantantInfo) c).ToList();
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
