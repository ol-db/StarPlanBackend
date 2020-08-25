using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using StarPlan.Models;
using StarPlanDBAccess.Procedures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace UnitTesting.Star_Plan_Logic_Testing.Space_Logic_Testing
{
    [TestClass]
    public class SolarSystemTests
    {
        #region DB methods

        #region get

        [TestMethod]
        public void GetAllFromDB_ValidRecords_ReturnSystemListJson()
        {
            //arrange
            ISqlStoredProc proc = MockLoadSystems();
            SolarSystemList systems = new SolarSystemList(0);
            List<object> systemsExpected = TestLoadSystemObject();
            string systemsExpectedJson = JsonConvert.SerializeObject(systemsExpected);

            //act
            systems.GetAllFromDB(proc);

            //logging
            Console.WriteLine("expected: {0}", systemsExpectedJson);
            Console.WriteLine("actual: {0}", systems.ToJsonSingle());

            //assert
            Assert.AreEqual(systemsExpectedJson, systems.ToJsonSingle());
        }

        #region test data

        private ISqlStoredProc MockLoadSystems()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@galaxyId", SqlDbType.VarChar);

            List<dynamic> systemData = TestLoadSystemObject();

            Mock<IDataReader> mockReader = new Mock<IDataReader>();
            mockReader.Setup(x => x.FieldCount).Returns(1);
            mockReader.Setup(x => x.GetName(0)).Returns("id");
            mockReader.Setup(x => x["id"]).Returns(0);
            mockReader.Setup(x => x.Read()).Callback
            (
                () =>
                {
                    int id = (int)mockReader.Object["id"];

                    mockReader.Setup(x => x["id"]).Returns(id + 1);

                    if (id == 2)
                    {
                        mockReader.Setup(x => x.Read()).Returns(false);
                    }
                }
            ).Returns(true);

            Mock<ISqlStoredProc> mockStoredProc = new Mock<ISqlStoredProc>(MockBehavior.Loose);
            mockStoredProc.Setup(x => x.GetParams()).Returns(cmd.Parameters);
            mockStoredProc.Setup(x => x.ExcecRdr()).Returns(mockReader.Object);

            return mockStoredProc.Object;
        }

        private List<dynamic> TestLoadSystemObject()
        {
            List<dynamic> systems = new List<dynamic>();
            systems.Add(new { id = 1});
            systems.Add(new { id = 2});
            systems.Add(new { id = 3});

            return systems;
        }

        #endregion

        #endregion

        #endregion
    }
}
