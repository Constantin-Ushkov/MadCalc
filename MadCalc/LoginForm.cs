using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace MadCalc
{
    public partial class LoginForm : Form
    {
        public string PasswordHash => ComputeSHA256(uiPasswordBox.Text);

        public LoginForm()
        {
            InitializeComponent();
        }

        private void uiOkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private static string ComputeSHA256(string s)
        {
            var hash = String.Empty;

            using (var sha256 = SHA256.Create())
            {
                var hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));

                foreach (byte b in hashValue)
                {
                    hash += $"{b:X2}";
                }
            }

            return hash;
        }
    }
}
