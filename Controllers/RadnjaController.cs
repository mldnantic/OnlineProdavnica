using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Models.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RadnjaController : ControllerBase
    {
        public OnlineProdavnicaContext Context { get; set; }

        public RadnjaController(OnlineProdavnicaContext context)
        {
            Context = context;
        }

        [Route("Preuzmi")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi ()
        {
            try
            {
                return Ok(await Context.Radnje.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("Dodaj/{Naziv}")]
        [HttpPost]
        public async Task<ActionResult> Dodaj (string Naziv)
        {
            if(string.IsNullOrWhiteSpace(Naziv))
            {
                return BadRequest("prazan naziv");
            }
            if(Naziv.Length>50)
            {
                return BadRequest("predugacak naziv radnje");
            }
            try
            {
                Radnja r = new Radnja();
                r.Naziv = Naziv;
                Context.Radnje.Add(r);
                await Context.SaveChangesAsync();
                return Ok(r);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}