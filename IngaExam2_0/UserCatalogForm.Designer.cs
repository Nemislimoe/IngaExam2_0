namespace IngaExam2_0
{
    partial class UserCatalogForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelUserCatalog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.labelUserCatalog = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelUserCatalog
            // 
            this.labelUserCatalog.AutoSize = true;
            this.labelUserCatalog.Font = new System.Drawing.Font("Script MT Bold", 16F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.labelUserCatalog.Location = new System.Drawing.Point(100, 100);
            this.labelUserCatalog.Name = "labelUserCatalog";
            this.labelUserCatalog.Size = new System.Drawing.Size(350, 32);
            this.labelUserCatalog.TabIndex = 0;
            this.labelUserCatalog.Text = "Каталог книг для користувача";
            // 
            // UserCatalogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.labelUserCatalog);
            this.Name = "UserCatalogForm";
            this.Text = "User Catalog";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}
