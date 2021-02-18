using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProyectoWPF2
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand
             ("Salir", "Exit", typeof(CustomCommands),
             new InputGestureCollection() { new KeyGesture(Key.S, ModifierKeys.Control) });

        public static readonly RoutedUICommand Actualizar = new RoutedUICommand
            ("Actualizar", "Actualizar", typeof(CustomCommands),
            new InputGestureCollection() { new KeyGesture(Key.D, ModifierKeys.Control) });

        public static readonly RoutedUICommand ActualizarSesion = new RoutedUICommand
            ("ActualizarSesion", "ActualizarSesion", typeof(CustomCommands),
            new InputGestureCollection() { new KeyGesture(Key.D, ModifierKeys.Control) });

        public static readonly RoutedUICommand Eliminar = new RoutedUICommand
           ("Eliminar", "Eliminar", typeof(CustomCommands),
           new InputGestureCollection() { new KeyGesture(Key.E, ModifierKeys.Control) });
    }
}
