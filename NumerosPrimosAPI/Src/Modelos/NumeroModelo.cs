using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NumerosPrimosAPI.Src.Modelos
{
    [Table("tb_numeros")]
    public class Numero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NumeroPrimo { get; set; }
    }
}
