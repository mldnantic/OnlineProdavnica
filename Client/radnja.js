import { Artikal } from "./artikal.js";
import { Korpa } from "./korpa.js";
import { Racun } from "./racun.js";

export class Radnja
{
    constructor(id,naziv)
    {
        this.id=id;
        this.naziv=naziv;
        this.korpa = new Korpa(this.id);
        this.racun = new Racun(this.id);
        this.kont=null;
    }

    crtaj(host)
    {
        this.kont = document.createElement("div");
        this.kont.className="glavniDiv";
        host.appendChild(this.kont);

        let naziv = document.createElement("h1");
        naziv.innerHTML=this.naziv;
        this.kont.appendChild(naziv);

        let gp = document.createElement("div");
        gp.className="glavniProzor";
        this.kont.appendChild(gp);

        let komandeDiv = document.createElement("div");
        komandeDiv.className="komande";
        gp.appendChild(komandeDiv);
        this.crtajKomande(komandeDiv);

        let artikliDiv = document.createElement("div");
        artikliDiv.className="artikli";
        artikliDiv.id="mainframe"+this.id;
        gp.appendChild(artikliDiv);
        
        this.crtajArt(artikliDiv,this.id);

        
    }

    crtajKomande(komandeDiv)
    {
        let btnKupi = document.createElement("button");
        btnKupi.innerHTML="Kupi";
        btnKupi.onclick=(ev)=>this.kupi(this.id);
        komandeDiv.appendChild(btnKupi);

        btnKupi = document.createElement("button");
        btnKupi.innerHTML="Vidi sadrzaj korpe";
        btnKupi.onclick=(ev)=>this.vidiKorpu(this.id);
        komandeDiv.appendChild(btnKupi);

        let btnRacuni = document.createElement("button");
        btnRacuni.innerHTML="Vidi racune";
        btnRacuni.onclick=(ev)=>this.vidiRacune(this.id);
        komandeDiv.appendChild(btnRacuni);

        let labela = document.createElement("label");
        labela.className="labelaUI";
        labela.innerHTML="Ime artikla:";
        komandeDiv.appendChild(labela);

        let inputtmp = document.createElement("input");
        inputtmp.id="imeinput"+this.id;
        komandeDiv.appendChild(inputtmp);

        labela = document.createElement("label");
        labela.className="labelaUI";
        labela.innerHTML="Cena:";
        komandeDiv.appendChild(labela);

        inputtmp = document.createElement("input");
        inputtmp.type="number";
        inputtmp.id="cenainput"+this.id;
        komandeDiv.appendChild(inputtmp);

        let btnDodaj = document.createElement("button");
        btnDodaj.innerHTML="Dodaj artikal";
        btnDodaj.onclick=(ev)=>this.dodaj(this.id);
        komandeDiv.appendChild(btnDodaj);

        let btnIzmeni = document.createElement("button");
        btnIzmeni.innerHTML="Izmeni artikal";
        btnIzmeni.onclick=(ev)=>this.izmeni(this.id);
        komandeDiv.appendChild(btnIzmeni);

        let btnObrisi = document.createElement("button");
        btnObrisi.innerHTML="Obrisi artikal";
        btnObrisi.onclick=(ev)=>this.obrisi(this.id);
        komandeDiv.appendChild(btnObrisi);
    }

    crtajArt(div,id)
    {
        fetch("https://localhost:5001/Artikal/Preuzmi/"+id)
        .then(p=>{
            p.json().then(artikli=>{
                artikli.forEach(artikal => {
                    var art=new Artikal(artikal.id,artikal.ime,artikal.cena);
                    art.crtajArtikal(div,id);
                })
            })
        })
    }

    kupi(id)
    {
        fetch("https://localhost:5001/Racun/Dodaj/"+id,{
            method:"POST"
        }).then(s=>{
            if(s.ok)
            {
                var teloApp = this.redraw(id,"artikli");
                this.crtajArt(teloApp,id);
            }
        })
    }

    dodaj(id)
    {
        let imeart = document.getElementById("imeinput"+this.id).value;
        console.log(imeart);

        if(imeart==0)
        {
            alert("unesite ime artikla");
            return;
        }
        if(imeart.length>20)
        {
            alert("predugo ime artikla");
            return;
        }

        let cenaart = document.getElementById("cenainput"+this.id).value;
        console.log(cenaart);

        if(cenaart==0)
        {
            alert("unesite cenu artikla");
            return;
        }
        if(cenaart<0)
        {
            alert("cena ne moze biti negativna");
            return;
        }

        fetch("https://localhost:5001/Artikal/Dodaj/"+cenaart+"/"+imeart+"/"+id,{
            method:"POST"
        }).then(s=>{
            if(s.ok)
            {
                var teloApp = this.redraw(id,"artikli");
                this.crtajArt(teloApp,id);
                s.json().then(data=>{
                    console.log(data);
                })
            }
        })
    }

    izmeni(id)
    {
        let imeart = document.getElementById("imeinput"+this.id).value;
        console.log(imeart);

        if(imeart==0)
        {
            alert("unesite ime artikla");
            return;
        }
        if(imeart.length>20)
        {
            alert("predugo ime artikla");
            return;
        }

        let cenaart = document.getElementById("cenainput"+this.id).value;
        console.log(cenaart);

        if(cenaart==0)
        {
            alert("unesite cenu artikla");
            return;
        }
        if(cenaart<0)
        {
            alert("cena ne moze biti negativna");
            return;
        }

        fetch("https://localhost:5001/Artikal/Izmeni/"+cenaart+"/"+imeart,{
            method:"PUT"
        }).then(s=>{
            if(s.ok)
            {
                var teloApp = this.redraw(id,"artikli");
                this.crtajArt(teloApp,id);
                s.json().then(data=>{
                    console.log(data);
                })
            }
        })

    }

    obrisi(id)
    {
        let imeart = document.getElementById("imeinput"+this.id).value;
        console.log(imeart);

        if(imeart==0)
        {
            alert("unesite ime artikla");
            return;
        }
        if(imeart.length>20)
        {
            alert("predugo ime artikla");
            return;
        }

        fetch("https://localhost:5001/Artikal/Obrisi/"+imeart,{
            method:"DELETE"
        }).then(s=>{
            if(s.ok)
            {
                var teloApp = this.redraw(id,"artikli");
                this.crtajArt(teloApp,id);
            }
        })
    }

    redraw(idradnje,imeklase)
    {
        var teloApp = document.getElementById("mainframe"+idradnje);
        var parent = teloApp.parentNode;
        parent.removeChild(teloApp);

        teloApp = document.createElement("div");
        teloApp.className=imeklase;
        teloApp.id="mainframe"+idradnje;
        parent.appendChild(teloApp);

        return teloApp;
    }
    
    vidiKorpu(id)
    {

        fetch("https://localhost:5001/KorpaSpoj/Preuzmi/"+id)
        .then(p=>{
            p.json().then(stavke=>{
                console.log(id);
                var teloApp = this.redraw(id,"korparacun");
                console.log(teloApp);
                let btnNazad = document.createElement("button");
                btnNazad.innerHTML="Nazad";
                btnNazad.className="btnNazad";
                btnNazad.onclick=(ev)=>this.nazad(this.id);
                teloApp.appendChild(btnNazad);
                if(stavke==0)
                {
                    let labela = document.createElement("h2");
                    labela.innerHTML="Korpa je trenutno prazna";
                    teloApp.appendChild(labela);
                }
                else{
                    stavke.forEach(stavka => {
                        this.korpa.crtajStavku(teloApp,stavka);
                    })
                }
                
            })
        })
    }

    vidiRacune(id)
    {
        fetch("https://localhost:5001/Racun/PreuzmiRacuneIzRadnje/"+id)
        .then(p=>{
            p.json().then(racuni=>{
                console.log(id);
                var teloApp = this.redraw(id,"korparacun");
                console.log(teloApp);
                let btnNazad = document.createElement("button");
                btnNazad.innerHTML="Nazad";
                btnNazad.className="btnNazad";
                btnNazad.onclick=(ev)=>this.nazad(this.id);
                teloApp.appendChild(btnNazad);
                racuni.forEach(racun => {
                    this.racun.crtajRacun(teloApp,racun);
                })
                
            })
        })
    }

    nazad(id)
    {
        console.log(id);

        var teloApp = this.redraw(id,"artikli");
        this.crtajArt(teloApp,id);
    }
}
