using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraMemory.Core
{
    public static class CardsGenerator
    {

       public static void GenerujKarty(GameSettings settings, Panel panelKart)
        {
            var tablicaElementow = Directory.GetFiles(settings.FolderObrazki);

            settings.MaxPunkty = tablicaElementow.Length;
            List<MemoryCard> karty = new List<MemoryCard>();

            foreach (var element in tablicaElementow)
            {
                var karta = new MemoryCard(Guid.NewGuid(), settings.PlikLogo, element);
                karty.Add(karta);

                var kartaDoPary = new MemoryCard(karta.Id, settings.PlikLogo, element);
                karty.Add(kartaDoPary);
            }

            panelKart.Controls.Clear();
            Random rand = new Random();

            for(int x = 0; x <settings.Kolumny; x++)
            {
                for(int y = 0; y < settings.Wiersze; y++)
                {
                    var losowyIndex = rand.Next(0, karty.Count());
                    var wybranaKarta = karty[losowyIndex];

                    int margines = 2;
                    wybranaKarta.Location = new System.Drawing.Point(
                        (x*settings.Bok) + (margines + x), 
                        (y*settings.Bok) + (margines + y)
                        );

                    wybranaKarta.Width = settings.Bok;
                    wybranaKarta.Height = settings.Bok;

                    wybranaKarta.Odkryj();

                    panelKart.Controls.Add(wybranaKarta);

                    karty.Remove(wybranaKarta);




                }
            }
        }



    }
}
