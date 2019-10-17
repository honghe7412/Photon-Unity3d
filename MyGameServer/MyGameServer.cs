using System;
using System.Collections.Generic;
using System.Text;
using Photon.SocketServer;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using System.IO;
using log4net.Config;

namespace MyGameServer
{
    public class MyGameServer : ApplicationBase
    {
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();
        //和客户端的链接
        protected override PeerBase CreatePeer(InitRequest initRequest) //photon帮助管理ClientPeer
        {
            log.Info("ClientConected");
            return new ClientPeer(initRequest);
        }
        //初始化
        protected override void Setup()
        {
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath,"log4net.config"));

            if (configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance); //设置使用的哪个日志的插件
                XmlConfigurator.ConfigureAndWatch(configFileInfo); //让这个插件读取配置文件
            }

            log.Info("Setup Complete");
        }
        //server关闭时
        protected override void TearDown()
        {
            log.Info("Shut down Complete");
        }
    }
}
