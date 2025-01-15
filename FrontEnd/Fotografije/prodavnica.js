export class Prodavnica
{
    constructor(naziv, cont)
    {
        this.naziv = naziv;
        this.zarada = 0;
        this.slike = [];
        this.container = cont;
    }

    dodajSliku(slika)
    {
        this.slike.push(slika);
    }

    crtajSlike(dimenzija, papir)
    {
        this.obrisiPrethodno(this.container);
        let photos;
        if(dimenzija != "" && papir != "")
        {
            photos = this.slike.filter(slika => slika.dimenzija == dimenzija && slika.papir == papir);
        }
        else if(dimenzija != "" && papir == "")
        {
            photos = this.slike.filter(slika => slika.dimenzija == dimenzija);
        }
        else if(dimenzija == "" && papir != "")
        {
            photos = this.slike.filter(slika => slika.papir == papir);
        }
        else
        {
            alert("Odaberite kriterijume");
            return;
        }

        photos.forEach(p => {
            let d = document.createElement("div");
            d.className = "slika";
            this.container.appendChild(d);
            p.dodajCont(d);

            let l = document.createElement("label");
            l.innerHTML = `${p.naziv} - ${p.dimenzija} (${p.papir})`;
            d.appendChild(l);

            let btnKupi = document.createElement("button");
            btnKupi.innerHTML = "Kupi";
            btnKupi.onclick=(ev)=>this.prodajSliku(p);
            d.appendChild(btnKupi);
        })
    }

    prodajSliku(photo)
    {
        let ramSlike = document.querySelector(".ram select");
        if(ramSlike.value == "")
        {
            alert("Odaberite ram za sliku!");
            return;
        }
        photo.dodajRam(ramSlike.value);

        photo.prodajSliku();
        this.zarada += photo.cena;
        let head = document.querySelector(".header label");
        head.innerHTML = `${this.naziv}: ${this.zarada}`;
        
        const index = this.slike.findIndex(slika => slika === photo);
        this.slike.splice(index, 1);
        
        alert("Slika sa ramom " + ramSlike.value + " je prodata!");
    }

    obrisiPrethodno(host)
    {
        var pics = host.querySelectorAll(".slika");

        pics.forEach(p => {
            host.removeChild(p);
        })
    }
}