using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServindAp.UI
{
    public partial class FormNuevoPrestamo : Form
    {
        public FormNuevoPrestamo()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormNuevoPrestamo_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
