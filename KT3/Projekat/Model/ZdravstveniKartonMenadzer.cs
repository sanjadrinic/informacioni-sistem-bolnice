﻿using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Projekat.Model
{
    class ZdravstveniKartonMenadzer
    {
        public static List<ZdravstveniKarton> kartoni = new List<ZdravstveniKarton>();
        public static List<LekarskiRecept> recepti = new List<LekarskiRecept>();

        /*public static List<ZdravstveniKarton> NadjiSveKartone()
        {
            if (File.ReadAllText("kartoni.xml").Trim().Equals(""))
            {
                return kartoni;
            }
            else
            {
                FileStream fileStream = File.OpenRead("kartoni.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(List<ZdravstveniKarton>));
                kartoni = (List<ZdravstveniKarton>)serializer.Deserialize(fileStream);
                fileStream.Close();
                return kartoni;
            }
        }*/

        /*
        public static ZdravstveniKarton NadjiKartonPoId(int id)
        {
            foreach (ZdravstveniKarton z in kartoni)
            {
                if (z.IdKartona == id)
                {
                    return z;
                }
            }
            return null;
        }*/

        /*public static void SacuvajKartone()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ZdravstveniKarton>));
            TextWriter filestream = new StreamWriter("kartoni.xml");
            serializer.Serialize(filestream, kartoni);
            filestream.Close();
        }*/

        public static int GenerisanjeIdRecepta(int idPac)
        {
            bool pomocna = false;
            int id = 1;
            foreach(Pacijent pac in PacijentiMenadzer.pacijenti)
            {
                if(pac.IdPacijenta == idPac)
                {
                    for (id = 1; id <= pac.Karton.LekarskiRecepti.Count; id++)
                    {
                        foreach (LekarskiRecept p in pac.Karton.LekarskiRecepti)
                        {
                            if (p.IdRecepta.Equals(id))
                            {
                                pomocna = true;
                                break;
                            }
                        }

                        if (!pomocna)
                        {
                            return id;
                        }
                        pomocna = false;
                    }
                }
            }
            

            return id;
        }

        public static void DodajRecept(LekarskiRecept recept) /*TO DO: PROMIJENITI OVO KAD SE DODA FAJL SA KARTONIMA*/
        {
            foreach (Pacijent pacijent in PacijentiMenadzer.pacijenti)
            {
                if(pacijent.IdPacijenta == recept.idPacijenta)
                {                    
                    pacijent.Karton.LekarskiRecepti.Add(recept);
                    Console.WriteLine(pacijent.Karton.LekarskiRecepti.Count);
                    TabelaRecepata.PrikazRecepata.Add(recept);

                }
            }
        }
    }
}
