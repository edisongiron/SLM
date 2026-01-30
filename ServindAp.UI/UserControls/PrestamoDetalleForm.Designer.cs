namespace ServindAp.UI.UserControls
{
    partial class PrestamoDetalleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // PrestamoDetalleForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1029, 933);
            Margin = new Padding(3, 4, 3, 4);
            Name = "PrestamoDetalleForm";
            Padding = new Padding(3, 85, 3, 4);
            Text = "Detalle del Préstamo";
            Load += PrestamoDetalleForm_Load;
            ResumeLayout(false);
        }

        #endregion
    }
}