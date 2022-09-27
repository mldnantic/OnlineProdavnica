export class Racun
{
    constructor(radnjaid)
    {
        this.radnjaid = radnjaid;
    }

    crtajRacun(canvas,racun)
    {
        let stDiv = document.createElement("div");
        stDiv.className="stavka";
        canvas.appendChild(stDiv);

        let st = document.createElement("label");
        let godina = new Date(racun.datum).getFullYear();
        let mesec = new Date(racun.datum).getMonth();
        let dan = new Date(racun.datum).getDate();
        let sat = new Date(racun.datum).getHours();
        let minut = new Date(racun.datum).getMinutes();
        if(minut<10)
        {
            minut="0"+minut;
        }
        st.innerHTML=dan+". "+mesec+". "+godina+". "+sat+":"+minut;
        st.className="stavka";
        stDiv.appendChild(st);

        st = document.createElement("label");
        st.innerHTML=racun.iznos+"RSD";
        st.className="stavka";
        stDiv.appendChild(st);

        let btnDetalji = document.createElement("button");
        btnDetalji.innerHTML="Detalji";
        btnDetalji.className="stavka";
        btnDetalji.onclick=(ev)=>this.detaljanRacun(canvas,racun.id,racun.iznos);
        stDiv.appendChild(btnDetalji);
    }

    detaljanRacun(canvas,racunid)
    {
        fetch("https://localhost:5001/Racun/PreuzmiDetaljeRacuna/"+racunid)
        .then(p=>{
            p.json().then(racuni=>{
                console.log(racunid);

                var teloApp = document.getElementById("mainframe"+this.radnjaid);
                var parent = canvas.parentNode;
                parent.removeChild(canvas);

                canvas = document.createElement("div");
                canvas.className="korparacun";
                canvas.id="mainframe"+this.radnjaid;
                parent.appendChild(canvas);

                console.log(canvas);
                
                racuni.forEach(racun => {
                    console.log(racun);
                    
                    let stDiv = document.createElement("div");
                    stDiv.className="stavka";
                    canvas.appendChild(stDiv);

                    let st = document.createElement("label");
                    st.innerHTML=racun.artikal.ime;
                    st.className="stavka";
                    stDiv.appendChild(st);

                    st = document.createElement("label");
                    st.innerHTML=racun.artikal.cena+" RSD";
                    st.className="stavka";
                    stDiv.appendChild(st);
                })
                
            })
        })
    }


}