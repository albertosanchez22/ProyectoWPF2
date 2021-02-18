using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProyectoWPF2.Conversores
{
    class IdPeliculaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string titulodevolver="";
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT titulo FROM peliculas where idPelicula=@idPelicula";
            comando.Parameters.Add("@idPelicula", SqliteType.Integer);
            comando.Parameters["@idPelicula"].Value = (string)value;
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {


                while (lector.Read())
                {





                     titulodevolver = (string)lector["titulo"];
                }
            }
            conexion.Close();
            return titulodevolver;
            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
