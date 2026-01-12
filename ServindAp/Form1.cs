using MaterialSkin;
using MaterialSkin.Controls;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ServindAp
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {

            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            var materialSkinManager = MaterialSkinManager.Instance;
            // Crea/obtiene la instancia única del gestor de MaterialSkin

            materialSkinManager.AddFormToManage(this);
            // Registra este formulario (Form1) para que MaterialSkin lo controle


            materialSkinManager.ColorScheme = new ColorScheme(
            Primary.Green600,       // Barra superior - Verde medio (67, 160, 71)
            Primary.Teal700,        // Hover/Selección - Verde esmeralda oscuro (0, 121, 107)
            Primary.LightGreen500,  // Sombras/Detalles - Verde limón (139, 195, 74)
            Accent.Green700,        // Verde
            TextShade.WHITE         // COLOR DEL TEXTO - Blanco  

            );

            this.DrawerShowIconsWhenHidden = true;
            this.DrawerHighlightWithAccent = true; // Hace que el icono seleccionado brille con el color Accent
            this.DrawerBackgroundWithAccent = false; // Evita que todo el fondo se pinte de verde fuerte


        }

        private void Form1_Load(object sender, EventArgs e)
        {


            // Centrar el logo horizontalmente
            int logoX = (Home.Width - Logo.Width) / 2;
            Logo.Location = new Point(logoX, 150); // Altura




            //Letra

            // Configurar fuente tamaño 14, negrita
            label1.Font = new Font("Rockwell", 14, FontStyle.Bold);
            // Color gris más claro
            label1.ForeColor = Color.FromArgb(130, 130, 130);

            // Centrado horizontal
            label1.AutoSize = false;
            label1.Width = Home.Width;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Location = new Point(0, 380);

            // Crear línea decorativa debajo del texto
            Panel lineaSubrayado = new Panel();
            lineaSubrayado.BackColor = Color.FromArgb(130, 130, 130);
            lineaSubrayado.Size = new Size(680, 3); // Ancho 680px, grosor 3px 

            // Calcular posición centrada y debajo del label
            int lineaX = (Home.Width - 680) / 2;
            int lineaY = label1.Location.Y + 30;
            lineaSubrayado.Location = new Point(lineaX, lineaY);

            // IMPORTANTE: Agregar al TabPage correcto
            Home.Controls.Add(lineaSubrayado);
            lineaSubrayado.BringToFront(); // Traer al frente por si está oculto


            // Segunda línea decorativa 
            Panel lineaSubrayado2 = new Panel();
            lineaSubrayado2.BackColor = Color.FromArgb(130, 130, 130);
            lineaSubrayado2.Size = new Size(600, 3);
            int linea2X = (Home.Width - 600) / 2;
            lineaSubrayado2.Location = new Point(linea2X, lineaY + 10); // 10px debajo de la primera
            Home.Controls.Add(lineaSubrayado2);
            lineaSubrayado2.BringToFront();





            //Page2


            // TÍTULO
            label2.Font = new Font("Rockwell", 14, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(130, 130, 130);
            label2.Location = new Point(90, 90); // Más a la izquierda (90) y más abajo (90)

            // Buscador
            label3.Font = new Font("Rockwell", 12, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(130, 130, 130);
            label3.Location = new Point(90, 450); // Más a la izquierda (90) y más abajo (90)



        }





        





    }
}
