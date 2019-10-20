using System;
using System.Collections.Generic;
using System.Text;
using Photon.SocketServer;
using ExitGames.Logging;
using System.IO;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using MyGameServer.Manage;
using Common;
using MyGameServer.Handler;

namespace MyGameServer
{
    class MyGameServer : ApplicationBase
    {
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();

        private static readonly MyGameServer instance;

        public new static MyGameServer Instance { get; private set; }

        public Dictionary<OperationCode, BaseHandler> handlerDic = new Dictionary<OperationCode, BaseHandler>();
        protected override PeerBase CreatePeer(InitRequest initRequest) //客户端接连TODO 
        {
            log.Info("一个客户端连接过来了。。。。");
            return new ClientPeer(initRequest);
        }

        protected override void Setup() //初始化 TODO
        {
            Instance = this;
            //log initialize
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");

            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));

            if (configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);//让photon知道使用的是Log4NetLog插件
                XmlConfigurator.ConfigureAndWatch(configFileInfo);//让log4net这个插件读取配置文件
            }

            log.Info("Initialization complete！");

            LoginHandler();
        }

        public void LoginHandler()
        {
            LoginHandler loginHandler = new LoginHandler();
            handlerDic.Add(loginHandler.OpCode, loginHandler);
            RegisterHandler registerHandler = new RegisterHandler();
            handlerDic.Add(registerHandler.OpCode, registerHandler);
            DefaultHandler defaultHandler = new DefaultHandler();
            handlerDic.Add(defaultHandler.OpCode, defaultHandler);
        }

        protected override void TearDown()  //服务器关闭 TODO
        {
            log.Info("Server shut down！");
        }
    }
}
