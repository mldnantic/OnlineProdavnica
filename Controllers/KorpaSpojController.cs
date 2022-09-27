using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Models.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorpaSpojController : ControllerBase
    {
        public OnlineProdavnicaContext Context { get; set; }

        public KorpaSpojController(OnlineProdavnicaContext context)
        {
            Context = context;
        }

        [Route("Preuzmi/{radnjaID}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi (int radnjaID)
        {
            if(radnjaID<0)
            {
                return BadRequest("nepostojeca radnja");
            }
            try
            {
                return Ok(await Context.KorpaSpojevi
                .Where(p=>p.Radnja.ID==radnjaID)
                .Select(p=>new {p.ID, p.Artikal})
                .ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("Dodaj/{radnjaID}/{artikalID}")]
        [HttpPost]
        public async Task<ActionResult> Dodaj (int radnjaID, int artikalID)
        {
            if(radnjaID<0)
            {
                return BadRequest("nepostojeca radnja");
            }
            if(artikalID<0)
            {
                return BadRequest("nepostojeci artikal");
            }
            try
            {
                var art = await Context.Artikli
                .Where(p=>p.ID==artikalID && p.Radnja.ID ==radnjaID)
                .FirstOrDefaultAsync();
                if(art==null)
                {
                    return BadRequest("nema artikal u radnji");
                }
                KorpaSpoj ks = new KorpaSpoj();
                ks.Radnja = await Context.Radnje.Where(p=>p.ID==radnjaID)
                .FirstOrDefaultAsync();
                ks.Artikal = art;
                Context.KorpaSpojevi.Add(ks);
                await Context.SaveChangesAsync();
                return Ok(ks);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("Obrisi/{artID}")]
        [HttpDelete]
        public async Task<ActionResult> Obrisi (int artID)
        {
            if(artID<0)
            {
                return BadRequest("artikal ne postoji u korpi");
            }
            try
            {
                var a = await Context.KorpaSpojevi.Where(p=>p.Artikal.ID==artID).FirstOrDefaultAsync();
                if(a==null)
                {
                    return BadRequest("ne postoji artikal u korpi");
                }
                Context.KorpaSpojevi.Remove(a);
                await Context.SaveChangesAsync();
                return Ok($"uspesno izbacen artikal iz korpe sa ID : {artID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}