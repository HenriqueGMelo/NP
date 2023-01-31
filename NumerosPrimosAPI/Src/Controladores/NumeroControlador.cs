using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NumerosPrimosAPI.Src.Modelos;
using NumerosPrimosAPI.Src.Repositorio;
using System;
using System.Threading.Tasks;

namespace NumerosPrimosAPI.Src.Controlador
{
    [ApiController]
    [Route("api/Numero")]
    [Produces("application/json")]
    public class NumeroControlador : ControllerBase
    {
        private readonly INumeroRepositorio _repositorio;

        public NumeroControlador(INumeroRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet("BuscarTodos")]
        public async Task<ActionResult> PegarTodosNumerosAsync()
        {
            var lista = await _repositorio.PegarTodosNumerosAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        [HttpGet("id/{idNumero}")]
        public async Task<ActionResult> PegarNumeroPeloIdAsync([FromRoute] int idNumero)
        {
            try
            {
                return Ok(await _repositorio.PegarNumeroPeloIdAsync(idNumero));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        [HttpPost("Adicionar")]
        public async Task<ActionResult> NovoNumeroAsync([FromBody] Numero numeros)
        {
            await _repositorio.NovoNumeroAsync(numeros);
            return Created($"api/Numeros{numeros.NumeroPrimo}", numeros);
        }

        [HttpPut("Atualizar")]
        public async Task<ActionResult> AtualizarNumeroAsync([FromBody] Numero numeros)
        {
            try
            {
                await _repositorio.AtualizarNumeroAsync(numeros);
                return Ok(numeros);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        [HttpDelete("id/{idNumero}/Deletar")]
        public async Task<ActionResult> DeletarNumeroAsync([FromRoute] int idNumero)
        {
            try
            {
                await _repositorio.DeletarNumeroAsync(idNumero);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }
    }
}
