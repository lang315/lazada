using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using xNet.Net;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Xml;
using Renci.SshNet;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace Lazada
{
    public partial class Form1 : Form
    {

        const int THREAD_SLEEP_FAST = 2000;
        const int THREAD_SLEEP_SLOW = THREAD_SLEEP_FAST * 5;
        Queue<Account> _lstAccount = new Queue<Account>();
        List<string> _listLinkProducts = new List<string>();
        List<string> _listUseAgent = new List<string>();
        List<string> _listSSH = new List<string>();
        List<string> _listLinkRef = new List<string>();
        int _timeSleepMin, _timeSleepMax, _countThreads, _port, _randomAccount, _randomAccountMin, _randomAccountMax;
        object _isRunningObj = new object();
        object _accountObj = new object();
        object _fileResultObj = new object();
        object _fileErrorObj = new object();
        object _threadCountObj = new object();
        object _randomAccountObj = new object();
        string _fileResultPath, _fileErrorPath;
        bool _onlyLikeProduct, _removeProduct;
        bool _isRunning;
        Random _random = new Random();

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            if (!LoadSetting())
            {
                btnStart.Enabled = true;
                return;
            }
        }

        void SettingsRequest(ref HttpRequest request, string userAgent)
        {
            request.Cookies = new CookieDictionary();
            //request.Proxy = HttpProxyClient.Parse("127.0.0.1:8888");
            request.UserAgent = userAgent;
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            request.AddHeader("Encoding", "gzip, deflate, br");
            request.AddHeader("Language", "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
        }

        bool IsSucesssfulReg(string html, string fullname)
        {
            if (html.Contains(fullname))
                return true;
            return false;
        }

        private void GenerateListUseAgent()
        {
            List<string> lstUseAgent = File.ReadAllLines(GetCurrentDirectory() + @"Agents.txt").ToList();
            lstUseAgent = lstUseAgent.OrderBy(n => Guid.NewGuid()).Take(lstUseAgent.Count).ToList();
            _listUseAgent = new List<string>(lstUseAgent);
        }

        private bool GenerateAccountQueue()
        {
            try
            {
                GenerateListSSH();
                var generateList = new GenerateListAccount(txtFileAccountPath.Text);
                _lstAccount = new Queue<Account>(generateList.Queue(_onlyLikeProduct, _listSSH));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool GenerateListLinkProducts()
        {
            try
            {
                _listLinkProducts = File.ReadAllLines(txtFileLinkPath.Text).ToList();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void GenerateListSSH()
        {
            _listSSH = File.ReadAllLines(txtFileSSHPath.Text).ToList();
        }

        private void GenerateListLinkRef()
        {
            _listLinkRef = File.ReadAllLines(GetCurrentDirectory() + @"LinkRef.txt").ToList();
        }

        private void Job()
        {
            Account account;
            string userAgent = string.Empty; 

            while (true)
            {
                lock (_isRunningObj)
                {
                    if (!_isRunning)
                        break;
                }

                lock (_accountObj)
                {
                    if (_lstAccount.Count <= 0)
                        break;
                    if (_randomAccount <= 0)
                    {
                        _randomAccount = _random.Next(_randomAccountMin + 1, _randomAccountMax + 1);
                    }
                    account = _lstAccount.Dequeue();
                    //Debug.WriteLine(account.Email);
                    _port++;
                    userAgent = _listUseAgent[_random.Next(0, _listUseAgent.Count)];
                }
                if (_onlyLikeProduct)
                {
                    JobLikeProduct(account, _port.ToString(), userAgent);
                    //RunJOB(account, _port.ToString(), userAgent);
                }

                else
                {
                    JobRegister(account, _port.ToString(), userAgent);
                }
            }

            lock (_threadCountObj)
            {
                _countThreads--;
                if (_countThreads == 0)
                {
                    _isRunning = false;
                    btnStart.Enabled = true;
                    grpSettings.Enabled = true;
                    btnStart.Text = "Start";
                }
            }
        }

        void JobLikeProduct(Account account, string port, string userAgent)
        {
            HttpRequest request = new HttpRequest();
            var configXNet = new ConfigXNet();
            configXNet.AddUserAgent(ref request, userAgent);

            try
            {
                using (var client = new SshClient(account.IP, 22, account.UsernameIP, account.PasswordIP))
                {
                    // timeout 30s
                    client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(25);
                    client.ErrorOccurred += (se, ev) => { };
                    client.KeepAliveInterval = TimeSpan.FromSeconds(60);
                    client.Connect();

                    if (client.IsConnected)
                    {
                        var forwarder = new ForwardedPortDynamic("127.0.0.1", Convert.ToUInt32(port));
                        forwarder.Exception += (se, ev) => { };
                        client.AddForwardedPort(forwarder);

                        forwarder.Start();
                        request.Proxy = HttpProxyClient.Parse(ProxyType.Socks5, "127.0.0.1:" + port);

                        var login = new LoginAccount();

                        if (login.IsSuccessful(ref request, account))
                        {
                            if (_removeProduct)
                            {
                                var remove = new RemoveProduct();
                                remove.Remove(ref request);
                            }

                            lock (_fileResultObj)
                            {
                                _randomAccount--;
                                rtxtResult.Text += account.Email + " | " + account.Password + Environment.NewLine;
                            }

                            InteractProducts(ref request, account);
                        }
                        else
                        {
                            var export = new ExportResult();
                            lock (_fileErrorObj)
                            {
                                export.FileResult(_fileErrorPath, account);
                                rtxtResult.Text += account.Email + " | " + account.Password + " | Lỗi không đăng nhập được" + Environment.NewLine;
                            }
                            return;
                        }

                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch
            {
                var export = new ExportResult();
                lock (_fileErrorObj)
                {
                    export.FileResult(_fileErrorPath, account);
                    rtxtResult.Text += account.Email + " | " + account.Password + " | Lỗi không đăng nhập được" + Environment.NewLine;
                }
                return;
            }
        }

        //bool Login(Account account, ref HttpRequest request, string port, string useAgent)
        //{

        //    SettingsRequest(ref request, useAgent);

        //    string html = request.Get("https://www.lazada.vn/customer/account/login/").ToString();
        //    var info = new ProductInfo(html);
        //    request.AddParam("FORM_TOKEN", info.FormToken());
        //    request.AddParam("referer", "https://www.lazada.vn/");
        //    request.AddParam("LoginForm[email]", account.Email);
        //    request.AddParam("LoginForm[password]", account.Password);
        //    Thread.Sleep(THREAD_SLEEP_FAST);
        //    html = request.Post("https://www.lazada.vn/customer/account/login/").ToString();
        //    html = request.Get("https://www.lazada.vn/customer/account/").ToString();
        //    if (html.Contains("Quản lý tài khoản"))
        //        return true;
        //    return false;

        //}

        void JobRegister(Account account, string port, string userAgent)
        {
            HttpRequest request = new HttpRequest();
            var configXNet = new ConfigXNet();
            configXNet.AddUserAgent(ref request, userAgent);

            try
            {
                using (var client = new SshClient(account.IP, 22, account.UsernameIP, account.PasswordIP))
                {
                    // timeout 30s
                    client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(25);
                    client.ErrorOccurred += (se, ev) => { };
                    client.KeepAliveInterval = TimeSpan.FromSeconds(60);
                    client.Connect();

                    if (client.IsConnected)
                    {
                        var forwarder = new ForwardedPortDynamic("127.0.0.1", Convert.ToUInt32(port));
                        forwarder.Exception += (se, ev) => { };
                        client.AddForwardedPort(forwarder);

                        forwarder.Start();
                        request.Proxy = HttpProxyClient.Parse(ProxyType.Socks5, "127.0.0.1:" + port);

                        var register = new RegisterAccount();

                        if (register.IsSuccessful(ref request, account))
                        {
                            lock (_randomAccountObj)
                            {
                                _randomAccount--;
                                var export = new ExportResult();
                                export.FileResult(_fileResultPath, account);
                                rtxtResult.Text += account.Email + " | " + account.Password + Environment.NewLine;
                            }

                            InteractProducts(ref request, account);

                            //Debug.WriteLine(account.Email, "Xong");

                        }
                        else
                        {
                            //Debug.WriteLine(account.Email, "Lỗi");
                            var export = new ExportResult();
                            lock (_fileErrorObj)
                                export.FileResult(_fileErrorPath, account);
                        }
                    }
                    else
                    {
                        //Debug.WriteLine(account.Email, "Lỗi SSH");
                        var export = new ExportResult();
                        lock (_fileErrorObj)
                            export.FileResult(_fileErrorPath, account);
                    }
                }
            }
            catch
            {
                //Debug.WriteLine(account.Email, "Lỗi SSH");
                var export = new ExportResult();
                lock (_fileErrorObj)
                    export.FileResult(_fileErrorPath, account);
            }

        }

        void RunJOB(Account account, string port, string userAgent)
        {
            HttpRequest request = new HttpRequest();
            var configXNet = new ConfigXNet();
            configXNet.AddUserAgent(ref request, userAgent);
            var login = new LoginAccount();
            if (login.IsSuccessful(ref request, account))
                foreach (var item in _listLinkProducts)
                {
                    string linkProduct = item;
                    if (linkProduct.Contains("?"))
                        linkProduct = linkProduct.Split('?')[0];
                    var product = new Product(linkProduct);
                    var interact = new InteractProduct(product);
                    interact.ViewProduct(ref request, linkProduct);
                    interact.AddToWishList(ref request);
                }
        }

        void InteractProducts(ref HttpRequest request, Account account)
        {
            string linkref = _listLinkRef[_random.Next(0, (_listLinkRef.Count - 1))];
            bool random = false;
            lock (_randomAccountObj)
            {
                //Debug.WriteLine(_randomAccount, "Số account");
                if (_randomAccount == 0)
                {
                    random = true;
                    _randomAccount = _random.Next(_randomAccountMin + 1, _randomAccountMax + 1);
                }
            }

            foreach (var item in _listLinkProducts)
            {
                request.Referer = linkref;
                Thread.Sleep(_random.Next(_timeSleepMin, _timeSleepMax + 1) * 1000);
                string linkProduct = item;
                if (linkProduct.Contains("?"))
                    linkProduct = linkProduct.Split('?')[0];
                var product = new Product(linkProduct);
                var interact = new InteractProduct(product);
                interact.ViewProduct(ref request, linkProduct);
                interact.AddToWishList(ref request);
            }


            if (random)
            {


                //Debug.WriteLine(account.Email, "Add to cart");
                foreach (var item in _listLinkProducts)
                {
                    request.Referer = linkref;
                    Thread.Sleep(_random.Next(_timeSleepMin, _timeSleepMax + 1) * 1000);
                    var config = new ConfigXNet();
                    string linkProduct = item;
                    if (linkProduct.Contains("?"))
                        linkProduct = linkProduct.Split('?')[0];
                    var product = new Product(linkProduct);
                    var interact = new InteractProduct(product);
                    config.AddHeaderXCSRFToken(ref request);
                    interact.AddToCart(ref request);

                }

            }



        }


        private void chkOnlyLike_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOnlyLike.Checked)
                chkRemoveProduct.Enabled = true;
            else
                chkRemoveProduct.Enabled = false;
        }

        private string GetCurrentDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        private bool SaveSetting()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(GetCurrentDirectory() + @"Settings.xml");
                xmlDoc.SelectSingleNode("/Settings/Common/CountThreads").InnerText = nudCountThreads.Value.ToString();
                xmlDoc.SelectSingleNode("/Settings/Common/RandomTime").InnerText = $"{nudTimeMin.Value.ToString()}-{nudTimeMax.Value.ToString()}";
                xmlDoc.SelectSingleNode("/Settings/Common/RandomAccount").InnerText = $"{nudMinAccount.Value.ToString()}-{nudMaxAccount.Value.ToString()}";
                xmlDoc.SelectSingleNode("/Settings/Common/PathFileAccount").InnerText = txtFileAccountPath.Text;
                xmlDoc.SelectSingleNode("/Settings/Common/PathFileLink").InnerText = txtFileLinkPath.Text;
                xmlDoc.SelectSingleNode("/Settings/Common/PathFileSSH").InnerText = txtFileSSHPath.Text;
                if (_onlyLikeProduct)
                {
                    xmlDoc.SelectSingleNode("/Settings/Common/OnlyLikeProduct").InnerText = "true";
                }
                else
                {
                    xmlDoc.SelectSingleNode("/Settings/Common/OnlyLikeProduct").InnerText = "false";
                }
                xmlDoc.Save(GetCurrentDirectory() + @"\Settings.xml");
                return true;
            }
            catch
            {
                MessageBox.Show("Có lỗi khi save settings", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool LoadSetting()
        {

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(GetCurrentDirectory() + @"Settings.xml");
                nudCountThreads.Value = Convert.ToInt32(xmlDoc.SelectSingleNode("/Settings/Common/CountThreads").InnerText);

                if (xmlDoc.SelectSingleNode("/Settings/Common/OnlyLikeProduct").InnerText == "true")
                {
                    chkOnlyLike.Checked = true;
                }
                else
                {
                    chkOnlyLike.Checked = false;
                }
                string time = xmlDoc.SelectSingleNode("/Settings/Common/RandomTime").InnerText;
                string randomAccount = xmlDoc.SelectSingleNode("/Settings/Common/RandomAccount").InnerText;
                nudTimeMin.Value = Convert.ToInt32(time.Split('-')[0]);
                nudTimeMax.Value = Convert.ToInt32(time.Split('-')[1]);
                nudMinAccount.Value = Convert.ToInt32(randomAccount.Split('-')[0]);
                nudMaxAccount.Value = Convert.ToInt32(randomAccount.Split('-')[1]);
                txtFileAccountPath.Text = xmlDoc.SelectSingleNode("/Settings/Common/PathFileAccount").InnerText;
                txtFileLinkPath.Text = xmlDoc.SelectSingleNode("/Settings/Common/PathFileLink").InnerText;
                txtFileSSHPath.Text = xmlDoc.SelectSingleNode("/Settings/Common/PathFileSSH").InnerText;
                return true;
            }
            catch
            {
                MessageBox.Show("Có lỗi khi load settings", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void JobSettings()
        {

            btnStart.Enabled = false;

            if (chkOnlyLike.Checked)
                _onlyLikeProduct = true;
            else
                _onlyLikeProduct = false;

            if (chkRemoveProduct.Checked)
                _removeProduct = true;
            else
                _removeProduct = false;

            if (!SaveSetting())
            {
                btnStart.Enabled = true;
                grpSettings.Enabled = true;
                btnStart.Text = "Start";
                return;
            }

            if (_isRunning)
            {
                _isRunning = false;
                btnStart.Text = "Waiting";
                return;
            }
            else
            {
                rtxtResult.Text = "";
            }

            _isRunning = true;
            btnStart.Text = "Stop";
            btnStart.Enabled = true;
            grpSettings.Enabled = false;

            _randomAccountMin = Convert.ToInt32(nudMinAccount.Value);
            _randomAccountMax = Convert.ToInt32(nudMaxAccount.Value);
            _timeSleepMin = Convert.ToInt32(nudTimeMin.Value);
            _timeSleepMax = Convert.ToInt32(nudTimeMax.Value);
            _countThreads = Convert.ToInt32(nudCountThreads.Value);

            if (!GenerateAccountQueue())
            {
                MessageBox.Show("Lỗi, file account đang mở!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnStart.Enabled = true;
                grpSettings.Enabled = true;
                btnStart.Text = "Start";
                return;
            }

            if (!GenerateListLinkProducts())
            {
                MessageBox.Show("Lỗi file Link sản phẩm!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnStart.Enabled = true;
                grpSettings.Enabled = true;
                btnStart.Text = "Start";
                return;
            }

            GenerateListUseAgent();
            GenerateListLinkRef();

            var export = new ExportResult();

            if (!_onlyLikeProduct)
            {
                _fileResultPath = "Register Result " + DateTime.Now.ToShortDateString().Replace(@"/", "") + ".xlsx";
                _fileErrorPath = "Register Error " + DateTime.Now.ToShortDateString().Replace(@"/", "") + ".xlsx";
                export.CreateFile(_fileResultPath);
                export.CreateFile(_fileErrorPath);
            }
            else
            {
                _fileResultPath = "Login Result " + DateTime.Now.ToShortDateString().Replace(@"/", "") + ".xlsx";
                _fileErrorPath = "Login Error " + DateTime.Now.ToShortDateString().Replace(@"/", "") + ".xlsx";
                export.CreateFile(_fileErrorPath);
            }

            _port = 0;
            _randomAccount = 0;


        }

        private void btnFileAccountPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog pathFileAddPath = new OpenFileDialog();
            pathFileAddPath.Filter = "All files (*.xlsx)|*.xlsx";
            if (pathFileAddPath.ShowDialog() == DialogResult.OK)
            {
                txtFileAccountPath.Text = pathFileAddPath.FileName;
            }
        }

        private void btnFileLinkPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog pathFileAddPath = new OpenFileDialog();
            pathFileAddPath.Filter = "All files (*.txt)|*.txt";
            if (pathFileAddPath.ShowDialog() == DialogResult.OK)
            {
                txtFileLinkPath.Text = pathFileAddPath.FileName;
            }
        }

        private void btnFileSSHPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog pathFileAddPath = new OpenFileDialog();
            pathFileAddPath.Filter = "All files (*.txt)|*.txt";
            if (pathFileAddPath.ShowDialog() == DialogResult.OK)
            {
                txtFileSSHPath.Text = pathFileAddPath.FileName;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            Thread threadSettings = new Thread(JobSettings);
            threadSettings.Start();
            threadSettings.Join();

            Thread[] threads = new Thread[_countThreads];
            new Thread(() =>
            {
                for (int i = 0; i < _countThreads; i++)
                {
                    threads[i] = new Thread(Job);
                    threads[i].IsBackground = true;
                    threads[i].Start();
                    Thread.Sleep(1000);
                }
            })
            { IsBackground = true }.Start();
        }
    }
}
