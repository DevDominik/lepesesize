using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public class Babu
    {
        private Point koordinata;
        private int lepettMezokSzama = 0;
        private List<Point> feketeLista = new List<Point>();
        private List<Point> feherLista = new List<Point>();

        public Babu(Point koordinata) { 
            this.koordinata = koordinata;
        }

        public void Lep(Point koordinata) {
            this.koordinata = koordinata;
        }
        public Point Koordinata { get { return koordinata; } }
    }
    public class LepesSiker
    {
        private bool sikeres;
        private Point probalt;

        public LepesSiker(bool sikeres, Point probalt)
        {
            this.sikeres = sikeres;
            this.probalt = probalt;
        }

        public bool Sikeres { get {  return sikeres; } }
        public Point Probalt { get {  return probalt; } }
    }
}
