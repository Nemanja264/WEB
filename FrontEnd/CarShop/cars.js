export class Cars
{
    constructor(naziv, model, boja, kolicina, imgLink, cena)
    {
        this.naziv = naziv;
        this.model = model;
        this.boja = boja;
        this.kolicina = kolicina;
        this.img = imgLink;
        this.cena = cena;
        this.lastBuy = null;
        this.container = null;
    }

    crtaj(host)
    {
        let divCar = document.createElement("div");
        divCar.className = "Car";
        host.appendChild(divCar);
        this.container = divCar;

        let l = document.createElement("label");
        l.innerHTML = "Marka: " + this.naziv;
        divCar.appendChild(l);

        l = document.createElement("label");
        l.innerHTML = "Model: " + this.model;
        divCar.appendChild(l);

        let slika = document.createElement("img");
        slika.src = this.img;
        divCar.appendChild(slika);

        l = document.createElement("label");
        l.className = "Kolicina";
        l.innerHTML = "Kolicina: " + this.kolicina;
        divCar.appendChild(l);

        l = document.createElement("label");
        l.className = "Datum";
        l.innerHTML = "Datum poslednje prodaje: ";
        if(this.lastBuy != null) 
            l.innerHTML+= this.lastBuy.getDate() + "/" + (this.lastBuy.getMonth()+1) + "/" +
            + this.lastBuy.getFullYear();
        else
            l.innerHTML += this.lastBuy;
        divCar.appendChild(l);

        l = document.createElement("label");
        l.innerHTML = "Cena: " + this.cena;
        divCar.appendChild(l);

        let btnNaruci = document.createElement("button");
        btnNaruci.innerHTML = "Naruci";
        btnNaruci.onclick=(ev)=>this.orderCar();
        divCar.appendChild(btnNaruci);
    }

    orderCar()
    {
        if(this.kolicina == 0)
        {
            alert("No available cars for this model");
            return;
        }

        let datumLabela = this.container.querySelector(".Datum");
        this.lastBuy = new Date();
        datumLabela.innerHTML = "Datum poslednje prodaje: " 
            + this.lastBuy.getDate() + "/" + (this.lastBuy.getMonth()+1) + "/" +
            + this.lastBuy.getFullYear();

        let kolicinaLabela = this.container.querySelector(".Kolicina");
        this.kolicina--;
        kolicinaLabela.innerHTML = "Kolicina: " + this.kolicina;
    }

}