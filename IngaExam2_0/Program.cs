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

            // Створюємо форму входу як діалогове вікно
            using (Form1 loginForm = new Form1())
            {
                // Якщо користувач успішно пройшов авторизацію (DialogResult = OK)
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Запускаємо головне вікно, передаючи дані (логін та роль)
                    Application.Run(new MainWindow(loginForm.CurrentUsername, loginForm.CurrentRole));
                }
            }
        }
    }
}
