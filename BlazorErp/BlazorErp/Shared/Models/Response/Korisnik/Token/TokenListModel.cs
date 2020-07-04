using BlazorErp.Shared.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Token
{
    public class TokenListModel : PagedListModel<TokenListModelItem>
    {

    }

    public class TokenListModelItem
    {
        public Guid Id { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime DatumIsteka { get; set; }
        public String PosljednjiIp { get; set; }
        public String PosljednjiKlijent { get; set; }
        public DateTime DatumPosljednjeAkcije { get; set; }
    }
}
