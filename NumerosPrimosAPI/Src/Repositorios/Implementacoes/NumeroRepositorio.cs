using Microsoft.EntityFrameworkCore;
using NumerosPrimosAPI.Src.Contexto;
using NumerosPrimosAPI.Src.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumerosPrimosAPI.Src.Repositorio.Implemtacoes
{
    public class NumeroRepositorio : INumeroRepositorio
    {
        private readonly NumeroContexto _contexto;

        public NumeroRepositorio(NumeroContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task AtualizarNumeroAsync(Numero numeros)
        {
            if (await ExisteNumero(numeros.NumeroPrimo)) throw new Exception("Numero primo ja existente no sistema!");

            var auxiliar = await PegarNumeroPeloIdAsync(numeros.Id);
            auxiliar.NumeroPrimo = numeros.NumeroPrimo;
            await _contexto.SaveChangesAsync();
        }

        public async Task DeletarNumeroAsync(int id)
        {
            _contexto.Numeros.Remove(await PegarNumeroPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        public async Task NovoNumeroAsync(Numero numeros)
        {
            await _contexto.Numeros.AddAsync(new Numero
            {
                NumeroPrimo = numeros.NumeroPrimo
            });
            await _contexto.SaveChangesAsync();
        }

        public async Task<Numero> PegarNumeroPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Id do número não encontrado");
            return await _contexto.Numeros.FirstOrDefaultAsync(n => n.Id == id);

            bool ExisteId(int id)
            {
                var auxiliar = _contexto.Numeros.FirstOrDefault(n => n.Id == id);
                return auxiliar != null;
            }
        }

        public async Task<List<Numero>> PegarTodosNumerosAsync()
        {
            return await _contexto.Numeros.ToListAsync();
        }

        //função auxiliar
        private async Task<bool> ExisteNumero(int numero)
        {
            var auxiliar = await _contexto.Numeros.FirstOrDefaultAsync(n => n.NumeroPrimo == numero);

            return auxiliar != null;
        }
    }
}
