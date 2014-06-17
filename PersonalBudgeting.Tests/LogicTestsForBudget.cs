using NUnit.Framework;
using PersonalBudgeting.BLL;
using PersonalBudgeting.MOCK_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudgeting.Tests
{
    [TestFixture]
    public class TestsLogicOfBudget
    {

        Core core;
        List<Income> _listofIncome;
        DAL myDAL;

        [TestFixtureSetUp]
        public void TestSetuptheEnvironment()
        {
            myDAL = new DAL();
            core = new Core();
            _listofIncome = myDAL.retrieveListOfIncome();
        }


        [Test]
        public void TestCalculatePendingAmount()
        {
           Assert.AreEqual(5100, core.getGrossIncome(_listofIncome));
        }


        [TestFixtureTearDown]
        public void TestTearDownTheEnvironment()
        {
            core = null;
            _listofIncome = null;

        }


    }
}
