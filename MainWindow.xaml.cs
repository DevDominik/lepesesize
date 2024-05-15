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
        List<Mezo> mezokLista = new List<Mezo>();
        List<Babu> babukLista = new List<Babu>();
        Random random = new Random();
        const int MEGADOTTLYUKAKSZAMA = 10;
        const int BABUKSZAMA = 50;
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
                    mezokLista.Add(new Mezo(new Point(i, j)));
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
            int eredetiMeret = mezokLista.Count;
            List<Mezo> masolatMezo = new List<Mezo>();
            foreach (var item in mezokLista)
            {
                masolatMezo.Add(item);
            }
            while (masolatMezo.Count > eredetiMeret - MEGADOTTLYUKAKSZAMA)
            {
                int kivalasztottIndex = random.Next(0, masolatMezo.Count);
                Mezo kivalasztott = masolatMezo[kivalasztottIndex];
                Ellipse ellipse = new Ellipse();
                Grid.SetColumn(ellipse, int.Parse(kivalasztott.Koordinata.X.ToString()));
                Grid.SetRow(ellipse, int.Parse(kivalasztott.Koordinata.Y.ToString()));
                ellipse.Fill = new SolidColorBrush(Color.FromRgb(190, 50, 10));
                ellipse.Width = 20;
                ellipse.Height = 20;
                ellipse.Opacity = 0.7;
                grdAlap.Children.Add(ellipse);
                mezokLista[kivalasztottIndex].MegmaradtLepesek = 0;
                masolatMezo.RemoveAt(kivalasztottIndex);
            }
            eredetiMeret = masolatMezo.Count;
            while (masolatMezo.Count > eredetiMeret - BABUKSZAMA)
            {
                int kivalasztottIndex = random.Next(0, masolatMezo.Count);
                Mezo kivalasztott = masolatMezo[kivalasztottIndex];
                babukLista.Add(new Babu(kivalasztott.Koordinata));
                Rectangle rectangle = new Rectangle();
                Grid.SetColumn(rectangle, int.Parse(kivalasztott.Koordinata.X.ToString()));
                Grid.SetRow(rectangle, int.Parse(kivalasztott.Koordinata.Y.ToString()));
                rectangle.Fill = new SolidColorBrush(Color.FromRgb(130, 130, 20));
                rectangle.Opacity = 0.9;
                grdAlap.Children.Add(rectangle);
                masolatMezo.RemoveAt(kivalasztottIndex);
            }
        }
        private LepesSiker BabuLeptetes(Babu babu, List<Point> koordinatak)
        {
            bool sikeres = false;
            Point point = babu.Koordinata;
            switch (random.Next(0, 2)) // koordináta
            {
                case 0: // x
                    switch (random.Next(0, 2))
                    {
                        case 0: // -1
                            point.X -= 1;
                            break;
                        case 1: // +1
                            point.X += 1;
                            break;
                    }
                    break;

                case 1: // y
                    switch (random.Next(0, 2))
                    {
                        case 0: // -1
                            point.Y -= 1;
                            break;
                        case 1: // +1
                            point.Y += 1;
                            break;
                    }
                    break;
            }
            if (!koordinatak.Contains(point))
            {
                sikeres = true;
                babu.Lep(point);
            }
            return new LepesSiker(sikeres, point);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<int> indexek = new List<int>();
            foreach (Babu babu in babukLista)
            {
                List<Point> koordinatak = new List<Point>();
                foreach (Babu item in babukLista)
                {
                    if (item.Koordinata.X != babu.Koordinata.X || item.Koordinata.Y != babu.Koordinata.Y)
                    {
                        koordinatak.Add(item.Koordinata);
                    }
                }
                List <LepesSiker> probalkozott = new List<LepesSiker>();
                bool kilephet = false;
                while (probalkozott.Count < 4 && !kilephet)
                {
                    LepesSiker probalt = BabuLeptetes(babu, koordinatak);
                    kilephet = probalt.Sikeres;
                    if (!kilephet)
                    {
                        bool talalt = false;
                        foreach (LepesSiker marvolt in probalkozott)
                        {
                            if (marvolt.Probalt.X == probalt.Probalt.X && marvolt.Probalt.Y == probalt.Probalt.Y)
                            {
                                talalt = true;
                            }
                        }
                        if (!talalt)
                        {
                            probalkozott.Add(probalt);
                        }
                    }
                }

                Mezo kivalasztottMezo = mezokLista.Where(m => m.Koordinata.X == babu.Koordinata.X && m.Koordinata.Y == babu.Koordinata.Y).First();
                if (kivalasztottMezo != null && !kivalasztottMezo.LehetLepni)
                {
                    int kapottIndex = babukLista.FindIndex(b => b.Koordinata.X == babu.Koordinata.X && b.Koordinata.Y == babu.Koordinata.Y);
                    if (kapottIndex > -1)
                    {
                        indexek.Add(kapottIndex);
                    }
                }
            }
            
            foreach (int index in indexek)
            {
                MessageBox.Show($"Lékre lépett: {babukLista[index].Koordinata.X};{babukLista[index].Koordinata.Y} | {index}");
                babukLista.RemoveAt(index);
            }
            
        }
    }
}