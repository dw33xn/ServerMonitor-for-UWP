﻿using Etg.SimpleStubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerMonitor.Models;
using ServerMonitor.Services.RequestServices;
using ServerMonitor.ViewModels.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestServerMonitor.TestViewModel
{
    [TestClass]
    public class TestSiteDetailViewModel
    {
        private ISiteDetailUtil utilObject;
        /// <summary>
        /// 测试类生成预处理
        /// </summary>
        [TestInitialize()]
        [Owner("Bin")]
        public void Initialize()
        {
            // 实例化待测试的对象
            utilObject = new SiteDetailUtilImpl();
        }

        private SiteDetailUtilImpl util = new SiteDetailUtilImpl();
        [TestMethod]
        public void TestSuccessCodeMatch()
        {
            SiteModel site1 = new SiteModel() { Request_succeed_code = "220,310" };
            SiteModel site2 = new SiteModel() { Request_succeed_code = "220" };
            SiteModel site3 = new SiteModel() { Request_succeed_code = "" };
            SiteModel site4 = new SiteModel() { Request_succeed_code = null };
            Assert.IsTrue(util.SuccessCodeMatch(site1, "220"));
            Assert.IsTrue(util.SuccessCodeMatch(site1, "310"));
            Assert.IsTrue(util.SuccessCodeMatch(site2, "220"));
            Assert.IsFalse(util.SuccessCodeMatch(site2, ""));
            Assert.IsFalse(util.SuccessCodeMatch(site3, "220"));
        }


        [TestMethod]
        [Owner("Bin")]
        public void TestAccessDNSServer() {
            SiteModel site = new SiteModel() {Site_address="8.8.8.8",Create_time=DateTime.Now,ProtocolIdentification="localhost"};
            //StubIRequest stub1 = new StubIRequest();
            LogModel log = utilObject.AccessDNSServer(site, DNSRequest.Instance).Result;
            // 判断是否进行了校验
            Assert.IsNotNull(log);
            // 判断请求的结果是否为预期的成功
             Assert.IsFalse(log.Is_error);
        }

        [TestMethod]
        [Owner("Bin")]
        public void TestCreateLogWithRequestServerResult()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Owner("Bin")]
        public void TestUpdateSiteStatus()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Owner("Bin")]
        public void TestGetIPAddressAsync()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Owner("Bin")]
        public void TestAccessFTPServer()
        {
            FTPRequest request = FTPRequest.Instance;
            request.Identification = new IdentificationInfo() {Username="free",Password="free" };
            SiteModel site = new SiteModel() {Site_address= "47.94.251.85", ProtocolIdentification="" };
            utilObject.AccessFTPServer(site, request);

            // 判断这次请求是否发生
            Assert.IsFalse(string.IsNullOrEmpty(request.ProtocalInfo));
            // 判断这次预计成功的请求是否成功
            Assert.AreEqual(site.Status_code, "1000");
            // 判断站点请求次数是否符合请求逻辑
            Assert.AreEqual(site.Request_count, 1);
        }

        [TestMethod]
        [Owner("Bin")]
        public void Testtt() { 

        }

        [TestCleanup]
        public void CleanUp() {

        }
    }
}
