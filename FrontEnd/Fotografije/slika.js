export class Slika
{
    constructor(naziv, dimenzija, papir, cena)
    {
        this.naziv = naziv;
        this.dimenzija = dimenzija;
        this.papir = papir;
        this.cena = cena;
        this.ram = null;
        this.cont = null;
    }

    dodajCont(cont)
    {
        this.cont = cont;
    }

    dodajRam(ram)
    {
        this.ram = ram;
    }

    prodajSliku()
    {
        let parent = this.cont.parentNode;

        parent.removeChild(this.cont);

        if(parent.querySelector(".slika") == null)
        {
            let dimenzijaSel = document.querySelector(".dimenzija select");
            if(dimenzijaSel.value != "")
            {
                const index = dimenzijaSel.selectedIndex;
                dimenzijaSel.remove(index);
            }

            let papirSel = document.querySelector(".papir select");
            if(papirSel.value != "")
            {
                const index = papirSel.selectedIndex;
                papirSel.remove(index);
            }
        }
    }
}