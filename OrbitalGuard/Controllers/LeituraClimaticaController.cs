using Microsoft.AspNetCore.Mvc;
using OrbitalGuard.DTOs;
using OrbitalGuard.Models;
using OrbitalGuard.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OrbitalGuard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeituraClimaticaController : ControllerBase
    {
        private readonly ILeituraClimaticaService _service;

        public LeituraClimaticaController(ILeituraClimaticaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var leituras = await _service.ObterTodasAsync();
                return Ok(leituras);
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
                var leitura = await _service.ObterPorIdAsync(id);
                if (leitura == null)
                    return NotFound($"Leitura {id} não encontrada.");

                return Ok(leitura);
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

        [HttpGet("por-satelite/{sateliteId}")]
        public async Task<IActionResult> GetBySatelite(int sateliteId)
        {
            try
            {
                var leituras = await _service.ObterPorSateliteAsync(sateliteId);
                return Ok(leituras);
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

        [HttpGet("por-regiao/{regiaoId}")]
        public async Task<IActionResult> GetByRegiao(int regiaoId)
        {
            try
            {
                var leituras = await _service.ObterPorRegiaoAsync(regiaoId);
                return Ok(leituras);
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LeituraClimaticaDto dto)
        {
            try
            {
                var leitura = new LeituraClimatica
                {
                    Timestamp = DateTime.UtcNow,
                    TemperaturaC = dto.TemperaturaC,
                    UmidadePercent = dto.UmidadePercent,
                    PressaoHpa = dto.PressaoHpa,
                    VelocidadeVentoKmh = dto.VelocidadeVentoKmh,
                    IndiceRisco = dto.IndiceRisco,
                    SateliteId = dto.SateliteId,
                    RegiaoMonitoradaId = dto.RegiaoMonitoradaId
                };

                await _service.CadastrarAsync(leitura);
                return CreatedAtAction(nameof(GetById), new { id = leitura.Id }, leitura);
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
        public async Task<IActionResult> Update(int id, [FromBody] LeituraClimaticaDto dto)
        {
            try
            {
                var existente = await _service.ObterPorIdAsync(id);
                if (existente == null)
                    return NotFound($"Leitura {id} não encontrada.");

                existente.TemperaturaC = dto.TemperaturaC;
                existente.UmidadePercent = dto.UmidadePercent;
                existente.PressaoHpa = dto.PressaoHpa;
                existente.VelocidadeVentoKmh = dto.VelocidadeVentoKmh;
                existente.IndiceRisco = dto.IndiceRisco;
                existente.SateliteId = dto.SateliteId;
                existente.RegiaoMonitoradaId = dto.RegiaoMonitoradaId;

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