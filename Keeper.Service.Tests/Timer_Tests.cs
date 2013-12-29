using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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
                                        Weight = rand.Next(100,270),
                                        IsMale = rand.Next(0,9) < 3
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

    [TestFixture]
    class Timer_Tests
    {
        [Test]
        public void getCompetitors_VerifyOutput()
        {
            

            MockStuff ms = new MockStuff();
            for (int i = 0; i < 100; i++)
            {
                var comp = ms.GenerateCompetitor();
                string output = @"Rank: {0}
FirstName = {1},
LastName = {2},
Team = {3},
Weight = {4},
Gender = {5}
";
                Console.WriteLine(output,comp.Rank,comp.FirstName, comp.LastName,comp.Team, comp.Weight, comp.IsMale == true ? "Male": "Female");
                
            }
        }


        [Test]
        //TODO: Rewrite this test to document why it is failing:
        public void sortByWeight_InitialTest()
        {
            int numberOfCompetitors = 1000;
            MockStuff ms = new MockStuff();
            Sort s = new Sort();
            var fakeCompetitors = ms.getFakeCompetitors(numberOfCompetitors);
            
            List<double> weightBrackets = new List<double>()
                                           {
                                               125.5,
                                               141,
                                               154,
                                               167.5,
                                               181,
                                               194.5,
                                               207.5,
                                               221
                                           };

            var output = s.seperateWeight(fakeCompetitors, weightBrackets).ToList();


            for (int i = 0; i <= weightBrackets.Count(); i++)
            {
                if (i == 0)
                {
                    Console.WriteLine("----------0lbs - {0}lbs---------", weightBrackets[i]);
                }
                else if (i == weightBrackets.Count)
                {
                    Console.WriteLine("----------{0}lbs - Infinity -----------", weightBrackets[i - 1]);
                }
                else
                {
                    Console.WriteLine("----------{0}lbs - {1}lbs---------", weightBrackets[i - 1], weightBrackets[i]);
                }

                

                foreach (var comp in output[i])
                {
                    Console.WriteLine("Weight: {0}", comp.Weight);
                    if (i == 0)
                    {
                        Assert.IsTrue(comp.Weight < weightBrackets[i]);
                    }
                    else if (i == weightBrackets.Count)
                    {
                        Assert.IsTrue(weightBrackets[i - 1] <= comp.Weight);
                    }
                    else
                    {
                        Assert.IsTrue(weightBrackets[i - 1] <= comp.Weight && comp.Weight < weightBrackets[i]);
                    }
                }

            }
        }
   




    }
}
