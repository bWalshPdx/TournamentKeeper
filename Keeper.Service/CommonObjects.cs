using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Keeper.Service
{
    using System.Collections;

    public class WeightDivision
    {
        public WeightDivision(string name, double loweestWeight, double heighestWeight)
        {
            Name = name;
            LowestWeight = loweestWeight;
            HighetstWeight = heighestWeight;
        }

        public string Name;
        public double LowestWeight;
        public double HighetstWeight;
    }

    public class Team
    {
        public string Name;
        public int TeamId;
    }

    //TODO: Add all the submissions:
    public enum Submission { Armbar, Kneebar, AnkleLock, Choke, Kimuara, WristLock }


    public class Competitor
    {
        public string Team;
        public string FirstName;
        public string LastName;
        public string Rank;
        public double Weight;
        public bool IsMale;
    }

    //TODO: Division should get two empty trees with the matches as their leaves:
    public class Division
    {
        Tournament t = new Tournament();


        public WeightDivision WeightDivision;
        public bool isMale;
        public string Rank;
        public Bracket bracket;

        public Division(List<Competitor> competitors)
        {
            Stack<Competitor> compStack = new Stack<Competitor>(competitors);

            bracket = new Bracket(compStack);
        }
    }

    public class Match
    {
        public List<CompetitorResult> Combatants = new List<CompetitorResult>();
        public int ElapsedTime;
        public CompetitorResult Winner;
        public Timer Timer;

        public Match(IEnumerable<Competitor> competitors)
        {
            foreach (var competitor in competitors)
                Combatants.Add(new CompetitorResult(competitor));

            AssignGreenBelt();
        }

        public void AssignGreenBelt()
        {
            Random r = new Random();
            Combatants[r.Next(0, 1)].isGreenBelt = true;
        }

    }

    public class CompetitorResult
    {
        public Competitor Competitor;
        public int Points;
        public bool isGreenBelt;
        public bool? IsWinner;
        public Submission? WinningSubmission;

        public CompetitorResult(Competitor competitor)
        {
            Competitor = competitor;
        }
    }

    
    public class Bracket
    {
        public Match match;
        public Bracket Right;
        public Bracket Left;

        public Bracket(Stack<Competitor> competitors)
        {
            //Push the competitors down the tree:
            if (competitors.Count() > 3)
            {
                Stack<Competitor> compRight = new Stack<Competitor>();

                for (int i = 0; i < competitors.Count() / 2; i++)
                {
                    compRight.Push(competitors.Pop());
                }

                Right = new Bracket(compRight);
                Left = new Bracket(competitors);
            }
            else
            {
                //Make new match:
                match = new Match(competitors);
            }
        }
    }

    
    


    public class CommonObjects
    {

        //TODO: Add all the submissions:
        public enum Submission { Armbar, Kneebar, AnkleLock, Choke, Kimuara, WristLock }


    }
}
