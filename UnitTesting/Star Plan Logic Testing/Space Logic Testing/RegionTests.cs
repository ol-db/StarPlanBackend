using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using StarPlan.Models.Space.Planets;
using StarPlanDBAccess.Procedures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;

namespace UnitTesting.Star_Plan_Logic_Testing.Space_Logic_Testing
{
    [TestClass]
    public class RegionTests
    {
        #region DB methods

        [TestMethod]
        public void GetAllFromDB_ValidRecords_ReturnRegionListJson()
        {
            //arrange
            ISqlStoredProc proc = MockLoadRegions();
            RegionList regions = new RegionList(0,new Point(2,3));
            List<object> regionsExpected = TestLoadRegionObject();
            string regionsExpectedJson = JsonConvert.SerializeObject(regionsExpected);

            //act
            regions.GetAllFromDB(proc);

            //logging
            Console.WriteLine("expected: {0}", regionsExpectedJson);
            Console.WriteLine("actual: {0}", regions.ToJson());

            //assert
            Assert.AreEqual(regionsExpectedJson, regions.ToJson());
        }

        #region test data

        private ISqlStoredProc MockLoadRegions()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@systemId", SqlDbType.VarChar);

            List<dynamic> regionData = TestLoadRegionObject();

            Mock<IDataReader> mockReader = new Mock<IDataReader>();
            mockReader.Setup(x => x.FieldCount).Returns(3);
            mockReader.Setup(x => x.GetName(0)).Returns("id");
            mockReader.Setup(x => x.GetName(1)).Returns("name");
            mockReader.Setup(x => x["id"]).Returns(0);
            mockReader.Setup(x => x.Read()).Callback
            (
                () =>
                {
                    int id = (int)mockReader.Object["id"];

                    string name = regionData[id].name;

                    mockReader.Setup(x => x["id"]).Returns(id + 1);
                    mockReader.Setup(x => x["name"]).Returns(name);

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

        private List<dynamic> TestLoadRegionObject()
        {
            List<dynamic> regions = new List<dynamic>();
            regions.Add(new { id = 1, name = "row1" });
            regions.Add(new { id = 2, name = "row2" });
            regions.Add(new { id = 3, name = "row3" });

            return regions;
        }

        #endregion

        #endregion
    }
}
