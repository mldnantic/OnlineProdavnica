export class Artikal
{
    constructor(id,ime,cena)
    {
        this.id=id;
        this.ime=ime;
        this.cena=cena;
        this.kont=null;
    }

    crtajArtikal(host,idradnje)
    {

        let artDiv = document.createElement("div");
        artDiv.className = "artikal";
        host.appendChild(artDiv);

            
        let ime = document.createElement("label");
        ime.innerHTML=this.ime;
        artDiv.appendChild(ime);

        ime = document.createElement("label");
        ime.innerHTML="Cena: " + this.cena+" RSD";
        artDiv.appendChild(ime);
        
        let btnkorpa = document.createElement("button");
        btnkorpa.innerHTML="Dodaj u korpu";
        btnkorpa.onclick=(ev)=>this.dodajUKorpu(this.id,idradnje);
        artDiv.appendChild(btnkorpa);

        let btnkorpaIzbaci = document.createElement("button");
        btnkorpaIzbaci.innerHTML="Izbaci iz korpe";
        btnkorpaIzbaci.onclick=(ev)=>this.izbaciIzKorpe(this.id);
        artDiv.appendChild(btnkorpaIzbaci);
        
    }

    dodajUKorpu(id,idradnje)
    {
        console.log("dodajukorpu params: 1. "+ id + " 2. "+ idradnje);
        fetch("https://localhost:5001/KorpaSpoj/Dodaj/"+idradnje+"/"+id,{
            method:"POST"
        })
    }

    izbaciIzKorpe(id)
    {
        console.log("izbrisiizkorpe params: 1.  "+ id);
        fetch("https://localhost:5001/KorpaSpoj/Obrisi/"+id,{
            method:"DELETE"
        })
    }
}