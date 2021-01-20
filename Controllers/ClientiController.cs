using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArticoliWebService.Dtos;
using ArticoliWebService.Models;
using AutoMapper;
using GestFidApi.Dtos;
using GestFidApi.Models;
using GestFidApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestFidApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/clienti")]
    public class ClientiController : Controller
    {
        private readonly IClientiRepository clientiRepository;
        private readonly IMapper mapper;

        public ClientiController(IClientiRepository clientiRepository, IMapper mapper)
        {
            this.clientiRepository = clientiRepository;
            this.mapper = mapper;
        }

        [HttpGet("cerca")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientiDto>))]
        public async Task<ActionResult<IEnumerable<ClientiDto>>> GetClienti()
        {
            //var articoliDto = new List<ArticoliDto>();

            var clienti = await this.clientiRepository.SelAll();
            
            if (clienti.Count == 0)
            {
                return NotFound(new ErrMsg(string.Format("Non è stato trovato alcun cliente"),"404"));
            }

            return Ok(mapper.Map<ICollection<ClientiDto>>(clienti));
        }

        [HttpGet("cerca/codice/{CodFid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientiDto))]
        public async Task<ActionResult<ClientiDto>> GetClientiByCode(string CodFid)
        {
            //var articoliDto = new List<ArticoliDto>();

            //await Task.Delay(2000); //TODO Da eliminare

            var clienti = await this.clientiRepository.SelCliByCode(CodFid);
            
            if (clienti is null)
            {
                return NotFound(new ErrMsg(string.Format($"Non è stato trovato cliente con codice {CodFid}"),"404"));
            }

            return Ok(mapper.Map<ClientiDto>(clienti));
        }

        [HttpGet("cerca/nome/{Nome}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientiDto>))]
        public async Task<ActionResult<IEnumerable<ClientiDto>>> GetClienti(string Nome)
        {
            //var articoliDto = new List<ArticoliDto>();

            var clienti = await this.clientiRepository.SelCliByName(Nome);
            
            if (clienti.Count == 0)
            {
                return NotFound(new ErrMsg(string.Format($"Non è stato trovato alcun cliente col nome {Nome}"),"404"));
            }

            return Ok(mapper.Map<ICollection<ClientiDto>>(clienti));
        }

        [HttpPost("inserisci")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoMsg))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult SaveArticoli([FromBody] Clienti cliente)
        {
            bool IsOk = false;

            if (cliente == null)
            {
                return BadRequest(ModelState);
            }

            //Verifichiamo che i dati siano corretti
            if (!ModelState.IsValid)
            {
                string ErrVal = "";

                string errore = (this.HttpContext == null) ? "400" : this.HttpContext.Response.StatusCode.ToString();

                foreach (var modelState in ModelState.Values) 
                {
                    foreach (var modelError in modelState.Errors) 
                    {
                        ErrVal += modelError.ErrorMessage + " - "; 
                    }
                }

                return BadRequest(new ErrMsg(ErrVal,errore));
            }

        
            if (!clientiRepository.ClienteExists(cliente.CodFid))
            {
                IsOk = clientiRepository.InsCliente(cliente);
            }
            else
            {
                IsOk = clientiRepository.UpdCliente(cliente);
            }

            if (!IsOk)
            {
                ModelState.AddModelError("", $"Ci sono stati problemi nell'inserimento del cliente {cliente.Nominativo}!  ");
                return StatusCode(500, ModelState);
            }
            

            return Ok(new InfoMsg(DateTime.Today, $"Inserimento cliente {cliente.Nominativo} eseguita con successo."));
            

        }

        [HttpDelete("elimina/{CodFid}")]
        [ProducesResponseType(201, Type = typeof(InfoMsg))]
        [ProducesResponseType(400, Type = typeof(ErrMsg))]
        [ProducesResponseType(422, Type = typeof(ErrMsg))]
        [ProducesResponseType(500, Type = typeof(ErrMsg))]
        public IActionResult DeleteArticoli(string CodFid)
        {
            if (CodFid == "")
            {
                return BadRequest(new ErrMsg($"E' necessario inserire il codice del cliente da eliminare!",
                    this.HttpContext.Response.StatusCode.ToString()));
            }

            //Contolliamo se l'articolo è presente (Usare il metodo senza Traking)
            var cliente = clientiRepository.SelCliByCode2(CodFid);

            if (cliente == null)
            {
                return StatusCode(422, new ErrMsg($"Cliente {CodFid} NON presente in anagrafica! Impossibile Eliminare!",
                    "422"));
            }

             //verifichiamo che i dati siano stati regolarmente eliminati dal database
            if (!clientiRepository.DelCliente(cliente))
            {
                return StatusCode(500, new ErrMsg($"Ci sono stati problemi nella eliminazione del cliente {CodFid}.",
                    "500"));
            }

            return Ok(new InfoMsg(DateTime.Today, $"Eliminazione cliente {CodFid} eseguita con successo!"));
        }

    }
}