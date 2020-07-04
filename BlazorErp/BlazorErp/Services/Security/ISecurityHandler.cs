using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorErp.Services.Security
{
    public interface ISecurityHandler
    {
        bool ImaPravo(string akcija);
    }
}
