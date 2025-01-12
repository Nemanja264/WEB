export class Proizvod
{
    constructor(naziv, kategorija, cena, kolicina)
    {
        this.naziv = naziv;
        this.kategorija = kategorija;
        this.cena = cena;
        this.kolicina = kolicina;
        this.cont = null;
    }

    crtajProizvod(host)
    {
        let divProducts = host.querySelector(".products");
        this.cont = document.createElement("div");
        divProducts.appendChild(this.cont);

        let l = document.createElement("label");
        l.innerHTML = `${this.naziv}: ${this.kolicina}`;
        this.cont.appendChild(l);

        let barCont = document.createElement("div");
        barCont.className = "barCont";
        barCont.classList.add(this.naziv);
        this.cont.appendChild(barCont);
        
        let bar = document.createElement("div");
        bar.style.backgroundColor = "green";
        bar.style.width = `${this.kolicina*4}px`;
        bar.className = "bar";
        barCont.appendChild(bar);
    }

    prodajProizvod()
    {
        let l = this.cont.querySelector("label");
        l.innerHTML = `${this.naziv}: ${this.kolicina}`;

        let bar = this.cont.querySelector(".bar")
        bar.style.width = `${this.kolicina*4}px`;
    }

    update(dodataKolicina, cena)
    {
        if(this.kolicina + dodataKolicina > 100)
            this.kolicina = 100;
        else
            this.kolicina += dodataKolicina;
        
        let l = this.cont.querySelector("label");
        l.innerHTML = `${this.naziv}: ${this.kolicina}`;

        let bar = this.cont.querySelector(".bar")
        bar.style.width = `${this.kolicina*4}px`;

        this.cena = cena;
    }
}