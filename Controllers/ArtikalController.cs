using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Models.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtikalController : ControllerBase
    {
        public OnlineProdavnicaContext Context { get; set; }

        public ArtikalController(OnlineProdavnicaContext context)
        {
            Context = context;
        }

        [Route("Preuzmi/{idRadnje}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi (int idRadnje)
        {
            if(idRadnje<0)
            {
                return BadRequest("radnja ne postoji");
            }
            try
            {
                var a = await Context.Artikli.Where(p=>p.Radnja.ID==idRadnje).ToListAsync();
                if(a!=null)
                {
                    return Ok(a);
                }
                else
                {
                    return BadRequest("ne postoji artikal");
                }
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("Dodaj/{cena}/{ime}/{radnjaID}")]
        [HttpPost]
        public async Task<ActionResult> Dodaj (int cena,string ime,int radnjaID)
        {
            if(string.IsNullOrWhiteSpace(ime))
            {
                return BadRequest("prazan naziv");
            }
            if(ime.Length>20)
            {
                return BadRequest("predugacko ime");
            }
            if(cena<0)
            {
                return BadRequest("cena<0");
            }
            if(radnjaID<0)
            {
                return BadRequest("radnja ne postoji");
            }
            try
            {
                var artcheck = await Context.Artikli.Where(p=>p.Ime==ime).FirstOrDefaultAsync();
                if(artcheck!=null)
                {
                    return BadRequest("ime artikla mora biti jedinstveno");
                }
                Artikal a = new Artikal();
                a.Cena = cena;
                a.Ime = ime;
                a.Radnja = await Context.Radnje.Where(p=>p.ID==radnjaID).FirstOrDefaultAsync();
                Context.Artikli.Add(a);
                await Context.SaveChangesAsync();
                return Ok(a);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("Izmeni/{cena}/{ime}")]
        [HttpPut]
        public async Task<ActionResult> Izmeni (int cena,string ime)
        {
            if(string.IsNullOrWhiteSpace(ime))
            {
                return BadRequest("prazan naziv");
            }
            if(cena<0)
            {
                return BadRequest("cena<0");
            }
            try
            {
                var a = await Context.Artikli.Where(p=>p.Ime==ime).FirstOrDefaultAsync();
                if(a==null)
                {
                    return BadRequest("artikal sa unetim imenom ne postoji");
                }
                a.Cena = cena;
                await Context.SaveChangesAsync();
                return Ok(a);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("Obrisi/{imeArtikla}")]
        [HttpDelete]
        public async Task<ActionResult> Obrisi (string imeArtikla)
        {
            if(string.IsNullOrEmpty(imeArtikla))
            {
                return BadRequest("artikal ne postoji");
            }
            try
            {
                var a = await Context.Artikli.Where(p=>p.Ime==imeArtikla).FirstOrDefaultAsync();
                string ime=a.Ime;
                var korpaart = await Context.KorpaSpojevi.Where(p=>p.Artikal==a).ToListAsync();
                Context.KorpaSpojevi.RemoveRange(korpaart);
                var racunart = await Context.RacunSpojevi.Where(p=>p.Artikal==a).ToListAsync();
                if(racunart.Count>0)
                {
                    return BadRequest("artikal je vec kupovan i ne moze biti obrisan iz evidencije");
                }
                Context.Artikli.Remove(a);
                await Context.SaveChangesAsync();
                return Ok($"uspesno obrisan artikal : {ime}");
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

    }
}