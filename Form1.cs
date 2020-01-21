using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptorDecryptor
{
    public partial class Encryptor : Form
    {
        public Encryptor()
        {
            InitializeComponent();
        }

        string hash = "f0xle@rn";
        Timer time = new Timer();

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            time.Tick += new EventHandler(Time_Tick);
            time.Interval = (2) * (1000);
            time.Start();
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 10;
            if (progressBar1.Value == 100)
            {
                time.Stop();
                byte[] data = UTF8Encoding.UTF8.GetBytes(txtValue.Text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        if (progressBar1.Value == 100)
                        {
                            txtEncrypt.Text = Convert.ToBase64String(results, 0, results.Length);
                        }
                    }
                }
                btnEncrypt.Visible = false;
            }
        }
    }
}
