namespace IngaExam2_0
{
    partial class HomePageForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelInfo;

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
            this.labelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Script MT Bold", 16F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.labelInfo.Location = new System.Drawing.Point(100, 100);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(250, 32);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "Головна сторінка системи";
            // 
            // HomePageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.labelInfo);
            this.Name = "HomePageForm";
            this.Text = "Home Page";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}
