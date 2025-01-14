export class Firma
{
    constructor(naziv, cena, prijemDatum, dostavaDatum)
    {
        this.naziv = naziv;
        this.cena = cena;
        this.zarada = 0;
        this.lowDate = prijemDatum;
        this.upperDate = dostavaDatum;
        this.vozila = [];
        this.cont = null;
    }

    dodajVozilo(vozilo)
    {
        this.vozila.push(vozilo);
    }

    crtajTrazeno(host, v, m)
    {
        this.cont = host;
        let vs = this.vozila.filter(vehicle => vehicle.kapacitet >= v && vehicle.maxTezina >= m);

        vs.forEach(vozilo => {
            let divVozilo = document.createElement("div");
            divVozilo.className = "contVozilo";
            let imeKlase = this.naziv.replace(/\s+/g, '-');
            divVozilo.classList.add(imeKlase);
            host.appendChild(divVozilo);

            let l = document.createElement("label");
            l.innerHTML = "Naziv: " + this.naziv;
            divVozilo.appendChild(l);

            vozilo.crtajSliku(divVozilo);

            l = document.createElement("label");
            l.innerHTML = "Cena: " + this.cena;
            divVozilo.appendChild(l);

            l = document.createElement("label");
            l.className = "zarada";
            l.innerHTML = "Zarada: " + this.zarada;
            divVozilo.appendChild(l);

            let btnIsporuci = document.createElement("button");
            btnIsporuci.innerHTML = "Isporuci";
            btnIsporuci.className = "isporuci";
            btnIsporuci.onclick=(ev)=>this.isporuci(divVozilo);
            divVozilo.appendChild(btnIsporuci);
        })
    }

    isporuci(host)
    {
        let ime = this.naziv.replace(/\s+/g, '-');
        console.log(ime);
        let conts = this.cont.querySelectorAll(`.${ime}`);

        this.zarada += this.cena;
        let l;
        conts.forEach(cont => {
            l = cont.querySelector(".zarada");
            l.innerHTML = "Zarada: " + this.zarada;
        })
    }
}