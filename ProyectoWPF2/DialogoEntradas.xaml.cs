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
    public partial class DialogoEntradas : Window
    {
      
        public int cantidadentradas { get; set; }
        public string formadepago { get; set; }
        
        public DialogoEntradas()
        {

            InitializeComponent();
            idsesioncomprar.Text = Properties.Settings.Default.idsesioncomprar.ToString();

        }

       

        private void aceptarañadirsala_Click(object sender, RoutedEventArgs e)
        {
            bool comp = false;
            
           while(comp==false)
            {
                if (bizumcheck.IsChecked == false && efectivocheck.IsChecked == false && tarjetacheck.IsChecked == false)
                {
                    alertatextbox.Text = "Error añade forma de pago";
                }
                else { alertatextbox.Text = ""; comp = true; }
            }
            
            if (Sesion.CompararAforoSQL(int.Parse(cantidadtextbox.Text),int.Parse(idsesioncomprar.Text))==false)
            {

                cantidadentradas = int.Parse(cantidadtextbox.Text);
                if(bizumcheck.IsChecked==true)
                {
                    formadepago = "Bizum";
                }else if(efectivocheck.IsChecked==true)
                {
                    formadepago = "Efectivo";
                }else if(tarjetacheck.IsChecked==true)
                {
                    formadepago = "Tarjeta";
                }
                DialogResult = true;
            }
            
            







        }

        private void cancelarañadirsala_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}