using Microsoft.AspNetCore.Mvc;
using OrbitalGuard.Domain.Enums;
using OrbitalGuard.DTOs;
using OrbitalGuard.Models;
using OrbitalGuard.Services.Interfaces;

namespace OrbitalGuard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertaDesastreController : ControllerBase
    {
        private readonly IAlertaDesastreService _service;

        public AlertaDesastreController(IAlertaDesastreService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var alertas = await _service.ObterTodosAsync();
                return Ok(alertas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var alerta = await _service.ObterPorIdAsync(id);
                if (alerta == null)
                    return NotFound($"Alerta {id} não encontrado.");

                return Ok(alerta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("nivel/{nivel}")]
        public async Task<IActionResult> GetByNivel(NivelAlerta nivel)
        {
            try
            {
                var alertas = await _service.ObterPorNivelAsync(nivel);
                return Ok(alertas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("tipo/{tipo}")]
        public async Task<IActionResult> GetByTipo(TipoDesastre tipo)
        {
            try
            {
                var alertas = await _service.ObterPorTipoAsync(tipo);
                return Ok(alertas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlertaDesastreDto dto)
        {
            try
            {
                var alerta = new AlertaDesastre
                {
                    TipoDesastre = dto.TipoDesastre,
                    NivelAlerta = dto.NivelAlerta,
                    Descricao = dto.Descricao,
                    DataHoraAlerta = DateTime.UtcNow,
                    Resolvido = false,
                    DataHoraResolucao = null,
                    LeituraClimaticaId = dto.LeituraClimaticaId
                };

                await _service.CadastrarAsync(alerta);
                return CreatedAtAction(nameof(GetById), new { id = alerta.Id }, alerta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPatch("{id}/resolver")]
        public async Task<IActionResult> Resolver(int id)
        {
            try
            {
                await _service.ResolverAlertaAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.RemoverAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}