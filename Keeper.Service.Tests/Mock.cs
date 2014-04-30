using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keeper.Service.Tests
{
    public class MockStuff
    {
        enum Rank { White = 1, Blue = 2, Purple = 3, Brown = 4, Black = 5 };

        Random rand = new Random();
        public Competitor GenerateCompetitor()
        {
            Rank Belts;

            Competitor output = new Competitor()
            {
                Rank = Enum.Parse(typeof(Rank), rand.Next(1, 5).ToString()).ToString(),
                FirstName = getFirstName(),
                LastName = getLastName(),
                Team = getTeamName(),
                Weight = rand.Next(100, 270),
                IsMale = rand.Next(0, 9) > 3
            };
            return output;
        }


        public Stack<Competitor> getFakeCompetitors(int count)
        {
            var output = new Stack<Competitor>();

            MockStuff ms = new MockStuff();
            for (int i = 0; i < count; i++)
            {
                output.Push(ms.GenerateCompetitor());
            }
            return output;
        }

        public string getFirstName()
        {

            List<string> firstName = new List<string>()
                                         {
                                             "Kapow",
                                             "Bam Bam",
                                             "Conkie",
                                             "Jennifer",
                                             "Amsterdam",
                                             "Ratsie",
                                             "Lemar",
                                             "Webly",
                                             "Brian",
                                             "JamFace",
                                             "Barbara",
                                             "Paul",
                                             "Maggie",
                                             "Annie"
                                         };

            return firstName[rand.Next(0, firstName.Count() - 1)];
        }

        public string getLastName()
        {
            List<string> lastName = new List<string>()
                                         {
                                             "Walsh",
                                             "Bowles",
                                             "Walker",
                                             "Lyon",
                                             "Wrekk",
                                             "Lamar",
                                             "Kuo",
                                             "Edwards",
                                             "Walrath",
                                             "Johnson",
                                             "DeLooze",
                                             "Debruge",
                                             "LouLou",
                                             "Talleo"
                                         };

            return lastName[rand.Next(0, lastName.Count() - 1)];
        }

        public string getTeamName()
        {

            List<string> teamName = new List<string>()
                                         {
                                             "Rickson Gracie",
                                             "NW Martial Arts",
                                             "Impact Jiu Jitsu",
                                             "Straightblast Gym",
                                             "Ralph Gracie Portland",
                                             "Knuckle Up",
                                             "Team Lovato",
                                             "Ribiero Jiu Jitsu",
                                             "Brazillian Top Team",
                                             "Ralph Machio Jiu Jitsu",
                                             "Team Miyagi"
                                         };

            return teamName[rand.Next(0, teamName.Count() - 1)];
        }

    }
}
