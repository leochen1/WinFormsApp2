using System.Net.Http.Headers;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Security.Policy;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //https://notify-bot.line.me/my/services/edit?clientId=etEJ2JexIOKD3vrPQ2VBB0
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
            Application.Exit();
            Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000 * 60; // 每分鐘
            timer1.Enabled = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int time = Convert.ToInt32(textBox1.Text.ToString());
            if (DateTime.Now.Minute % time == 0)
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "zJGq6xtRuVt4Wc8HubnkIfPcyHC4fxX46BDqR2IliZo");
                var content = new Dictionary<string, string>();
                content.Add("message", $"Hello 里歐 每 {time} 分鐘對小公主說 我愛你");
                httpClient.PostAsync("https://notify-api.line.me/api/notify", new FormUrlEncodedContent(content));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //https://josephchou56.pixnet.net/blog/post/218992416-line-notify-send-local-image-c%23
            
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls;

                //var file = @"https://womany.net/cdn-cgi/image/w=800,fit=scale-down,f=auto/https://castle.womany.net/images/content/pictures/101112/womany_5DA4C8EE0D6271571080430_1571816219-28925-0091-4871.jpeg";  //圖檔位置
                var file = @"D:\wow.jpg";
                var upfilebytes = File.ReadAllBytes(file);
                HttpClientHandler handler = new HttpClientHandler();
                HttpClient Client = new HttpClient(handler);
                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "zJGq6xtRuVt4Wc8HubnkIfPcyHC4fxX46BDqR2IliZo");  //Token
                MultipartFormDataContent content = new MultipartFormDataContent();
                ByteArrayContent baContent = new ByteArrayContent(upfilebytes);
                content.Add(baContent, "imageFile", "wow.jpg");
                string url = @"https://notify-api.line.me/api/notify?message=";      //組訊息
                url = url + "反派小公主";
                var result = Client.PostAsync(url, content).Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //https://blog.poychang.net/line-notify-2-use-web-api/

            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, 60);
                client.BaseAddress = new Uri("https://notify-api.line.me/api/notify");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "zJGq6xtRuVt4Wc8HubnkIfPcyHC4fxX46BDqR2IliZo");
                var file = @"D:\wow.jpg";
                var upfilebytes = File.ReadAllBytes(file);

                MultipartFormDataContent content = new MultipartFormDataContent();
                ByteArrayContent baContent = new ByteArrayContent(upfilebytes);
                content.Add(baContent, "imageFile", "wow.jpg");

                client.PostAsync("", content);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //http://jashliao.eu/wordpress/2018/04/19/c%E5%91%BC%E5%8F%ABcurl%E6%8A%93%E5%8F%96%E7%B6%B2%E9%A0%81%E5%85%A7%E5%AE%B9-c-call-curl-exe-to-txt-file/
            //https://developers.line.biz/en/docs/messaging-api/sticker-list/#sticker-definitions

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "curl.exe";  // Specify exe name.
            start.Arguments = @"-X POST https://notify-api.line.me/api/notify -H ""Authorization: Bearer zJGq6xtRuVt4Wc8HubnkIfPcyHC4fxX46BDqR2IliZo"" -F ""message=Leo Love Brenda"" -F ""stickerPackageId=11537"" -F ""stickerId=52002743""";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;

            using (Process p = Process.Start(start))
            {

            }
        }
    }
}