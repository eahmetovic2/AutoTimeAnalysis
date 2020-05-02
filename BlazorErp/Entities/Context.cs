using Microsoft.EntityFrameworkCore;
using System;

namespace BlazorErp.Entities
{
    /// <summary>
    /// Db kontekst aplikacije
    /// </summary>
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
    }
}
