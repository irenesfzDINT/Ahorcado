using System;
using System.Collections.Generic;
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

namespace Ahorcado
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            for (int filas = 0; filas < 4; filas++)
            {
                for (int columnas = 0; columnas < 10; columnas++)
                {
                    Viewbox viewbox = new Viewbox();
                    Label label = new Label();
                    Button button = new Button();

                    label.Content = "e";
                    viewbox.Child = label;
                    button.Content = viewbox;

                    Grid.SetRow(button, filas);
                    Grid.SetColumn(button, columnas);
                    CuadriculaUniformGrid.Children.Add(button);
                }
            }
            
        }
    }
}
