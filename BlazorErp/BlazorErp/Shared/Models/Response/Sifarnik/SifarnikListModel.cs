using BlazorErp.Shared.Models.Response.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazorErp.Shared.Models.Response.Sifarnik
{
    public class SifarnikListModel : ListModel<SifarnikModel>
    {
        public List<PoljeSifarnika> FieldsList { get; set; }

        public int Total { get; set; }

        public SifarnikListModel()
        {
        }

        public SifarnikListModel(IEnumerable<SifarnikModel> sifarnici, List<PoljeSifarnika> polja, int total)
            : base(sifarnici)
        {
            FieldsList = polja;
            Total = total;
        }

        public List<Dictionary<string, object>> Fields
        {
            get
            {
                return FieldsList.Select(a => a.DajKonfiguracije()).ToList();
            }
            set { }
        }

    }
}
