using Microsoft.EntityFrameworkCore;
using NumerosPrimosAPI.Src.Modelos;

namespace NumerosPrimosAPI.Src.Contexto
{
    public class NumeroContexto : DbContext
    {
        public DbSet<Numero> Numeros { get; set; }    

        public NumeroContexto(DbContextOptions<NumeroContexto> opt) : base(opt)
        {

        }
    }
}
