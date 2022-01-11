using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Application.Dtos;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventosService _eventoService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public EventosController(IEventosService eventoService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null) return NotFound("Nenhum evento encontrado");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Erro ao tentar recuperar eventos. Erro: " + ex.Message);
            }
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if (evento == null) return NoContent();
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Erro ao tentar recuperar evento. Erro: " + ex.Message);
            }
        }


        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetByITema(string tema)
        {
            try
            {
                var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if (evento == null) return NotFound("Eventos por tema não encontrados");
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Erro ao tentar recuperar evento. Erro: " + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                var evento = await _eventoService.AddEventos(model);
                if (evento == null) return NoContent();
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Erro ao tentar adicionar evento. Erro: " + ex.Message);
            }
        }

        [HttpPost("upload-image/{eventoId}")]
        public async Task<IActionResult> UploadImage(int eventoId)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(eventoId, true);
                if (evento == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    DeleteImage(evento.ImagemURL);
                    evento.ImagemURL = await SaveImage(file);
                }

                var eventoRetorno = await _eventoService.UpdateEventos(eventoId, evento);
                return Ok(eventoRetorno);



            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Erro ao tentar adicionar evento. Erro: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventoDto model)
        {
            try
            {
                var evento = await _eventoService.UpdateEventos(id, model);
                if (evento == null) return NoContent();
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Erro ao tentar atualizar evento. Erro: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if (evento == null) return NoContent();

                if (await _eventoService.DeleteEvento(id))
                {
                    DeleteImage(evento.ImagemURL);
                    return Ok(new { message = "Deletado" });
                }
                else
                {
                    throw new Exception("Ocorreu um problema não especifico ao tentar deletar Evento");
                }
              
                // if (await _eventoService.DeleteEvento(id))
                // {
                //   
                //     return Ok("Deletado");
                // }
                // else
                // {
                //     return BadRequest("Evento não deletado");
                // }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Erro ao tentar deletar evento. Erro: " + ex.Message);
            }
        }

        [NonAction]
        public void DeleteImage(string imgNm)
        {
            try
            {
                var imgPath = Path.Combine(_hostEnvironment.ContentRootPath, @"Recursos/imagem-evento", imgNm);
                if (System.IO.File.Exists((imgPath)))
                {
                    System.IO.File.Delete(imgPath);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            try
            {
                string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
                .Take(10)
                .ToArray())
                .Replace(' ', '-');
                imageName = $"{imageName}{DateTime.UtcNow.ToString("yyyymmssfff")}{Path.GetExtension(imageFile.FileName)}";
                var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Recursos/imagem-evento", imageName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                return imageName;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}