using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Token
{
    public class AktivacijskiTokenModel
    {
		public int Id { get; set; }

		public string Token { get; set; }

		public string KorisnickoIme { get; set; }

		public DateTime DatumKreiranja { get; set; }

		public DateTime DatumIsteka { get; set; }
	}
}
