using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Keeper.Service.Tests
{
    

    [TestFixture]
    public class Tournament_Tests
    {
        [Test]
        public void Bracket_ProperAmountOfLeavesAvailable()
        {
            MockStuff m = new MockStuff();
            var competitors = m.getFakeCompetitors(10);
            
            Bracket b = new Bracket(competitors);

            var stuff = "stuff";
        }

        [Test]
        public void Bracket_Print()
        {
            
            /*
             *                 stuff
             *       stuff2           stuff2
             * stuff3   stuff3   stuff3    stuff3
             */
        }


        //TODO:<NA> Still researching how to print a binary tree:
        //http://stackoverflow.com/questions/1649027/how-do-i-print-out-a-tree-structure

        public string printBracket(Bracket bracket, int printLevel)
        {


            var matchCount = bracket.match.Combatants.Count();


            


            if (bracket.match == null)
            {
                return "0";
            }
            else
            {
                
            }




        }

    }
}
