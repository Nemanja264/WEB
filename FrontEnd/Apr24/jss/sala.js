export class Sala{
    constructor(id, naziv, brRedova, sedista_po_redu, projekcija)
    {
        this.id = id;
        this.naziv = naziv;
        this.brRedova = brRedova;
        this.sedista_po_redu = sedista_po_redu;
        this.projekcija = projekcija;
        this.sedista = [];
        this.kupovinaCont = null;
    }



    drawSala(host)
    {
        const h3 = document.createElement("h3");
        h3.innerHTML = `${this.projekcija.film}: ${this.projekcija.termin} - ${this.naziv}`
        host.appendChild(h3);

        const salaPrikaz = document.createElement("div");
        salaPrikaz.classList.add("prikaz-container");
        host.appendChild(salaPrikaz);

        const kupiCont = document.createElement("div");
        kupiCont.classList.add("kupi-container");
        salaPrikaz.appendChild(kupiCont);
        this.kupovinaCont = kupiCont;

        this.drawKupovina(kupiCont);

        const karteCont = document.createElement("div");
        karteCont.classList.add("karte-container");
        salaPrikaz.appendChild(karteCont);

        this.drawSedista(karteCont);
    }

    drawKupovina(host)
    {
        const h3 = document.createElement("h3");
        h3.innerHTML = "Kupi kartu";
        host.appendChild(h3);

        const labele = [["Red", "red"], ["Broj sedista", "sediste"], ["Cena karte", "cena"], ["Sifra","sifra"]];
        labele.forEach(([l, polje]) => {
            const div = document.createElement("div");
            host.appendChild(div);

            const label = document.createElement("label");
            label.textContent = l+":";
            div.appendChild(label);

            const input = document.createElement("input");
            input.className = `${polje}-input`;
            if(l == "Sifra")
                input.type = "text";
            else
                input.type = "number";
            input.readOnly = true;
            div.appendChild(input);
        })

        const btn = document.createElement("button");
        btn.textContent = "Kupi Kartu";
        btn.addEventListener("click", () => this.kupiKartu(host));
        host.appendChild(btn);
    }

    drawSedista(host)
    {
        for(let i=0; i<this.brRedova; i++)
        {
            const div = document.createElement("div");
            div.className = "red";
            host.appendChild(div);

            for(let j=0; j<this.sedista_po_redu;j++)
            {
                const karta = this.projekcija.karte.find(k => k.red === (i+1) && k.sediste === (j+1));
                console.log(karta);

                const btn = document.createElement("button");
                btn.textContent = `Red: ${i+1}, Broj: ${j+1}`;
                btn.className = `Sala-${i+1}-${j+1}`;
                btn.style.backgroundColor = !karta.zauzeto ?  "#bbf7d0" : "#fecaca";
                btn.addEventListener("click", () => karta.fillKartaInput(this.kupovinaCont));
                div.appendChild(btn);
            }
        }
    }

    kupiKartu(host)
    {
        const red = host.querySelector(".red-input").value;
        console.log(red);
        const sediste = host.querySelector(".sediste-input").value;

        const mesto = document.querySelector(`.Sala-${red}-${sediste}`);

        const karta = this.projekcija.karte.find(k => Number(k.red) === Number(red) && Number(k.sediste) === Number(sediste));
        if(karta.zauzeto === true)
            alert("Sediste je zauzeto!");
        else
        {
            karta.zauzeto = true;
            alert(`Karta je kupljena, Red:${karta.red}, Sediste: ${karta.sediste}, Cena: ${karta.cena}`);
            mesto.style.backgroundColor = "#fecaca";
        }
        
        console.log(karta);
    }
}