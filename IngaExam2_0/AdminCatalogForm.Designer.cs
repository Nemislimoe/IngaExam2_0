namespace IngaExam2_0
{
    partial class AdminCatalogForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelAdminCatalog;

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
            this.labelAdminCatalog = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelAdminCatalog
            // 
            this.labelAdminCatalog.AutoSize = true;
            this.labelAdminCatalog.Font = new System.Drawing.Font("Script MT Bold", 16F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.labelAdminCatalog.Location = new System.Drawing.Point(100, 100);
            this.labelAdminCatalog.Name = "labelAdminCatalog";
            this.labelAdminCatalog.Size = new System.Drawing.Size(350, 32);
            this.labelAdminCatalog.TabIndex = 0;
            this.labelAdminCatalog.Text = "Адміністративний каталог книг";
            // 
            // AdminCatalogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.labelAdminCatalog);
            this.Name = "AdminCatalogForm";
            this.Text = "Admin Catalog";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}
