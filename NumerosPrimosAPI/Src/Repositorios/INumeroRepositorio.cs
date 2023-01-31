using NumerosPrimosAPI.Src.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NumerosPrimosAPI.Src.Repositorio
{
    public interface INumeroRepositorio
    {
        Task<List<Numero>> PegarTodosNumerosAsync();
        Task<Numero> PegarNumeroPeloIdAsync(int id);
        Task NovoNumeroAsync(Numero numeros);
        Task AtualizarNumeroAsync(Numero numeros);
        Task DeletarNumeroAsync(int id);
    }
}
