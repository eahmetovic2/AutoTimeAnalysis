using BlazorErp.Shared.Models.Request.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorErp.Shared.Models.Request.LogAkcija
{
    public class ListLogEntitetRequestModel : PagedRequestModel
    {
        [Required]
        public string Entitet { get; set; }
        public int Id { get; set; }
        public string KorisnickoIme { get; set; }
        public DateTime? Od { get; set; }
        public DateTime? Do { get; set; }
    }
}
