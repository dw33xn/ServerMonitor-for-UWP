using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerMonitor.SiteDb;
using ServerMonitor.Models;
using System.Diagnostics;
using Windows.UI.Popups;

namespace TestServerMonitor.TestDAO
{
    [TestClass]
    public class TestSiteDAO
    {
        /// <summary>
        /// 测试用site列表
        /// </summary>
        List<SiteModel> siteModels = new List<SiteModel>();
        /// <summary>
        /// 测试用siteDAO
        /// </summary>
        SiteDAO siteDAO = new SiteDaoImpl();
        /// <summary>
        /// 测试用site
        /// </summary>
        SiteModel site;

        [TestMethod]
        [AssemblyInitialize]
        public static void InitDatabase(TestContext testContext)
        {
            DBInit db = DataBaseControlImpl.Instance;
            db.InitDB("simple5.db");
        }
        [TestMethod]
        [TestInitialize]
        public  void Init()//初始化数据库并向site列表填充数据
        {
            siteModels = new List<SiteModel>();
            SiteModel tmpsite = new SiteModel()
            {
                Site_name = "Google",
                Site_address = "https://www.google.com",
                Is_server = false,
                Protocol_type = "HTTPS",
                Server_port = 1,
                Monitor_interval = 5,
                Is_Monitor = false,
                Status_code = "200",
                Request_interval = 25383,
                Create_time = DateTime.Now,
                Update_time = DateTime.Now,
                Is_pre_check = false,
                Request_succeed_code = "200",
                Last_request_result = 0
            };
            SiteModel tmpsite2 = new SiteModel()
            {
                Site_name = "BaiDu",
                Site_address = "https://www.google.com",
                Is_server = false,
                Protocol_type = "HTTP",
                Server_port = 1,
                Monitor_interval = 5,
                Is_Monitor = false,
                Status_code = "200",
                Request_interval = 25383,
                Create_time = DateTime.Now,
                Update_time = DateTime.Now,
                Is_pre_check = false,
                Request_succeed_code = "200",
                Last_request_result = 0
            };
            
            siteModels.Add(tmpsite);
            siteModels.Add(tmpsite2);
            site = new SiteModel()
            {
                Site_name = "Google",
                Site_address = "https://www.google.com",
                Is_server = false,
                Protocol_type = "HTTPS",
                Server_port = 1,
                Monitor_interval = 5,
                Is_Monitor = false,
                Status_code = "200",
                Request_interval = 25383,
                Create_time = DateTime.Now,
                Update_time = DateTime.Now,
                Is_pre_check = false,
                Request_succeed_code = "200",
                Last_request_result = 0
            };

        }
        /// <summary>
        /// 测试InsertOneSite
        /// 返回值为1，插入一条Site数据成功
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_InsertOneSite_Success()
        {
            Assert.AreEqual(1, siteDAO.InsertOneSite(site), "success");//如果结果为1，插入成功
        }

        /// <summary>
        /// 没有SiteName字段
        /// </summary>
        [TestMethod]
        public void TestSiteDAOImpl_InsertOneSite_WithOut_Site_name()
        {
            site.Site_name = null;
            Assert.AreEqual(-1, siteDAO.InsertOneSite(site), "未设置SiteName");
        }
        /// <summary>
        /// 没有Site_address字段
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_InsertOneSite_WithOut_Site_address()
        {
            site.Site_address = null;
            Assert.AreEqual(-1, siteDAO.InsertOneSite(site), "未设置SiteAddress");//如果结果为1，插入成功
        }
        /// <summary>
        /// 没有Protocol_type字段
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_InsertOneSite_WithOut_Protocol_type()
        {
            site.Protocol_type = null;
            Assert.AreEqual(-1, siteDAO.InsertOneSite(site), "未设置");//如果结果为1，插入成功
        }
        /// <summary>
        /// 插入site列表成功
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_InsertListSite_Success()
        {
            Assert.AreEqual(siteModels.Count, siteDAO.InsertListSite(siteModels), "插入数据成功");
        }
        /// <summary>
        /// site列表中的site存在Site_name为空
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_InsertListSite_Failed_By_Item_WithOut_Site_name()
        {
            siteModels[0].Site_name = null;
            Assert.AreNotEqual(siteModels.Count, siteDAO.InsertListSite(siteModels), "插入错误");
        }
        /// <summary>
        /// site列表中的site存在Protocol_type为空
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_InsertListSite_Failed_By_Item_WithOut_Protocol_type()
        {
            siteModels[0].Protocol_type = null;
            Assert.AreNotEqual(siteModels.Count, siteDAO.InsertListSite(siteModels), "插入错误");
        }
        /// <summary>
        /// site列表中的site存在Site_address为空
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_InsertListSite_Failed_By_Item_WithOut_Site_address()
        {
            siteModels[0].Site_address = null;
            Assert.AreNotEqual(siteModels.Count, siteDAO.InsertListSite(siteModels), "插入错误");
        }
        /// <summary>
        /// 获取Site列表成功
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_GetAllSite_Success()
        {
            Assert.AreNotEqual(null, siteDAO.GetAllSite(), "获取成功");
        }
        /// <summary>
        /// 成功获取到Site
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_GetSiteById_Success() {
            if(siteDAO.GetAllSite().Count!=0)
            Assert.AreNotEqual(null, siteDAO.GetSiteById(siteDAO.GetAllSite()[0].Id));//数据库初始会插入两条数据，第一条将被删除
        }

        /// <summary>
        /// 未找到对应id的site对象
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_GetSiteById_Not_Find()
        {
            Assert.AreEqual(null, siteDAO.GetSiteById(-1), "未找到该id对应的site");
        }
        /// <summary>
        /// 成功删除
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_DeleteOneSite_Success()
        {
            List<SiteModel> list = siteDAO.GetAllSite();
            if (list.Count==0)
            {
                //site表中没有数据
                Assert.AreEqual(0, siteDAO.DeleteOneSite(1), "未找到对应site");
            }
            else
            {
                Assert.AreEqual(1, siteDAO.DeleteOneSite(list[0].Id), "删除成功");
            }

        }

        /// <summary>
        /// 没有对应id的site
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_DeleteOneSite_Not_Find_Site()
        {
            Assert.AreEqual(0, siteDAO.DeleteOneSite(-1), "未找到对应site");
        }
        /// <summary>
        /// 更新site成功
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_UpdateSite_Success()
        {
            List<SiteModel> models = siteDAO.GetAllSite();
            if(models.Count!=0){
                Assert.AreEqual(1, siteDAO.UpdateSite(models[0]));
            }
            else
            {
                //如果测试数据还未插入或者已经删除将返回0
            }
        }
        /// <summary>
        /// 更新将Site_name设置为空
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_UpdateSite_Failed_By_Set_Site_name_Null()
        {

            List<SiteModel> list = siteDAO.GetAllSite();
            if (list.Count!=0)
            {
                list[0].Site_name = null;
                Assert.AreEqual(-1, siteDAO.UpdateSite(list[0]), "将Site_name设置为null");
            }
            else
            {
                //site表中没有数据
            }
        }

        /// <summary>
        /// 更新将Site_address设为空
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_UpdateSite_Failed_By_Set_Site_address_Null()
        {
            List<SiteModel> list = siteDAO.GetAllSite();
            if (list.Count != 0)
            {
                list[0].Site_address = null;
                Assert.AreEqual(-1, siteDAO.UpdateSite(list[0]), "将Site_address设置为null");
            }
            else
            {
                //site表中没有数据
            }
        }
        /// <summary>
        /// 更新将Protocol_type设为空
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_UpdateSite_Failed_By_Set_Protocol_type_Null()
        {

            List<SiteModel> list = siteDAO.GetAllSite();
            if (list.Count != 0)
            {
                list[0].Protocol_type = null;
                Assert.AreEqual(-1, siteDAO.UpdateSite(list[0]), "将Protocol_type设置为null");
            }
            else
            {
                //site表中没有数据
            }
        }
        /// <summary>
        /// 更新site列表成功
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_UpdateListSite_Success()
        {
                Assert.AreEqual(siteDAO.GetAllSite().Count, siteDAO.UpdateListSite(siteDAO.GetAllSite()), "更新成功");
        }
        /// <summary>
        /// 更新site列表，其中有site的Site_address为空
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_UpdateListSite_Failed_By_Set_Site_address_Null()
        {
            List<SiteModel> models = siteDAO.GetAllSite();
            if (models.Count != 0) {
                models[0].Site_address = null;
                Assert.AreEqual(-1, siteDAO.UpdateListSite(models));
            }
            else {
                //数据库内数据为空    
                Assert.AreEqual(0, siteDAO.UpdateListSite(models));
            }
        }
        /// <summary>
        /// 更新site列表，其中有site的Site_name为空
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_UpdateListSite_Failed_By_Set_Site_name_Null()
        {
            List<SiteModel> models = siteDAO.GetAllSite();
            if (models.Count != 0)
            {
                models[0].Site_name = null;
                Assert.AreEqual(-1, siteDAO.UpdateListSite(models));
            }
            else
            {
                //数据库内数据为空    
                Assert.AreEqual(0, siteDAO.UpdateListSite(models));
            }
        }
        /// <summary>
        /// 更新site列表，其中有site的Protocol_type为空
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_UpdateListSite_Failed_By_Set_Protocol_type_Null()
        {
            List<SiteModel> models = siteDAO.GetAllSite();
            if (models.Count != 0)
            {
                models[0].Protocol_type = null;
                Assert.AreEqual(-1, siteDAO.UpdateListSite(models));
            }
            else
            {
                //数据库内数据为空    
                Assert.AreEqual(0, siteDAO.UpdateListSite(models));
            }
        }
        /// <summary>
        /// 执行语句成功，失败时返回一个长度为0的List
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_DBExcuteSiteCommand_Success()
        {
            object[] o = new object[] { };
            Assert.AreNotEqual(null, siteDAO.DBExcuteSiteCommand("select *from site",o ));
        }
    }
}
