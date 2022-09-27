import { Radnja } from "./radnja.js";

fetch("https://localhost:5001/Radnja/Preuzmi")
.then(p=>{
    p.json().then(radnje=>{
        radnje.forEach(radnja => {
            var r=new Radnja(radnja.id,radnja.naziv);
            r.crtaj(document.body);
        })
    })
})
