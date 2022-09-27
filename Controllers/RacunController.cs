using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Models.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RacunController : ControllerBase
    {
        public OnlineProdavnicaContext Context { get; set; }

        public RacunController(OnlineProdavnicaContext context)
        {
            Context = context;
        }

        [Route("PreuzmiRacuneIzRadnje/{radnjaID}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi (int radnjaID)
        {
            if(radnjaID<0)
            {
                return BadRequest("nepostojeci racun");
            }
            try
            {
                var racuni = await Context.Racuni.Where(p=>p.Radnja.ID == radnjaID).ToListAsync();
                return Ok(racuni);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PreuzmiDetaljeRacuna/{racunID}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiDetaljeRacuna (int racunID)
        {
            if(racunID<0)
            {
                return BadRequest("nepostojeci racun");
            }
            try
            {
                var detalji = await Context.RacunSpojevi
                .Where(p=>p.Racun.ID == racunID)
                .Select(p=>new {p.ID, p.Artikal})
                .ToListAsync();
                return Ok(detalji);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("Dodaj/{radnjaID}")]
        [HttpPost]
        public async Task<ActionResult> Dodaj (int radnjaID)
        {
            if(radnjaID<0)
            {
                return BadRequest("nepostojeca radnja");
            }
            try
            {
                var iznosLista = await Context.KorpaSpojevi
                .Where(p=>p.Radnja.ID == radnjaID)
                .Select(p=>new{p.Artikal.Cena,p.Artikal})
                .ToListAsync();
                
                if(iznosLista.Count==0)
                {
                    return BadRequest("nema artikala u korpi");
                }

                int iznos=0;

                foreach (var item in iznosLista)
                {
                    iznos+=item.Cena;
                }

                var prikazRacuna = new
                {
                    artikli=iznosLista,
                    iznos=iznos
                };

                Racun racun = new Racun();
                racun.Iznos = iznos;
                racun.Radnja = await Context.Radnje
                .Where(p=>p.ID==radnjaID)
                .FirstOrDefaultAsync();
                racun.Datum=DateTime.Now;
                Context.Racuni.Add(racun);

                foreach (var item in iznosLista)
                {
                    RacunSpoj rs = new RacunSpoj();
                    rs.Artikal = item.Artikal;
                    rs.Racun = racun;
                    Context.RacunSpojevi.Add(rs);
                }

                var zapraznukorpu = await Context.KorpaSpojevi
                .Where(p=>p.Radnja.ID == radnjaID).ToListAsync();
                Context.KorpaSpojevi.RemoveRange(zapraznukorpu);
                await Context.SaveChangesAsync();
                
                return Ok(prikazRacuna);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}