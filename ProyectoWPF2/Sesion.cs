using Microsoft.Data.Sqlite;
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
    class Sesion : INotifyPropertyChanged
    {

        int idSesion;
        int pelicula;
        int sala;
        string hora;

        public Sesion(int idsesion,int pelicula,int sala,string hora)
        {
            this.idSesion = idsesion;
            this.pelicula = pelicula;
            this.sala = sala;
            this.hora = hora;
        }
        
        public Sesion()
        {

        }


        public int Id
        {
            get => idSesion;
            set
            {


                idSesion = value;
                NotifyPropertyChanged("IdSesion");

            }
        }

        public int Pelicula
        {
            get => pelicula;
            set
            {


                pelicula = value;
                NotifyPropertyChanged("Pelicula");

            }
        }

        public int Sala
        {
            get => sala;
            set
            {


                sala = value;
                NotifyPropertyChanged("Sala");

            }
        }

        public string Hora
        {
            get => hora;
            set
            {


                hora = value;
                NotifyPropertyChanged("Hora");

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;



        private void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public static ObservableCollection<Sesion> getSamples()
        {
            ObservableCollection<Sesion> lista = new ObservableCollection<Sesion>();

            
            // añadirsesionSQL(new Sesion(0, 1, 0, "10:30"));
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM sesiones";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {


                while (lector.Read())
                {
                    int id = lector.GetInt32(0);
                    int pelicula = lector.GetInt32(1);
                    int sala = lector.GetInt32(2);
                    string hora = (string)lector["hora"];
                  


                    lista.Add(new Sesion(id, pelicula, sala, hora));
                }
            }
            conexion.Close();
            return lista;



        }

        public static void actualizarsesionSQL(Sesion sesion)
        {
            //falta añadir el bool
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE  sesiones SET pelicula=@pelicula,sala=@sala,hora=@hora WHERE idSesion=@idsesion";

            comando.Parameters.Add("@idsesion", SqliteType.Integer);
            comando.Parameters.Add("@pelicula", SqliteType.Integer);
            comando.Parameters.Add("@sala", SqliteType.Integer);
            comando.Parameters.Add("@hora", SqliteType.Text);




            comando.Parameters["@idsesion"].Value = sesion.Id;
            comando.Parameters["@pelicula"].Value = sesion.Pelicula;
            comando.Parameters["@sala"].Value = sesion.Sala;
            comando.Parameters["@hora"].Value = sesion.Hora;



            comando.ExecuteNonQuery();

            conexion.Close();
        }

        public static void eliminarsesionSQL(string id)
        {
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM  sesiones WHERE idSesion=@idsesion";

            comando.Parameters.Add("@idsesion", SqliteType.Integer);
            comando.Parameters["@idsesion"].Value = int.Parse(id);

            comando.ExecuteNonQuery();

            conexion.Close();

        }
        public static bool CompararSesion(int sala)
        {

            //Sala disponible
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();

            comando.CommandText = "SELECT disponible FROM sesiones where sala=@sala";
            comando.Parameters.Add("@sala", SqliteType.Integer);
            comando.Parameters["@sala"].Value = sala;
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {

                while (lector.Read())
                {


                    if ((lector["disponible"].Equals("false")))
                    {
                        MessageBox.Show("ERROR: Sala no disponible");
                        return true;
                    }

                }


            }
            conexion.Close();
            if (ComprararSesion(sala))
            {
                return true;
            }
            else return false;
        }
        public static bool ComprararSesion(int sala)
        {
            //COMPARA SESIONES
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();

            comando.CommandText = "SELECT COUNT(idSesion) FROM sesiones where sala=@sala";
            comando.Parameters.Add("@sala", SqliteType.Integer);
            comando.Parameters["@sala"].Value =sala;
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {

                while (lector.Read())
                {


                    if (int.Parse((lector["COUNT(idSesion)"]).ToString())>=3)
                    {
                        MessageBox.Show("ERROR: Supera el numero maximo de sesiones permitidas");
                        return true;
                    }

                }


            }
            conexion.Close();
            return false;
        }


        public static bool CompararAforoSQL(int cantidadcomprar,int idsesion)
        {

            
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();

            comando.CommandText = "SELECT cantidad,capacidad FROM ventas join salas on ventas.sesion=salas.idsala where sesion=@sesion";
            comando.Parameters.Add("@sesion", SqliteType.Integer);
            comando.Parameters["@sesion"].Value = idsesion;
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {

                while (lector.Read())
                {


                    if ((int.Parse((lector["cantidad"]).ToString())+cantidadcomprar) >= int.Parse((lector["capacidad"]).ToString()))
                    {
                        MessageBox.Show("ERROR: Supera el numero maximo de capacidad de la sala");
                        return true;
                    }

                }


            }
            conexion.Close();
            return false;
        }
        public static void añadirsesionSQL(Sesion sesion)
        {
            
          
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO sesiones(pelicula,sala,hora) VALUES(@pelicula,@sala,@hora)";

            comando.Parameters.Add("@pelicula", SqliteType.Integer);
            comando.Parameters.Add("@sala", SqliteType.Integer);
            comando.Parameters.Add("@hora", SqliteType.Text);





            comando.Parameters["@pelicula"].Value = sesion.Pelicula;
            comando.Parameters["@sala"].Value = sesion.Sala;
            comando.Parameters["@hora"].Value = sesion.Hora;



            comando.ExecuteNonQuery();

            conexion.Close();
        }

    }
}
