using Microsoft.Data.Sqlite;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoWPF2
{
    class Sala : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        int idSala;
        string numero;
        int capacidad;
        bool disponible;

        public Sala(int idsala,string numero,int capacidad,bool disponible)
        {
            this.idSala = idsala;
            this.numero = numero;
            this.capacidad = capacidad;
         
                this.disponible = disponible;
            

        }

        public Sala()
        {

        }

        public int Id
        {
            get => idSala;
            set
            {


                idSala = value;
                NotifyPropertyChanged("IdSala");

            }
        }
        public string Numero
        {
            get => numero;
            set
            {


                numero = value;
                NotifyPropertyChanged("Numero");

            }
        }
        public int Capacidad
        {
            get => capacidad;
            set
            {


                capacidad = value;
                NotifyPropertyChanged("Capacidad");

            }
        }
        public bool Disponible
        {
            get => disponible;
            set
            {

                
                disponible = value;
                NotifyPropertyChanged("Disponible");

            }
        }

        public static ObservableCollection<Sala> getSamples()
        {
            ObservableCollection<Sala> lista =new ObservableCollection<Sala>();


            //añadirsalaSQL();
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM salas";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {

                while (lector.Read())
                {
                    int id = lector.GetInt32(0);
                    string numero = (string)lector["numero"];
                    int capacidad = lector.GetInt32(2);
                    bool disponible = lector.GetBoolean(0);
                    

                    lista.Add(new Sala(id,numero,capacidad,disponible));
                }
            }
            conexion.Close();
            return lista;



        }
        public static void actualizarsalaSQL(Sala sala)
        {
            //falta añadir el bool
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE  salas SET numero=@numero,capacidad=@capacidad,disponible=@disponible WHERE idSala=@idsala" ;

            comando.Parameters.Add("@numero", SqliteType.Text);
            comando.Parameters.Add("@capacidad", SqliteType.Integer);
            comando.Parameters.Add("@idsala", SqliteType.Integer);
            comando.Parameters.Add("@disponible", SqliteType.Integer);




            comando.Parameters["@idsala"].Value = sala.Id;
            comando.Parameters["@numero"].Value = sala.Numero;
            comando.Parameters["@capacidad"].Value = sala.Capacidad;
            comando.Parameters["@disponible"].Value = sala.Disponible;



            comando.ExecuteNonQuery();

            conexion.Close();

        }

        public static bool CompararSala(string numero)
        {
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM salas";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {

                while (lector.Read())
                {
                    if(numero.ToLower().Equals((string)lector["numero"]))
                    {
                        return true;
                    }


                    
                }
                
            }
            conexion.Close();
            return false;
           
        }
        public static void añadirsalaSQL(Sala sala)
        {
            //Falta añadir los bool bien sale siempre 0
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO salas(numero,capacidad,disponible) VALUES(@numero,@capacidad,@disponible)";
           
            comando.Parameters.Add("@numero", SqliteType.Text);
            comando.Parameters.Add("@capacidad", SqliteType.Integer);
            comando.Parameters.Add("@disponible", SqliteType.Integer);





            comando.Parameters["@numero"].Value = sala.Numero;
                comando.Parameters["@capacidad"].Value = sala.Capacidad;
            comando.Parameters["@disponible"].Value = sala.Disponible;



            comando.ExecuteNonQuery();
           
            conexion.Close();




        }
        private void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
