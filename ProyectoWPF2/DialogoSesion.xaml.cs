using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoWPF2
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class DialogoSesion : Window
    {
        public int salaañadir { get; set; }
        public int peliculaañadir { get; set; }
        public string horañadir { get; set; }
        public DialogoSesion()
        {

            InitializeComponent();

        }

      

        private void aceptarañadirsesion_Click(object sender, RoutedEventArgs e)
        {
            //Comprobacion de pelicula y sala que existan y devuelvan sus respectivos ids.
            
            if (Sesion.ComprararSesion(int.Parse(salaañadirtextbox.Text))==false)
            {
                salaañadir = int.Parse(salaañadirtextbox.Text);
                peliculaañadir = int.Parse(peliculaañadirtextbox.Text);
                horañadir = horaañadirtextbox.Text;
                DialogResult = true;
            }
        else MessageBox.Show("ERROR: El numero de sala ya existe , cambia el nombre");


            
        }

        private void cancelarañadirsesion_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}