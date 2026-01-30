namespace ServindAp.UI.UserControls
{
    partial class FormEditarHerramienta
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormEditarHerramienta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 600);
            this.Name = "FormEditarHerramienta";
            this.Text = "Editar Herramienta";
            this.Load += new System.EventHandler(this.FormEditarHerramienta_Load);
            this.ResumeLayout(false);
        }
    }
}