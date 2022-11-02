using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraMemory.Core
{
    public class MemoryCard : Label
    {

        public Guid Id { get; set; }
        public Image Tył { get; private set; }
        public Image Obrazek { get; private set; }

        public MemoryCard(Guid id, string tył, string obrazek)
        {
            Id = id;
            Tył = Image.FromFile(tył);
            Obrazek = Image.FromFile(obrazek);
            BackgroundImageLayout = ImageLayout.Stretch;

        }

        public void Zakryj()
        {
            BackgroundImage = Tył;
            Enabled = true;
        }

        public void Odkryj()
        {
            BackgroundImage = Obrazek;
            Enabled = false;
        }



    }
}
