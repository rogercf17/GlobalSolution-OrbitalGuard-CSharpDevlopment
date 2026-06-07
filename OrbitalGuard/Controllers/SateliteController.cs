using Microsoft.AspNetCore.Mvc;
using OrbitalGuard.DTOs;
using OrbitalGuard.Models;
using OrbitalGuard.Services.Interfaces;

namespace OrbitalGuard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SateliteController : ControllerBase
    {
        private readonly ISateliteService _service;

        public SateliteController(ISateliteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var satelites = await _service.ObterTodosAsync();
                return Ok(satelites);
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
                var satelite = await _service.ObterPorIdAsync(id);
                if (satelite == null)
                    return NotFound($"Satélite {id} não encontrado.");

                return Ok(satelite);
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

        [HttpGet("ativos")]
        public async Task<IActionResult> GetAtivos()
        {
            try
            {
                var satelites = await _service.ObterAtivosAsync();
                return Ok(satelites);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SateliteDto dto)
        {
            try
            {
                var satelite = new Satelite
                {
                    Nome = dto.Nome,
                    Fabricante = dto.Fabricante,
                    Ativo = dto.Ativo,
                    AltitudeKm = dto.AltitudeKm,
                    TipoOrbita = dto.TipoOrbita,
                    CoberturaDegraus = dto.CoberturaDegraus,
                    leituras = new List<LeituraClimatica>()
                };

                await _service.CadastrarAsync(satelite);
                return CreatedAtAction(nameof(GetById), new { id = satelite.Id }, satelite);
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
        public async Task<IActionResult> Update(int id, [FromBody] SateliteDto dto)
        {
            try
            {
                var existente = await _service.ObterPorIdAsync(id);
                if (existente == null)
                    return NotFound($"Satélite {id} não encontrado.");

                existente.Nome = dto.Nome;
                existente.Fabricante = dto.Fabricante;
                existente.Ativo = dto.Ativo;
                existente.AltitudeKm = dto.AltitudeKm;
                existente.TipoOrbita = dto.TipoOrbita;
                existente.CoberturaDegraus = dto.CoberturaDegraus;

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