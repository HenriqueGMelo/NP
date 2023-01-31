using NumerosPrimosAPI.Src.Modelos;

namespace NumerosPrimosAPI.Src.Servicos
{
    public interface IAdicionarNumerosServico
    {
        Numero NovoNumeroAsync(Numero numeros);
    }
}
