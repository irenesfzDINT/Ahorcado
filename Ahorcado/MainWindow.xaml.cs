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
        Char[] abecedario = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        String[] palabras = { "hola", "adios", "DINT", "sdfsdfasdfasdfsad" };
        String palabra = null;
        Char[] letrasPalabra = null;
        Random rn = new Random();
        int intentos = 10;
        public MainWindow()
        {
            InitializeComponent();
            CreaBotones();
            palabra = EligePalabra();
            CreaGuiones(palabra);
        }
        private void CreaBotones()
        {
            foreach (char letra in abecedario)
            {
                Button button = new Button();
                button.Content = letra.ToString().ToUpper();
                button.Tag = letra.ToString().ToUpper();
                button.Click += Button_Click;
                button.KeyUp += Button_KeyUp;
                CuadriculaUniformGrid.Children.Add(button);
            }
        }
        private void CreaGuiones(String palabra)
        {
            letrasPalabra = palabra.ToCharArray();
            int numeroLetras = palabra.Length;
            for (int i = 0; i < numeroLetras; i++)
            {
                TextBlock texto = new TextBlock();
                //texto.Name = "textoTextBlock";
                texto.Text = "_";
                texto.FontSize = 100;
                texto.Tag = letrasPalabra[i].ToString();
                texto.Margin = new Thickness(15);
                PalabraWrapPanel.Children.Add(texto);
            }
        }
        private String EligePalabra()
        {
            palabra = palabras[rn.Next(0, palabras.Length)];
            return palabra;
        }
        private void Button_KeyUp(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void CompruebaCoincidencia(String letra)
        {
            letrasPalabra = palabra.ToCharArray();

            if (palabra.Contains(letra))
            {
            }
            else
            {
                
                intentos--;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String letra  = (sender as Button).Tag.ToString();
            letrasPalabra = palabra.ToCharArray();

            if (palabra.Contains(letra))
            {

            }
            else
            {

                intentos--;
            }
        }
    }
}

