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
    class ConvertidorSala : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string titulodevolver = "";
            SqliteConnection conexion = new SqliteConnection("Data Source=peliculasbase.db");
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT numero FROM salas where idSala=@idSala";
            comando.Parameters.Add("@idSala", SqliteType.Integer);
            comando.Parameters["@idSala"].Value = (int)value;
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {


                while (lector.Read())
                {





                    titulodevolver = (string)lector["numero"];
                }
            }
            conexion.Close();
            return titulodevolver;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //No es necesario
            throw new NotImplementedException();
        }
    }
}
