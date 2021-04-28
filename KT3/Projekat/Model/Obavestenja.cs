﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Projekat.Model;
using Model;
using System.ComponentModel;

namespace Projekat.Model
{
    public class Obavestenja : INotifyPropertyChanged
    {
        public Obavestenja() { }

        // TODO:  izbrisati ovaj konsturktor, prosiriti u kodu sa odg konstruktorom 
        public Obavestenja(int id, String datum, string TipOb, string SadrzajOb, bool Notifikacija)
        {
            this.IdObavestenja = id;
            this.Datum = datum;
            this.TipObavestenja = TipOb;
            this.SadrzajObavestenja = SadrzajOb;
            this.Notifikacija = Notifikacija;
        }

        public Obavestenja(int id, String datum, string TipOb, string SadrzajOb, List<int> ListaIdPacijenta, int IdLekara, bool Notifikacija, string Oznaka)
        {
            this.IdObavestenja = id;
            this.Datum = datum;
            this.TipObavestenja = TipOb;
            this.SadrzajObavestenja = SadrzajOb;
            this.ListaIdPacijenata = ListaIdPacijenta;
            this.IdLekara = IdLekara;
            this.Notifikacija = Notifikacija;
            this.Oznaka = Oznaka;
        }

        public int IdObavestenja { get; set; }
        public string TipObavestenja { get; set; }  // ili naslov
        public string SadrzajObavestenja { get; set; }
        public string Datum { get; set; }
        public List<int> ListaIdPacijenata { get; set; }
        public int IdLekara { get; set; }
        public string Oznaka { get; set; }  // kome je namenjeno obavestenje
        public bool Notifikacija { get; set; }   // notifikacija za zakazivanje, pomeranje ili otkazivanje termina + za uzimanje leka - ove notifikacije nisu na oglasnoj tabli sekretara

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
