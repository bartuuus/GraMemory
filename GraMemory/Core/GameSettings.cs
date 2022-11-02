using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraMemory.Core
{
   public class GameSettings
    {
        public int CzasGry { get; private set; }
        public int CzasPodgladu { get; private set; }
        public int MaxPunkty { get; private set; }
        public int Wiersze { get; private set; }
        public int Kolumny { get; private set; }
        public int Bok { get; private set; }
        public int AktualnePunkty { get; private set; }

        public string PlikLogo => $"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "logo.jpg")}";
        public string FolderObrazki => $"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img", "memory")}";
        public GameSettings()
        {
            UstawStartowe();
        }

        public void UstawStartowe()
        {
            CzasPodgladu = 5;
            CzasGry = 60;
            MaxPunkty = 0;
            Wiersze = 4;
            Kolumny = 6;
            Bok = 150;
            AktualnePunkty = 0;
            


        }


    }
}
