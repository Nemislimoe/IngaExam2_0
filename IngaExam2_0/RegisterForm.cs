using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SQLite;

namespace IngaExam2_0
{
    public partial class RegisterForm : Form
    {
        string correctLogin;
        string correctPassword;
        string correctRole;



        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string password1 = txtPassword1.Text;
            string password2 = txtPassword2.Text;
            string role = "";

            bool isValidInfo = true;

            HideErrors();
             
            if (password1.Length < 8)
            {
                 errorPassword1.Text = "!Пароль має містити мінімум 8 символів!";
                 errorPassword1.Visible = true;
                 isValidInfo = false;

            }

            if (password1 != password2)
            {
                errorPassword2.Text = "!Паролі не співпадають!";
                errorPassword2.Visible = true;
                isValidInfo = false;
            }


            if (login.Length < 5)
            {
                errorLogin.Text = "!Логін має містити мінімум 5 символів!";
                errorLogin.Visible = true;
                isValidInfo = false;
            }

            if (rbAdmin.Checked)
            {
                role = "Admin";
                errorRole.Visible = false;
            }

            else if (rbUser.Checked)
            {
                role = "User";
                errorRole.Visible = false;
            }

            else
            {
                errorRole.Text = "Choose role pls";
                errorRole.Visible = true;
                isValidInfo = false;
            }
            
            if (isValidInfo)
            {
                correctLogin = login;
                correctPassword = HashPassword(password2);
                correctRole = role;
                MessageBox.Show($"Login: {login}\n Password: {correctPassword}\n Role: {role}");
            }
            
        }

        private string HashPassword(string password) { 
            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private void HideErrors()
        {
            errorLogin.Visible = false;
            errorPassword1.Visible = false;
            errorPassword2.Visible = false;
            errorRole.Visible = false;
        }


    }
}
