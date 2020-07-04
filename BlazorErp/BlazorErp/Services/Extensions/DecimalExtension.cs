using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services.Extensions
{
    public static class DecimalExtension
    {
        public static decimal Zaokruzeno(this decimal x)
        {
            return Math.Round(x, 2, MidpointRounding.AwayFromZero);
        }
    }
}
