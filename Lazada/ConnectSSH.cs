using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lazada
{
    class ConnectSSH
    {
        public void Connect(string IP, string usernameIP, string passwordIP, string port)
        {
            using (var _client = new SshClient(IP, 22, usernameIP, passwordIP))
            {
                // timeout 30s
                _client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(25);
                _client.ErrorOccurred += (se, ev) => { };
                _client.KeepAliveInterval = TimeSpan.FromSeconds(60);
                _client.Connect();

                if (_client.IsConnected)
                {
                    var forwarder = new ForwardedPortDynamic("127.0.0.1", Convert.ToUInt32(port));
                    forwarder.Exception += (se, ev) => { };
                    _client.AddForwardedPort(forwarder);

                    forwarder.Start();



                    // ko dung nua thi stop
                    //forwarder.Stop();
                    //client.Disconnect
                    //MessageBox.Show("forwarded");
                    //Thread.Sleep(10 * 60 * 1000);

                }
            }
        }
    }
}
