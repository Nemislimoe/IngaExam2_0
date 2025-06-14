using System;
using System.Windows.Forms;

namespace IngaExam2_0
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ініціалізація бази даних
            DatabaseHelper.InitializeDatabase();

            while (true)
            {
                // Створюємо форму входу як діалогове вікно
                using (Form1 loginForm = new Form1())
                {
                    // Якщо користувач не пройти авторизацію, виходимо із циклу
                    if (loginForm.ShowDialog() != DialogResult.OK)
                        break;

                    // Запуск головного вікна.
                    using (MainWindow mainForm = new MainWindow(loginForm.CurrentUsername, loginForm.CurrentRole))
                    {
                        Application.Run(mainForm);
                    }
                }
            }
        }
    }
}