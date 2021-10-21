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
        String[] palabras = { "hola", "adios", "DINT", "sdfsdfasdfasdfsad", "yucca", "PSP", "Apruebame" };
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
                button.Tag = letra.ToString();
                button.Click += Button_Click;

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

        public void CompruebaCoincidencia(String letra)
        {
            letrasPalabra = palabra.ToCharArray();
            if (palabra.Contains(letra))
            {
                foreach (TextBlock tb in PalabraWrapPanel.Children)
                {
                    if (tb.Tag.ToString() == letra) tb.Text = letra.ToUpper();
                }
            }
            else
            {
                intentos--;
                if (intentos == 0)
                {
                    TodoMal();
                }
            }
        }
        private void TodoMal()
        {
            foreach (Button boton in CuadriculaUniformGrid.Children)
            {
                boton.IsEnabled = false;
            }
            MessageBox.Show("Has perdido");
            int contador = 0;
            //muestra palabra
            letrasPalabra = palabra.ToCharArray();
            foreach (TextBlock tb in PalabraWrapPanel.Children)
            {
                tb.Text = letrasPalabra[contador].ToString().ToUpper();
                contador++;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String letra = (sender as Button).Tag.ToString();
            (sender as Button).IsEnabled = false;
            CompruebaCoincidencia(letra);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (Button boton in CuadriculaUniformGrid.Children)
            {
                if (e.Key.ToString().Contains(boton.Tag.ToString().ToUpper()))
                    boton.IsEnabled = false;
            }
            CompruebaCoincidencia(e.Key.ToString().ToLower());
        }
    }
}

