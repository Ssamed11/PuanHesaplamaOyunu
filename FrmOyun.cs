using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp2
{

    public partial class FrmOyun : Form
    {
        static int toplam = 0;
        static int sure = 10;
        Random rnd = new Random();

        public FrmOyun()
        {
            InitializeComponent();
        }

        
        private void tmrButton_Tick(object sender, EventArgs e)
        {
            Button btn = new Button();
            btn.Size = new Size(50, 50);
            btn.Location = new Point(rnd.Next(this.ClientSize.Width - pnlGosterge.Width - btn.Width), rnd.Next(this.ClientSize.Height - btn.Height));
            btn.Text = rnd.Next(100).ToString();
            this.Controls.Add(btn);
            btn.Click += Btn_Click;

        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            toplam += int.Parse(btn.Text);
            lblSkor.Text = $"Skor: {toplam}";
            btn.Dispose();
            //sürenin eksiye gitmemesi lazım +
            //süre bittiğinde oyun bitti skor: şeklinde message box çıkacak +
            // bir text dosyasına tarih ve saatiyle skoru kaydedeceksiniz
            //yeniden başlamak ister misniiz diye soracaksınız evet derse yeniden başlayacak
            //hayır derse kapatıcak bitecek 2. maddede
        }

        private void FrmOyun_Load(object sender, EventArgs e)
        {
            tmrButton.Start();
            tmrSure.Start();
        }

        private void tmrSure_Tick(object sender, EventArgs e)
        {
            sure--;
            lblSure.Text = sure.ToString();
            if (sure == 0)
            {

                tmrSure.Stop();
                tmrButton.Stop();
                lblSkor.Visible = false;
                MessageBox.Show("Oyun bitti. Toplam Skor: " + toplam);
                
                DialogResult cevap;
                cevap = MessageBox.Show("Devam etmek istiyor musunuz? ", "Çıkış", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (cevap == DialogResult.No)
                {
                    Application.Exit();
                }
                else
                {
                    Application.Restart();
                }
                
                FileStream fs1 = new FileStream("D:\\OyunRaporu.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs1);
                writer.Write("Oyun bitti.\nOyun Puanı:" + toplam + "\nOyun Tarihi:  " + DateTime.Now);
                writer.Close();
            }
        }
    }
}


