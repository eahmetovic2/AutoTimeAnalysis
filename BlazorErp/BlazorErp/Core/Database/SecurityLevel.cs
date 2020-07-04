using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Core.Database
{
    public class SecurityLevel
    {
        public bool Read { get; set; }

        public bool Create { get; set; }

        public bool Update { get; set; }

        public bool Delete { get; set; }

        public bool Retroaktivno { get; set; }

        public bool SamoCitanje()
        {
            return Read && !Create && !Update && !Delete && !Retroaktivno;
        }
    }
}
