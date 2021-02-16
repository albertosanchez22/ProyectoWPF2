﻿using Microsoft.Data.Sqlite;
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
    public partial class MainWindow : Window
    {
        ObservableCollection<Pelicula> listapeliculas;
        ObservableCollection<Sala> listasalas;

        public MainWindow()
        {
            if(Properties.Settings.Default.basecreada==false)
            {
                CrearSQL();
            }



            
            InitializeComponent();
            listasalas = Sala.getSamples();
            listapeliculas = Pelicula.getSamples();

            peliculaslistbox.DataContext = listapeliculas;
            salaslistbox.DataContext = listasalas;
        }
       

        public void CrearSQL()
        {
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            
            
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = @"DROP TABLE IF EXISTS peliculas;
                                    CREATE TABLE peliculas (
                                idPelicula   INTEGER PRIMARY KEY,
                                  titulo       TEXT,
                                    cartel       TEXT,
                                       año          INTEGER,
                                      genero       TEXT,
                                     calificacion TEXT
                                    );  

                                         DROP TABLE IF EXISTS salas;
                                        CREATE TABLE salas (
                                      idSala     INTEGER PRIMARY KEY AUTOINCREMENT,
                                                 numero     TEXT,
                                            capacidad  INTEGER,
                                            disponible BOOLEAN DEFAULT (true) 
                                        );



                                                DROP TABLE IF EXISTS sesiones;
                                                CREATE TABLE sesiones (
                                                    idSesion INTEGER PRIMARY KEY AUTOINCREMENT,
                                                    pelicula INTEGER REFERENCES peliculas (idPelicula),
                                                    sala     INTEGER REFERENCES salas (idSala),
                                                    hora     TEXT
                                                );


                                                    DROP TABLE IF EXISTS ventas;
                                                CREATE TABLE ventas (
                                                    idVenta  INTEGER PRIMARY KEY AUTOINCREMENT,
                                                    sesion   INTEGER REFERENCES sesiones (idSesion),
                                                    cantidad INTEGER,
                                                    pago     TEXT
                                                );";


                                        



            comando.ExecuteNonQuery();
            conexion.Close();
            Properties.Settings.Default.basecreada = true;
            Properties.Settings.Default.Save();

        }

       

        private void botonañadirsala_Click(object sender, RoutedEventArgs e)
        {
            Dialogo dialogo = new Dialogo();
            dialogo.Owner = this;
            if (dialogo.ShowDialog() == true)
            {
                Sala sala = new Sala();
                sala.Numero = dialogo.numeroañadir;
                sala.Capacidad = dialogo.capacidadañadir;

                sala.Disponible = dialogo.disponibleañadir;
                
               
                if (Sala.CompararSala(sala.Numero) == false)
                {
                    Sala.añadirsalaSQL(sala);
                    listasalas.Add(sala);
                    salaslistbox.DataContext = null;
                    salaslistbox.DataContext = listasalas;
                }else MessageBox.Show("ERROR: El numero de sala ya existe , cambia el nombre");


            }
        }

        private void Actualizar_Executed(object sender, ExecutedRoutedEventArgs e)
        {

           

            Sala sala = new Sala();
            sala.Id = int.Parse(idsalatexbox.Text);
            sala.Numero = numerotexbox.Text;
            sala.Capacidad = int.Parse(capacidadtexbox.Text);
            sala.Disponible = bool.Parse(disponibletexbox.Text);
            
                Sala.actualizarsalaSQL(sala);

                listasalas = Sala.getSamples();
                salaslistbox.DataContext = null;
                salaslistbox.DataContext = listasalas;
              
                
          

        }

        private void Actualizar_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

            if (listasalas != null && salaslistbox.SelectedItem == null )
            {
                e.CanExecute = false;
            }
            else e.CanExecute = true;
        }
    }
}
