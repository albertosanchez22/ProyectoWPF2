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
    public partial class Dialogo : Window
    {
        public string numeroañadir { get; set; }
        public int capacidadañadir { get; set; }
        public bool disponibleañadir { get; set; }
        public Dialogo()
        {

            InitializeComponent();

        }

       

        private void aceptarañadirsala_Click(object sender, RoutedEventArgs e)
        {

            
            if (Sala.CompararSala(numerotextbox.Text) == false)
            {
                numeroañadir = numerotextbox.Text;
                capacidadañadir = int.Parse(capacidadtextbox.Text);
                disponibleañadir = (bool)disponiblecheck.IsChecked;
                DialogResult = true;
            }
        else MessageBox.Show("ERROR: El numero de sala ya existe , cambia el nombre");


            
        }

        private void cancelarañadirsala_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}