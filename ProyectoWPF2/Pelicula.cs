using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProyectoWPF2
{
    class Pelicula : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private string titulo;
        private string cartel;
        private int año;
        private string genero;
        private string calificacion;

        public Pelicula(int id,string titulo,string cartel,int año,string genero,string calificacion)
        {
            this.id = id;
            this.titulo = titulo;
            this.cartel = cartel;
            this.año = año;
            this.genero = genero;
            this.calificacion = calificacion;
        }
        public string Calificacion
        {
            get => calificacion;
            set
            {
                
                
                    calificacion = value;
                    NotifyPropertyChanged("Calificacion");
                
            }
        }
        public string Genero
        {
            get => genero;
            set
            {
               
                
                    genero = value;
                    NotifyPropertyChanged("Genero");
                
            }
        }
        public int Año
        {
            get => año;
            set
            {
               
                
                    año = value;
                    NotifyPropertyChanged("Año");
                
            }
        }

        public int Id
        {
            get => id;
            set
            {
              
                
                    id = value;
                    NotifyPropertyChanged("Id");
                
            }
        }
        public string Titulo
        {
            get => titulo;
            set
            {
                
                
                    titulo = value;
                    NotifyPropertyChanged("Titulo");
                
            }
        }
        public string Cartel
        {
            get => cartel;
            set
            {
                
                
                    cartel = value;
                    NotifyPropertyChanged("Cartel");
                
            }
        }

        public static ObservableCollection<Pelicula> getSamples()
        {
            ObservableCollection<Pelicula> lista;
            if (Properties.Settings.Default.buscado!=DateTime.Today)
            {
                //eliminamos peliculas de la base de datos y las añadimos de nuevo(ahorramos comparaciones)
                eliminarPeliculasSQL();
                var client = new RestClient(Properties.Settings.Default.endpoint);
                var request = new RestRequest("peliculas", Method.GET);
                var response = client.Execute(request);
                Properties.Settings.Default.buscado = DateTime.Today;
                Properties.Settings.Default.Save();
                lista = JsonConvert.DeserializeObject<ObservableCollection<Pelicula>>(response.Content);
                añadirPeliculasSQL(lista);
                return selectPeliculasSQL();
            }
            //tiene que devolver las pelis de la base de datos
            return selectPeliculasSQL();
           


        }

       public static void eliminarPeliculasSQL()
        {
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText="DELETE FROM peliculas";
            comando.ExecuteNonQuery();
            conexion.Close();
        }
     
        public static ObservableCollection<Pelicula> selectPeliculasSQL()
        {
            ObservableCollection<Pelicula> lista = new ObservableCollection<Pelicula>(); ;
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM peliculas";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
               
                while (lector.Read())
                 {
                    int id = lector.GetInt32(0);
                    string titulo = (string)lector["titulo"];
                    string cartel = (string)lector["cartel"];
                    int año = lector.GetInt32(3);
                    string genero = (string)lector["genero"];
                    string calificacion = (string)lector["calificacion"];
                    lista.Add(new Pelicula(id,titulo,cartel,año,genero,calificacion));
                }
            }
            conexion.Close();
            return lista;
                
        
        
           
        }
        public static void añadirPeliculasSQL(ObservableCollection<Pelicula> lista)
        {
            /* idPelicula   INTEGER PRIMARY KEY,
                                  titulo       TEXT,
                                    cartel       TEXT,
                                       año          INTEGER,
                                      genero       TEXT,
                                     calificacion TEXT*/
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO peliculas VALUES(@id,@titulo,@cartel,@año,@genero,@calificacion)";
            comando.Parameters.Add("@id",SqliteType.Integer);
            comando.Parameters.Add("@titulo", SqliteType.Text);
            comando.Parameters.Add("@cartel", SqliteType.Text);
            comando.Parameters.Add("@año", SqliteType.Integer);
            comando.Parameters.Add("@genero", SqliteType.Text);
            comando.Parameters.Add("@calificacion", SqliteType.Text);

            foreach(Pelicula c in lista)
            {
                comando.Parameters["@id"].Value = c.Id;
                comando.Parameters["@titulo"].Value = c.Titulo;
                comando.Parameters["@cartel"].Value = c.Cartel;
                comando.Parameters["@año"].Value = c.Año;
                comando.Parameters["@genero"].Value = c.Genero;
                comando.Parameters["@calificacion"].Value = c.Calificacion;
                comando.ExecuteNonQuery();
            }
            conexion.Close();




        }

        private void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
