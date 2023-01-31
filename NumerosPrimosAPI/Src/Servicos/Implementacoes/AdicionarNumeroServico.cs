using NumerosPrimosAPI.Src.Modelos;
using NumerosPrimosAPI.Src.Repositorio;
using System;

namespace NumerosPrimosAPI.Src.Servicos.Implementacoes
{
    public class AdicionarNumeroServico : IAdicionarNumerosServico
    {
        private readonly INumeroRepositorio _repositorio;

        public AdicionarNumeroServico(INumeroRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Numero NovoNumeroAsync(Numero numeros)
        {
            Numero numeroRetorno = null;

            if (numeros.NumeroPrimo <= 0)
            {
                throw new Exception("Número invalido, Digite número inteiro positivo!");
            }
            if (numeros.NumeroPrimo == 1)
            {
                throw new Exception("Número válido, mas não é primo!");
            }         

            

            int divisor = 0;

            for (int i = 1; i <= numeros; i++)
            {
                if (numeros % i == 0)
                {
                    divisor++;
                }
            }

            if (divisor == 2)
            {
                var numeroAdicionado = _repositorio.NovoNumeroAsync(numeros);
            }
            return numeroRetorno;
        }
    }
}
