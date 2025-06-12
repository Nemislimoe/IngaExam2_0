namespace IngaExam2_0
{
    partial class UserCatalogForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRent;
        private System.Windows.Forms.Label lblCatalog;
        private System.Windows.Forms.Label lblHistory;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblCatalog = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRent = new System.Windows.Forms.Button();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.lblHistory = new System.Windows.Forms.Label();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCatalog
            // 
            this.lblCatalog.AutoSize = true;
            this.lblCatalog.Font = new System.Drawing.Font("Script MT Bold", 12F);
            this.lblCatalog.Location = new System.Drawing.Point(30, 20);
            this.lblCatalog.Name = "lblCatalog";
            this.lblCatalog.Size = new System.Drawing.Size(150, 24);
            this.lblCatalog.Text = "Каталог книг:";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(30, 60);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(200, 22);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(250, 55);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.Text = "Пошук";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnRent
            // 
            this.btnRent.Location = new System.Drawing.Point(400, 55);
            this.btnRent.Name = "btnRent";
            this.btnRent.Size = new System.Drawing.Size(100, 30);
            this.btnRent.Text = "Орендувати";
            this.btnRent.Click += new System.EventHandler(this.btnRent_Click);
            // 
            // dgvBooks
            // 
            this.dgvBooks.Location = new System.Drawing.Point(30, 100);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.ReadOnly = true;
            this.dgvBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBooks.Size = new System.Drawing.Size(700, 150);
            // 
            // lblHistory
            // 
            this.lblHistory.AutoSize = true;
            this.lblHistory.Font = new System.Drawing.Font("Script MT Bold", 12F);
            this.lblHistory.Location = new System.Drawing.Point(30, 270);
            this.lblHistory.Name = "lblHistory";
            this.lblHistory.Size = new System.Drawing.Size(200, 24);
            this.lblHistory.Text = "Історія оренди:";
            // 
            // dgvHistory
            // 
            this.dgvHistory.Location = new System.Drawing.Point(30, 310);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.Size = new System.Drawing.Size(700, 150);
            // 
            // UserCatalogForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.lblCatalog);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnRent);
            this.Controls.Add(this.dgvBooks);
            this.Controls.Add(this.lblHistory);
            this.Controls.Add(this.dgvHistory);
            this.Name = "UserCatalogForm";
            this.Text = "Каталог книг для користувача";
            this.Load += new System.EventHandler(this.UserCatalogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
