using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public class Mezo
    {
        private Point koordinata;
        private int megmaradtLepesek = 10;
        public Mezo(Point koordinata, bool isLyuk = false)
        {
            this.koordinata = koordinata;
            if (isLyuk)
            {
                megmaradtLepesek = 0;
            }
        }
        public Point Koordinata { get { return koordinata; } }
        public int MegmaradtLepesek { get { return megmaradtLepesek; } set { megmaradtLepesek = value; } }
        public bool LehetLepni { get { return megmaradtLepesek > 0; } }
    }
}
