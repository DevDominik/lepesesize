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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> jomezokLista = new List<string>();
        List<string> helyFoglalas = new List<string>();
        List<string> lyukakHelye = new List<string>();
        Random random = new Random();
        const int MEGADOTTLYUKAKSZAMA = 10;
        const int LEPOKSZAMA = 50;
        const int OSZLOPOK = 25;
        const int SOROK = 25;
        public MainWindow()
        {
            InitializeComponent();
            grdAlap.Background = new SolidColorBrush(Color.FromRgb(30, 150, 50));
            for (int i = 0; i < OSZLOPOK; i++)
            {
                grdAlap.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < SOROK; i++)
            {
                grdAlap.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < OSZLOPOK; i++)
            {
                for (int j = 0; j < SOROK; j++)
                {
                    jomezokLista.Add($"{i};{j}");
                    Label label = new();
                    label.Content = $"{i + 1};{j + 1}";
                    label.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    label.HorizontalContentAlignment = HorizontalAlignment.Center;
                    label.VerticalContentAlignment = VerticalAlignment.Center;
                    Grid.SetColumn(label, i);
                    Grid.SetRow(label, j);
                    grdAlap.Children.Add(label);
                }
            }
            int eredetiMeret = jomezokLista.Count;
            while (jomezokLista.Count > eredetiMeret - MEGADOTTLYUKAKSZAMA)
            {
                int kivalasztottIndex = random.Next(0, jomezokLista.Count);
                string kivalasztott = jomezokLista[kivalasztottIndex];
                lyukakHelye.Add(kivalasztott);
                string[] bontott = kivalasztott.Split(";");
                Ellipse ellipse = new Ellipse();
                Grid.SetColumn(ellipse, int.Parse(bontott[0]));
                Grid.SetRow(ellipse, int.Parse(bontott[1]));
                ellipse.Fill = new SolidColorBrush(Color.FromRgb(190, 50, 10));
                ellipse.Width = 20;
                ellipse.Height = 20;
                ellipse.Opacity = 0.7;
                grdAlap.Children.Add(ellipse);
                jomezokLista.RemoveAt(kivalasztottIndex);
            }
            eredetiMeret = jomezokLista.Count;
            while (jomezokLista.Count > eredetiMeret - LEPOKSZAMA)
            {
                int kivalasztottIndex = random.Next(0, jomezokLista.Count);
                string kivalasztott = jomezokLista[kivalasztottIndex];
                helyFoglalas.Add(kivalasztott);
                string[] bontott = kivalasztott.Split(";");
                Rectangle rectangle = new Rectangle();
                Grid.SetColumn(rectangle, int.Parse(bontott[0]));
                Grid.SetRow(rectangle, int.Parse(bontott[1]));
                rectangle.Fill = new SolidColorBrush(Color.FromRgb(130, 130, 20));
                rectangle.Opacity = 0.9;
                grdAlap.Children.Add(rectangle);
                jomezokLista.RemoveAt(kivalasztottIndex);
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<int> indexek = new List<int>();
            foreach (var item in grdAlap.Children)
            {
                if (item is Label)
                {
                    Label elem = item as Label;
                    int[] koordinatak = { Grid.GetColumn(elem), Grid.GetRow(elem) };
                    int[] kivalasztottKoordinatak = { Grid.GetColumn(elem), Grid.GetRow(elem) };
                    int? cserelendoIndexMezok, cserelendoIndexHely;
                    for (int i = 0; i < helyFoglalas.Count; i++)
                    {
                        if (helyFoglalas[i] == $"{koordinatak[0]};{koordinatak[1]}")
                        {
                            cserelendoIndexHely = i;
                            break;
                        }
                    }
                    if ()
                    {

                    }
                    

                }
            }
            
        }
    }
}
