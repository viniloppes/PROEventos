using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;
using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;
using System.Linq;

namespace ProEventos.Application
{
    public class LoteService : ILoteService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly ILotePersist _lotePersist;
        private readonly IMapper _mapper;

        public LoteService(IGeralPersist geralPersist,
                            ILotePersist lotePersist,
                            IMapper mapper)
        {
            _geralPersist = geralPersist;
            _lotePersist = lotePersist;
            _mapper = mapper;

        }
        
        public async Task AddLote(int eventoId, LoteDto model)
        {
            try
            {
                var lote = _mapper.Map<Lote>(model);
                lote.EventoId = eventoId;
                _geralPersist.Add<Lote>(lote);
                await _geralPersist.SaveChangesAsync();
                // if ()
                // {
                //     var eventoRetorno = await _lotePersist.GetEventoByIdAsync(evento.Id, false);
                //     return _mapper.Map<LoteDto>(eventoRetorno);
                // }
                // return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models)
        {
            try
            {
                var lotes = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddLote(eventoId, model);
                    }
                    else
                    {
                     var lote = lotes.FirstOrDefault(lote => lote.Id == model.Id);
                        model.EventoId = eventoId;

                        _mapper.Map(model, lote);

                        _geralPersist.Update<Lote>(lote);

                        await _geralPersist.SaveChangesAsync();
                    }

                }


                var lotesRetorno = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
                return _mapper.Map<LoteDto[]>(lotesRetorno);


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> DeleteLote(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersist.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null) throw new Exception("Lote para delete não encontrado");


                _geralPersist.Delete<Lote>(lote);
                return await _geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await _lotePersist.GetLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;
                var resultado = _mapper.Map<LoteDto[]>(lotes);
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto> GetLoteByIdAsync(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersist.GetLoteByIdsAsync(eventoId, loteId);
                if (lote == null) return null;

                var resultado = _mapper.Map<LoteDto>(lote);
                // var eventosRetorno = new LoteDto();
                // eventosRetorno = new LoteDto()
                // {
                //     Id = evento.Id,
                //     Local = evento.Local,
                //     DataEvento = evento.DataEvento.ToString(),
                //     Tema = evento.Tema,
                //     QtdPessoas = evento.QtdPessoas,
                //     ImagemURL = evento.ImagemURL,
                //     Telefone = evento.Telefone,
                //     Email = evento.Email,

                // });

                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}