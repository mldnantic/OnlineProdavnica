export class Korpa
{
    constructor(idradnje)
    {
        this.idradnje=idradnje;
    }

    crtajStavku(canvas,stavka)
    {
        let stDiv = document.createElement("div");
        stDiv.className="stavka";
        canvas.appendChild(stDiv);

        let st = document.createElement("label");
        st.innerHTML=stavka.artikal.ime;
        st.className="stavka";
        stDiv.appendChild(st);

        st = document.createElement("label");
        st.innerHTML=stavka.artikal.cena+" RSD";
        st.className="stavka";
        stDiv.appendChild(st);
    }
}