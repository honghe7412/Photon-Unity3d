using System;
using System.Collections.Generic;
using System.Text;
using Photon.SocketServer;
using ExitGames.Logging;
using System.IO;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using MyGameServer.Manage;

namespace MyGameServer
{
    class MyGameServer : ApplicationBase
    {
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();
        protected override PeerBase CreatePeer(InitRequest initRequest) //客户端接连TODO 
        {
            log.Info("一个客户端连接过来了。。。。");
            return new ClientPeer(initRequest);
        }

        protected override void Setup() //初始化 TODO
        {
            //log initialize
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");

            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));

            if (configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);//让photon知道使用的是Log4NetLog插件
                XmlConfigurator.ConfigureAndWatch(configFileInfo);//让log4net这个插件读取配置文件
            }

            log.Info("Initialization complete！");

            IUserManage userManage = new UserManage();
            
            log.Info(userManage.VerifiyUser("学习database", "photon"));
            log.Info(userManage.VerifiyUser("学习database", "photon1"));
        }

        protected override void TearDown()  //服务器关闭 TODO
        {
            log.Info("Server shut down！");
        }
    }
}
