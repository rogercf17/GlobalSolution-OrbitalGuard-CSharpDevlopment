using Microsoft.AspNetCore.Mvc;
using OrbitalGuard.DTOs;
using OrbitalGuard.Models;
using OrbitalGuard.Services.Interfaces;

namespace OrbitalGuard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegiaoMonitoradaController : ControllerBase
    {
        private readonly IRegiaoMonitoradaService _service;

        public RegiaoMonitoradaController(IRegiaoMonitoradaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var regioes = await _service.ObterTodosAsync();
                return Ok(regioes);
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
                var regiao = await _service.ObterPorIdAsync(id);
                if (regiao == null)
                    return NotFound($"Região {id} não encontrada.");

                return Ok(regiao);
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

        [HttpGet("pais/{pais}")]
        public async Task<IActionResult> GetByPais(string pais)
        {
            try
            {
                var regioes = await _service.ObterPorPaisAsync(pais);
                return Ok(regioes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegiaoMonitoradaDto dto)
        {
            try
            {
                var regiao = new RegiaoMonitorada
                {
                    Nome = dto.Nome,
                    Pais = dto.Pais,
                    Latitude = dto.Latitude,
                    Longitude = dto.Longitude,
                    AreaKm2 = dto.AreaKm2,
                    Leituras = new List<LeituraClimatica>(),
                    Alertas = new List<AlertaDesastre>()
                };

                await _service.CadastrarAsync(regiao);
                return CreatedAtAction(nameof(GetById), new { id = regiao.Id }, regiao);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RegiaoMonitoradaDto dto)
        {
            try
            {
                var existente = await _service.ObterPorIdAsync(id);
                if (existente == null)
                    return NotFound($"Satélite {id} não encontrado.");

                existente.Nome = dto.Nome;
                existente.Pais = dto.Pais;
                existente.Latitude = dto.Latitude;
                existente.Longitude = dto.Longitude;
                existente.AreaKm2 = dto.AreaKm2;

                await _service.AtualizarAsync(existente);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
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
