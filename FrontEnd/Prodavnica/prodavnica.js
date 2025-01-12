export class Prodavnica
{
    constructor(naziv, lokacija, brtel)
    {
        this.naziv = naziv;
        this.lokacija = lokacija;
        this.telefon = brtel;
        this.proizvodi = [];
        this.container = null;
    }

    crtajProdavnicu(host)
    {
        this.container = host;

        let h3 = document.createElement("h3");
        h3.innerHTML = "Prodavnica: " + this.naziv;
        host.appendChild(h3);

        let prikaz = document.createElement("div");
        prikaz.className = "prikaz";
        host.appendChild(prikaz);

        let divBars = document.createElement("div");
        divBars.className = "products";
        prikaz.appendChild(divBars);

        let divProdaja = document.createElement("div");
        divProdaja.className = "kupovina";
        prikaz.appendChild(divProdaja);
    }

    dodajProizvod(proizvod)
    {
        let existingPR;
        if(existingPR = this.proizvodi.find(p => p.naziv == proizvod.naziv))
        {
            if(existingPR.kolicina == 100)
            {
                alert("Maximalan broj ovog proizvoda je dostignut");
                return;
            }
            
            existingPR.update(proizvod.kolicina, proizvod.cena);
            return;
        }

        this.proizvodi.push(proizvod);
        console.log(proizvod);

        let p = this.container.querySelector(".prikaz");
        let d = this.container.querySelector(".kupovina");
        let divProdaja = document.createElement("div");
        divProdaja.className = "prodaja";
        divProdaja.classList.add(proizvod.naziv);
        d.appendChild(divProdaja);

        let l = document.createElement("label");
        l.innerHTML = "Kolicina:";
        divProdaja.appendChild(l);

        let input = document.createElement("input");
        input.min = '0';
        input.max = proizvod.kolicina;
        input.step = '1'
        divProdaja.appendChild(input);

        let btnProdaj = document.createElement("button");
        btnProdaj.innerHTML = "Prodaj";
        btnProdaj.onclick=(ev)=>this.prodaja(divProdaja);
        divProdaja.appendChild(btnProdaj);

        proizvod.crtajProizvod(p);
    }

    prodaja(host)
    {
        let trazenaKolicina = host.querySelector("input").value;
        let product = this.proizvodi.find(proizvod => proizvod.naziv == host.classList[1]);
        
        if(trazenaKolicina > product.kolicina)
        {
            alert("Trazili ste vise nego sto ima dostupnih proizvoda");
            return;
        }
        else  
            product.kolicina -= trazenaKolicina;

        product.prodajProizvod();
    }
}