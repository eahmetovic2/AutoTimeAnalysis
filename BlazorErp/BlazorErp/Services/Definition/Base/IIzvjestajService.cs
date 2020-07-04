using BlazorErp.Entities.Models;
using BlazorErp.Entities.Models.Base;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace BlazorErp.Services
{
    public interface IIzvjestajService:IService
    {
        Task<byte[]> Pdf(IzvjestajPostavke postavke);
    }

    public class IzvjestajParametar
    {
        public string Naziv { get; set; }
        public List<string> Vrijednosti { get; }
        public bool Vidljivost { get; set; }

        public IzvjestajParametar()
        {
            this.Vrijednosti = new List<string>();
        }

        public IzvjestajParametar(string naziv, string vrijednost)
        {
            this.Naziv = naziv;
            this.Vrijednosti = new List<string>();
            this.Vrijednosti.Add(vrijednost);
        }
    }

    public class IzvjestajPostavke
    {
    

        public IzvjestajPostavke()
        {
            Parametri = new List<IzvjestajParametar>();           
        }

        public string Naziv { get; set; }

        public string Putanja { get; set; }

        public decimal Sirina { get; set; }

        public decimal Visina { get; set; }

        public bool Landscape { get; set; }

        public bool KorisnickePostavke { get; set; }

        public decimal MarginTop { get; set; }

        public decimal MarginLeft { get; set; }

        public decimal MarginRight { get; set; }

        public decimal MarginBottom { get; set; }

        public List<IzvjestajParametar> Parametri { get; set; }

        public void DodajParametar(string naziv, string vrijednost)
        {
            Parametri.Add(new IzvjestajParametar(naziv, vrijednost));
        }
    }
}
