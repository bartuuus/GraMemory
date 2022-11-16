using GraMemory.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraMemory
{
    public partial class MemoryForm : Form
    {
        private GameSettings _settings;
        MemoryCard _pierwszaOdkrytaKarta;
        MemoryCard _drugaOdkrytaKarta;



        public MemoryForm()
        {
            InitializeComponent();
            _settings = new GameSettings();
            UstawKontrolki(_settings);

            GenerujKarty(_settings, panelKart);
            timerCzasPodgladu.Start();
        }

        private void BtnClicked(object sender, EventArgs e)
        {
            var btn = (MemoryCard)sender;

            if (_pierwszaOdkrytaKarta == null)
            {
                _pierwszaOdkrytaKarta = btn;
                _pierwszaOdkrytaKarta.Odkryj();
            }
            else
            {
                _drugaOdkrytaKarta = btn;
                _drugaOdkrytaKarta.Odkryj();

                panelKart.Enabled = false;
                if (_pierwszaOdkrytaKarta.Id == _drugaOdkrytaKarta.Id)
                {
                    AktualizujPunkty(10);

                    _pierwszaOdkrytaKarta = null;
                    _drugaOdkrytaKarta = null;

                    panelKart.Enabled = true;
                }
                else
                {
                    timerZakrywacz.Start();
                }


            }

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

        private void TimerCzasPodgladu_Tick(object sender, EventArgs e)
        {
            _settings.CzasPodgladu--;

            lblStartInfo.Text = $"Początek gry za {_settings.CzasPodgladu}";

            if (_settings.CzasPodgladu <= 0)
            {
                lblStartInfo.Visible = false;
                foreach (MemoryCard karta in panelKart.Controls)
                {
                    karta.Zakryj();
                }

                timerCzasPodgladu.Stop();
                timerCzasGry.Start();
            }

        }
        public void GenerujKarty(GameSettings settings, Panel panelKart)
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

                for (int x = 0; x < settings.Kolumny; x++)
                {
                    for (int y = 0; y < settings.Wiersze; y++)
                    {
                        var losowyIndex = rand.Next(0, karty.Count());
                        var wybranaKarta = karty[losowyIndex];

                        int margines = 2;
                        wybranaKarta.Location = new System.Drawing.Point(
                            (x * settings.Bok) + (margines + x),
                            (y * settings.Bok) + (margines + y)
                            );

                        wybranaKarta.Width = settings.Bok;
                        wybranaKarta.Height = settings.Bok;

                        wybranaKarta.Odkryj();

                        panelKart.Controls.Add(wybranaKarta);

                        wybranaKarta.Click += BtnClicked;

                        karty.Remove(wybranaKarta);




                    }
                }

            }

        private void TimerZakrywacz_Tick(object sender, EventArgs e)
        {
            _pierwszaOdkrytaKarta.Zakryj();
            _drugaOdkrytaKarta.Zakryj();
            _pierwszaOdkrytaKarta = null;
            _drugaOdkrytaKarta = null;

            panelKart.Enabled = true;

            AktualizujPunkty(-2);

            timerZakrywacz.Stop();



        }

        private void AktualizujPunkty(int punkty)
        {
            _settings.AktualnePunkty += punkty;
            lblPunktyWartosc.Text = _settings.AktualnePunkty.ToString();
        }
    }
}
