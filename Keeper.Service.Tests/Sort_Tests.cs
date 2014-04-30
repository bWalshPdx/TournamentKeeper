using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Keeper.Service.Tests
{

    [TestFixture]
    public class SortTests
    {
        //http://www.londoninternationalbjj.com/files/65971007.jpg
        List<WeightDivision> _weightDivisions = new List<WeightDivision>()
                                                       {
                                                           new WeightDivision("Rooster", 0, 127),
                                                           new WeightDivision("Light Feather", 127, 141.5),
                                                           new WeightDivision("Feather", 141.5, 154.5),
                                                           new WeightDivision("Light", 154.5, 168),
                                                           new WeightDivision("Middle", 168, 181.5),
                                                           new WeightDivision("Middle Heavy", 181.5, 195),
                                                           new WeightDivision("Heavy", 195, 208),
                                                           new WeightDivision("Super Heavy", 208, 222),
                                                           new WeightDivision("Ultra Heavy", 222, 9999.99),
                                                       };

        [Test]
        public void getCompetitors_VerifyOutput()
        {
            

            MockStuff ms = new MockStuff();
            for (int i = 0; i < 100; i++)
            {
                var comp = ms.GenerateCompetitor();
                string output = @"Rank: {0} FirstName = {1}, LastName = {2}, Team = {3}, Weight = {4}, isMale = {5}";

                Console.WriteLine(output,comp.Rank,comp.FirstName, comp.LastName,comp.Team, comp.Weight, comp.IsMale == true ? "Male": "Female");
                
            }
        }


        //TODO: <NA> Write test to just use clumps of competitors instead of divisions:
        [TestCase(1, 0)]
        [TestCase(111, 0)]
        [TestCase(127, 0)]
        [TestCase(127.001, 1)]
        [TestCase(133, 1)]
        [TestCase(141, 1)]
        [TestCase(142, 2)]
        [TestCase(160, 3)]
        [TestCase(170, 4)]
        [TestCase(190, 5)]
        [TestCase(196, 6)]
        [TestCase(210, 7)]
        [TestCase(250, 8)]
        public void sortByWeight_GoIntoTheProperSlot(double WeightUnderTest, int DivisionIndex)
        {
            Sort sort = new Sort();

            Stack<Competitor> fakeCompetitors = new Stack<Competitor>();

            Competitor testCompetitor = new Competitor()
            {
                FirstName = "",
                IsMale = false,
                LastName = "",
                Rank = "",
                Team = "",
                Weight = WeightUnderTest
            };

            fakeCompetitors.Push(testCompetitor);
            
            var output = sort.seperateWeight(fakeCompetitors,_weightDivisions);

            //First:
            Assert.That(output[DivisionIndex].Count() > 0);
        }

        
        [Test]
        public void sortByGender_InitialTest()
        {
            int numberOfCompetitors = 10;
            MockStuff ms = new MockStuff();
            var fakeCompetitors = ms.getFakeCompetitors(numberOfCompetitors);

            Sort s = new Sort();

            Stack<Func<IEnumerable<Competitor>, IEnumerable<Stack<Competitor>>>> initialSort = new Stack<Func<IEnumerable<Competitor>, IEnumerable<Stack<Competitor>>>>();

            Func<IEnumerable<Competitor>, IEnumerable<Stack<Competitor>>> sortByGender = c => c.GroupBy(comp => comp.IsMale).Select(a => new Stack<Competitor>(a.ToList()));

            initialSort.Push(sortByGender);

            var output = s.Seperate(fakeCompetitors, initialSort).ToList();

            foreach (var compClump in output)
            {
                var firstGender = compClump.First().IsMale;
                
                Assert.That(compClump.All(c => c.IsMale == firstGender));
            }

        }

        
    }
}
