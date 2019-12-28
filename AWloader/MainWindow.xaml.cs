using System.Windows;
using System.Diagnostics;
using System;
using System.Windows.Input;
using System.Net;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace AWloader
{
    public partial class MainWindow
    {
        
        public MainWindow()
        {
            InitializeComponent();
            Directory.CreateDirectory(@"C:/AWloader");
            
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) //lets you move window
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
        private void Download (string url, string file, string useragent)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("User-Agent: " + useragent);

                byte[] plaintext = ProtectedData.Unprotect(File.ReadAllBytes(@"C:/AWloader/login.txt"), File.ReadAllBytes(@"C:/AWloader/entropy"), DataProtectionScope.CurrentUser);
                var token = Convert.ToBase64String(plaintext);
                wc.Headers.Add("Host: aimware.net");
                wc.Headers.Add("Accept: */*");
                wc.Headers.Add("Accept-Encoding: identity");
                wc.Headers.Add("Accept-Language: en-US");
                wc.Headers.Add("Accept-Charset: *");
                wc.Headers.Add("Referer: http://aimware.net/forum/");
                wc.Headers.Add("Authorization: Basic " + token);
                wc.DownloadFile(url, @"C:/AWloader/aimware" + file + ".exe");
            }
        }
        private void Button_Click(object sender3, RoutedEventArgs e)
        {
            while (true)
            {
                try
                {
                    Download("https://aimware.net/forum/panel.php?action=download-client", "loader", "Mozilla/5.0 (Windows NT 6.1; Trident/7.0; rv:11.0) like Gecko");
                    Process.Start(@"C:/AWloader/aimwareloader.exe");
                }
                catch (Exception)
                {
                    continue;
                }
                break;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                try
                {
                    Download("https://aimware.net/forum/panel.php?action=download-client-v5", "loader", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36");
                    Process.Start(@"C:/AWloader/aimwareloader.exe");
                }
                catch (Exception)
                {
                    continue;
                }
                break;
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // Data to protect. Convert a string to a byte[] using Encoding.UTF8.GetBytes().
            byte[] plaintext = Encoding.UTF8.GetBytes(txtusername.Text + ":" + txtpassword.Text);
            // Generate additional entropy (will be used as the Initialization vector)
            byte[] entropy = new byte[20];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(entropy);
            }
            File.WriteAllBytes(@"C:/AWloader/entropy", entropy);
            byte[] ciphertext = ProtectedData.Protect(plaintext, entropy, DataProtectionScope.CurrentUser);
            File.WriteAllBytes(@"C:/AWloader/login.txt", ciphertext);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/matt1tk/AWloader");
        }
    }
}