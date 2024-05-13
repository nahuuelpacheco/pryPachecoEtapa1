using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace pryPachecoEtapa1
{
    public partial class frmFirma : Form
    {
        public Bitmap Dibujo;
        public frmFirma()
        {
            InitializeComponent();
            Dibujo = new Bitmap(pctDibujo.Width, pctDibujo.Height);
            pctDibujo.Image = Dibujo;
        }
        bool Clickeando;    //sirve para saber si estamos clickeando, devuelve true o false
        Point CoordenadasMouse; //las coordenadas del mouse que nos sirven para dibujar

        private void frmFirma_Load(object sender, EventArgs e)
        {

        }

        private void btnDibujar_Click(object sender, EventArgs e)
        {
            //Activamos/desactivamos botones
            btnDibujar.Enabled = false;
            btnGuardar.Enabled = true;
        }

        private void pctDibujo_MouseDown(object sender, MouseEventArgs e)
        {
            Clickeando = true;
            CoordenadasMouse = e.Location; //Avisamos que estamos clickeando y damos coordenadas
        }

        private void pctDibujo_MouseUp(object sender, MouseEventArgs e)
        {
            Clickeando = false; //Avisamos que no esta clickeando el usuario
        }

        private void pctDibujo_MouseMove(object sender, MouseEventArgs e)
        {
            if (Clickeando == true) //Si clickeamos
            {
                using (Graphics g = pctDibujo.CreateGraphics()) //Creamos donde dibujar
                {
                    using (Pen Lapiz = new Pen(Color.Green, 3)) //Definimos nuestro lapiz
                    {
                        g.DrawLine(Lapiz, CoordenadasMouse, e.Location); //Dibujamos linea teniendo en cuenta el lapiz y coordenadas del mouse
                        CoordenadasMouse = e.Location;  //Sin esto no se movería el lapiz correctamente

                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta = Path.Combine(Application.StartupPath, "Firmas"); //Donde se inicia la app + la carpeta
                if (!Directory.Exists(ruta)) //si no existe se crea
                {
                    Directory.CreateDirectory(ruta);
                }
                string archivo = $"{DateTime.Now.ToString("yyyy-MM-dd hhmmss")}.png"; //nombre del archivo es el dia y hora
                string destino = Path.Combine(ruta, archivo); //Combinamos ruta y nomb del archivo
                Dibujo.Save(destino, System.Drawing.Imaging.ImageFormat.Png); //Se guarda
                MessageBox.Show("¡Firma guardada con exito!");
                pctDibujo.Invalidate();  //limpiamos dibujo                  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
    }
}    

