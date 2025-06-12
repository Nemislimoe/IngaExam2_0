using System;
using System.Data;
using System.Windows.Forms;

namespace IngaExam2_0
{
    public partial class UserCatalogForm : Form
    {
        private int currentUserId = 1;

        public UserCatalogForm()
        {
            InitializeComponent();
        }

        private void UserCatalogForm_Load(object sender, EventArgs e)
        {
            LoadBooks();
            LoadRentalHistory();
        }

        private void LoadBooks(string filter = "")
        {
            DataTable dt = DatabaseHelper.GetBooks(filter);
            dgvBooks.DataSource = dt;
        }

        private void LoadRentalHistory()
        {
            DataTable dt = DatabaseHelper.GetRentalHistory(currentUserId);
            dgvHistory.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadBooks(txtFilter.Text.Trim());
        }

        private void btnRent_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                int bookId = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["BookId"].Value);
                string status = dgvBooks.SelectedRows[0].Cells["Status"].Value.ToString();
                if (status != "в наявності")
                {
                    MessageBox.Show("Ця книга вже в оренді!");
                    return;
                }
                if (DatabaseHelper.RentBook(bookId, currentUserId))
                {
                    MessageBox.Show("Книга успішно орендована!");
                    LoadBooks(txtFilter.Text.Trim());
                    LoadRentalHistory();
                }
                else
                {
                    MessageBox.Show("Помилка при оренді книги!");
                }
            }
            else
            {
                MessageBox.Show("Оберіть книгу для оренди!");
            }
        }
    }
}
