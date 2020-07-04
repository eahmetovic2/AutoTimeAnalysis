
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorErp.Shared.Models.Request.Korisnik
{
    public class KreirajKorisnikaRequestModel
    {
        [Required]
        [StringLength(64)]
        public String KorisnickoIme { get; set; }

        [Required]
        [StringLength(256)]
        public String Email { get; set; }

        [Required]
        [StringLength(128)]
        public String PunoIme { get; set; }

        [Required]
        [StringLength(128)]
        public String Lozinka { get; set; }
        public int? PolId { get; set; }

        public int SkolaId { get; set; }

        public int OsobaId { get; set; }

        [Required]
        public List<DodavanjeUlogeKorisnika> Uloge { get; set; }


    }
}
