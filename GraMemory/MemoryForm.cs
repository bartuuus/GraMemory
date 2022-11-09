using GraMemory.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraMemory
{
    public partial class MemoryForm : Form
    {
        private GameSettings _settings;


        public MemoryForm()
        {
            InitializeComponent();
            _settings = new GameSettings();
            UstawKontrolki(_settings);


        }

        void UstawKontrolki(GameSettings settings)
        {
            panelKart.Width = _settings.Bok * _settings.Kolumny;
            panelKart.Height = _settings.Bok * _settings.Wiersze;
            this.Width = panelKart.Width + 40;
            this.Height = panelKart.Height + 100;

            lblStartInfo.Text = $"Początek gry za {_settings.CzasPodgladu}";
            lblPunktyWartosc.Text = _settings.AktualnePunkty.ToString();
            lblCzasWartosc.Text = _settings.CzasGry.ToString();
            lblStartInfo.Visible = true;

        }
      

    }
}
