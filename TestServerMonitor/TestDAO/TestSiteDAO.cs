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
using System.IO;
using Windows.Storage;

namespace TestServerMonitor.TestDAO
{
    [TestClass]
    public class TestSiteDAO
    {
        /// <summary>
        /// 测试用数据库名称
        /// </summary>
        public static string dbname="test";
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

        /// <summary>
        /// 调用测试方法之前先初始化数据库
        /// </summary>
        /// <param name="testContext"></param>
        [AssemblyInitialize]
        public static void InitDatabase(TestContext testContext)
        {
            DBInit db = DataBaseControlImpl.Instance;
            db.InitDB(TestSiteDAO.dbname);
        }
        /// <summary>
        /// 测试结束时删除数据库
        /// </summary>
        [AssemblyCleanup]
        public static void Cleanup()
        {
            string dBPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, TestSiteDAO.dbname);
            File.Delete(dBPath);
        }
        /// <summary>
        /// 每次调用测试方法还原测试site和测试site列表的状态
        /// </summary>
        [TestMethod]
        [TestInitialize]
        public  void Init()
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
            Assert.AreEqual(1, siteDAO.InsertOneSite(site), "success");//返回结果正确
            Assert.AreNotEqual(null, siteDAO.GetSiteById(site.Id));//确认数据已经插入
        }

        /// <summary>
        /// 没有SiteName字段
        /// </summary>
        [TestMethod]
        public void TestSiteDAOImpl_InsertOneSite_Failed_By_Not_Set_Site_name()
        {
            site.Site_name = null;
            Assert.AreEqual(-1, siteDAO.InsertOneSite(site), "insert failed by not set Site_name");
        }
        /// <summary>
        /// 没有Site_address字段
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_InsertOneSite_Failed_By_Not_Set_Site_address()
        {
            site.Site_address = null;
            Assert.AreEqual(-1, siteDAO.InsertOneSite(site), "insert failed by not set Site_address");//如果结果为1，插入成功
        }
        /// <summary>
        /// 没有Protocol_type字段
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_InsertOneSite_Failed_By_Not_Set_Protocol_type()
        {
            site.Protocol_type = null;
            Assert.AreEqual(-1, siteDAO.InsertOneSite(site), "insert failed by not set Protocol_type");//如果结果为1，插入成功
        }
        /// <summary>
        /// 插入site列表成功
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_InsertListSite_Success()
        {
            Assert.AreEqual(siteModels.Count, siteDAO.InsertListSite(siteModels), "insert success");
            foreach(SiteModel tmp in siteModels){
                Assert.AreNotEqual(null, siteDAO.GetSiteById(tmp.Id),"check success");//确认site列表插入成功
            }
        }
        /// <summary>
        /// site列表中的site存在Site_name为空
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_InsertListSite_Failed_By_Item_Not_Set_Site_name()
        {
            siteModels[0].Site_name = null;
            Assert.AreNotEqual(siteModels.Count, siteDAO.InsertListSite(siteModels), "insert failed by item not set Site_name");
        }
        /// <summary>
        /// site列表中的site存在Protocol_type为空
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_InsertListSite_Failed_By_Item_Not_Set_Protocol_type()
        {
            siteModels[0].Protocol_type = null;
            Assert.AreNotEqual(siteModels.Count, siteDAO.InsertListSite(siteModels), "insert failed by item not set Protocol_type");
        }
        /// <summary>
        /// site列表中的site存在Site_address为空
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_InsertListSite_Failed_By_Item_Not_Set_Site_address()
        {
            siteModels[0].Site_address = null;
            Assert.AreNotEqual(siteModels.Count, siteDAO.InsertListSite(siteModels), "insert failed by item not set Site_address");
        }
        /// <summary>
        /// 获取Site列表成功
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_GetAllSite_Success()
        {
            Assert.AreNotEqual(null, siteDAO.GetAllSite(), "get sites success");
        }
        /// <summary>
        /// 成功获取到Site
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_GetSiteById_Success() {
            siteDAO.InsertOneSite(site);
            Assert.AreNotEqual(null, siteDAO.GetSiteById(site.Id), "get site success");
        }

        /// <summary>
        /// 未找到对应id的site对象
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_GetSiteById_Failed_By_Id_Not_Find()
        {
            Assert.AreEqual(null, siteDAO.GetSiteById(-1), "not find site by this id");
        }
        /// <summary>
        /// 成功删除
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_DeleteOneSite_Success()
        {
            SiteModel tmp = new SiteModel()
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
            siteDAO.InsertOneSite(tmp);
            Assert.AreEqual(1, siteDAO.DeleteOneSite(tmp.Id));//删除成功返回1
            Assert.AreEqual(null, siteDAO.GetSiteById(tmp.Id),"delete success");//检测数据库里是否还有该数据
        }

        /// <summary>
        /// 没有对应id的site
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_DeleteOneSite_Failed_By_Not_Find_Site()
        {
            Assert.AreEqual(0, siteDAO.DeleteOneSite(-1), "not find site");
        }
        /// <summary>
        /// 更新site成功
        /// </summary>
        [TestMethod]
        public void TestSiteDAO_UpdateSite_Success()
        {
            SiteModel tmp = new SiteModel()
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
            siteDAO.InsertOneSite(tmp);
            tmp.Is_Monitor = true;
            siteDAO.UpdateSite(tmp);
            Assert.IsTrue(tmp.Is_Monitor,"更新成功");
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
                Assert.AreEqual(-1, siteDAO.UpdateSite(list[0]), "Site_name is be set null");
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
                Assert.AreEqual(-1, siteDAO.UpdateSite(list[0]), "Site_address is be set null");
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
                Assert.AreEqual(-1, siteDAO.UpdateSite(list[0]), "Protocol_type is be set null");
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
            List<SiteModel> tmp_sites = new List<SiteModel>();
            tmp_sites.Add(new SiteModel()
            {
                Site_name = "Google",
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
            });
            tmp_sites.Add(new SiteModel()
            {
                Site_name = "BaiDu",
                Site_address = "https://www.baidu.com",
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
            });
            siteDAO.InsertListSite(tmp_sites);
            tmp_sites[0].Site_name = "new";
            Assert.AreEqual(tmp_sites.Count, siteDAO.UpdateListSite(tmp_sites));//方法返回值正确
            Assert.AreEqual(tmp_sites[0].Site_name, siteDAO.GetSiteById(tmp_sites[0].Id).Site_name);//确认修改成功
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
