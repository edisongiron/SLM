using MaterialSkin;
using MaterialSkin.Controls;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ServindAp.UI
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {

            InitializeComponent();
            WindowState = FormWindowState.Maximized;

            var materialSkinManager = MaterialSkinManager.Instance;
            // Crea/obtiene la instancia única del gestor de MaterialSkin

            materialSkinManager.AddFormToManage(this);
            // Registra este formulario (Form1) para que MaterialSkin lo controle


            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Green600,       // Barra superior - Verde medio (67, 160, 71)
                Primary.Teal700,        // Hover/Selección - Verde esmeralda oscuro (0, 121, 107)
                Primary.LightGreen500,  // Sombras/Detalles - Verde limón (139, 195, 74)
                Accent.Green700,        // Verde
                TextShade.BLACK         
            );

            DrawerShowIconsWhenHidden = true;
            DrawerHighlightWithAccent = true; // Hace que el icono seleccionado brille con el color Accent
            DrawerBackgroundWithAccent = false; // Evita que todo el fondo se pinte de verde fuerte
        }





        





    }
}
