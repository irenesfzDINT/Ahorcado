using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Ahorcado
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char[] abecedario = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        string[] palabras = { "hola", "adios", "dint", "uwu", "electroencefalografista", "esternocleidomastoideo", "arroz", "alpaca", "hemiplejia" };
        string palabra = null;
        char[] letrasPalabra = null;
        Random rn = new Random();
        int intentosGastados = 0;
        public MainWindow()
        {
            InitializeComponent();
            CreaBotones();
            CreaJuego();
        }
        private void CreaBotones()
        {
            foreach (char letra in abecedario)
            {
                Button button = new Button();
                Viewbox vb = new Viewbox();
                Label lb = new Label();
                lb.Content = letra.ToString().ToUpper();
                vb.Child = lb;
                button.Content = vb;
                button.Tag = letra.ToString();
                button.Style = (Style)Resources["BotonesDinamicos"];
                CuadriculaUniformGrid.Children.Add(button);
            }
        }
        public void CreaJuego()
        {
            IntentosTextBlock.Text = "Intentos disponibles -> 10";
            RendirseButton.IsEnabled = true;
            //reinicia intentos
            intentosGastados = 0;
            //borra la palabra
            PalabraWrapPanel.Children.Clear();
            //reinicia imagen
            CambiaImagen(0);
            //elige palabra random
            palabra = EligePalabra();
            //muestra palabras
            CreaGuiones(palabra);
        }
        private void CambiaImagen(int numeroImagen)
        {
            //oculto todas las imágenes
            foreach (Image img1 in ImagenStackPanel.Children)
            {
                img1.Visibility = Visibility.Collapsed;
            }
            //muestro la imagen que quiera
            Image img = (Image)ImagenStackPanel.Children[numeroImagen];
            img.Visibility = Visibility.Visible;

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
            if (palabra.Contains(letra.ToLower()))
            {
                foreach (TextBlock tb in PalabraWrapPanel.Children)
                {
                    if (tb.Tag.ToString() == letra) tb.Text = letra.ToUpper();
                }
                JuegoGanado();
            }
            else
            {
                intentosGastados++;
                IntentosTextBlock.Text = "Intentos disponibles -> " + (10 - intentosGastados).ToString();
                CambiaImagen(intentosGastados);
                if (intentosGastados == 10)
                {
                    JuegoPerdido();
                }
            }
        }
        private void JuegoGanado()
        {
            bool coincide = true;
            for (int i = 0; i < letrasPalabra.Length; i++)
            {
                TextBlock tb = (TextBlock)PalabraWrapPanel.Children[i];
                if (tb.Text == "_")
                {
                    coincide = false;
                    break;
                }
            }
            if (coincide)
            {
                MessageBox.Show("Has ganado", "Victoria", MessageBoxButton.OK, MessageBoxImage.Information);
                BotonesHabilitados(false);
                RendirseButton.IsEnabled = false;
            }
        }

        private void JuegoPerdido()
        {
            BotonesHabilitados(false);
            CambiaImagen(10);
            MessageBox.Show("Has perdido", "Derrota", MessageBoxButton.OK, MessageBoxImage.Error);
            int contador = 0;
            //muestra palabra
            letrasPalabra = palabra.ToCharArray();
            foreach (TextBlock tb in PalabraWrapPanel.Children)
            {
                tb.Text = letrasPalabra[contador].ToString().ToUpper();
                contador++;
            }
            RendirseButton.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string letra = (sender as Button).Tag.ToString();
            (sender as Button).IsEnabled = false;
            CompruebaCoincidencia(letra);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            int keyEnum = (int)e.Key;
            foreach (Button boton in CuadriculaUniformGrid.Children)
            {
                if (keyEnum == 146) //la tecla Ñ
                {
                    if (boton.Tag.ToString().ToUpper() == "Ñ" && boton.IsEnabled)
                    {
                        CompruebaCoincidencia(e.Key.ToString().ToLower());
                        boton.IsEnabled = false;
                    }
                }
                else
                {   //si palabra contiene la tecla pulsada y el botón de la tecla esta enabled
                    if (e.Key.ToString().ToLower().Contains(boton.Tag.ToString()) && boton.IsEnabled)
                    {
                        CompruebaCoincidencia(e.Key.ToString().ToLower());
                        boton.IsEnabled = false;
                    }
                }
            }


        }
        public void BotonesHabilitados(bool botonHabilitado)
        {
            foreach (Button boton in CuadriculaUniformGrid.Children)
            {
                boton.IsEnabled = botonHabilitado;
            }
        }
        private void NuevaPartida_Click(object sender, RoutedEventArgs e)
        {
            BotonesHabilitados(true);
            CreaJuego();
        }
        private void Rendirse_Click(object sender, RoutedEventArgs e)
        {
            JuegoPerdido();
        }
    }
}