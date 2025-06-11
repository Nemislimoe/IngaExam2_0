using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace IngaExam2_0
{
    public partial class Form1 : Form
    {
        // Для демонстрації – жорстко задані дані для адміністратора
        private string correctLogin = "admin";
        private string correctPassword = "admin"; // Для прикладу, порівнюється незахешований пароль

        // Ці властивості потрібні для передачі даних до головного вікна
        public string CurrentUsername { get; private set; }
        public string CurrentRole { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Text;
            string hashedPassword = HashPassword(password);

            // Тут для прикладу використовується просте порівняння;
            // у реальному застосунку потрібно порівнювати збережений хеш
            if (login == correctLogin && password == correctPassword)
            {
                labelMessage.Visible = false;
                MessageBox.Show("Вхід успішно виконано ^^");

                // Записуємо дані користувача
                CurrentUsername = login;
                // Наприклад, якщо логін "admin" – роль Administrator, інакше User
                CurrentRole = "Admin";

                // Встановлюємо результат діалогу та закриваємо форму, що дозволяє продовжити у Program.cs
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                labelMessage.Text = "!Логін або пароль вказано не вірно!";
                labelMessage.Visible = true;
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            // Запускаємо форму реєстрації, якщо потрібно
            RegisterForm regForm = new RegisterForm();
            regForm.Show();
            // Не викликаємо Hide(), якщо плануємо завершити форму входу після успішної авторизації
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
