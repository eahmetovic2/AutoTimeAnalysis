using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Prava
{
    public enum PravoAkcije
    {
        //Korisnik
        KorisnikDodavanje,
        KorisnikIzmjena,
        KorisnikPregled,
        KorisnikLista,
        KorisnikPromjenaLozinke,
        KorisnikPregledLicnihPodataka,
        KorisnikIzmjenaLicnihPodataka,
        KorisnikPregledElementaSifarnika,

        //Uloga
    }
    public class PravoAkcije2
    {
        //public class KorisnikStruct
        //{
        //    private static ushort i = 1;
        //    public static ushort Dodavanje = ++i;
        //    public static ushort Izmjena = i++;
        //    public static ushort Pregled = i++;
        //    public static ushort Lista = i++;
        //    public static ushort PromjenaLozinke = i++;
        //    public static ushort PregledLicnihPodataka = i++;
        //    public static ushort IzmjenaLicnihPodataka = i++;
        //    public static ushort Authenticate = i++;
        //    public static ushort PregledElementaSifarnika = i++;
        //}
        public enum Korisnik
        {
            Dodavanje,
            Izmjena,
            Pregled,
            Lista,
            PromjenaLozinke,
            PregledLicnihPodataka,
            IzmjenaLicnihPodataka,
            PregledElementaSifarnika
        }
    
        public enum Uloga
        {

        }
        public enum Dashboard
        {

        }
        public enum Log
        {

        }
        public enum Postavke
        {

        }
        public enum Sifarnik
        {

        }
        public enum GlavniMeni
        {

        }

    }
}
