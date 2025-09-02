import { Sala } from "./sala.js";
import { Karta } from "./karta.js";
import { Projekcija } from "./projekcija.js";

const response = await fetch("js/data.json");
const data = await response.json();

const sale = data.sale;
console.log(sale);

const projekcije = data.projekcije;
const karte = data.karte;

function drawPage(host)
{
    const pageCont = document.createElement("div");
    pageCont.className = "page-container";
    host.appendChild(pageCont);

    drawSale(pageCont);
}

function drawSale(host)
{
    sale.forEach(s => {
        const salaCont = document.createElement("div");
        salaCont.className = "sala-container";
        host.appendChild(salaCont);

        const pr = projekcije.find(p => p.sala_id === s.id);
        const karteZaProjekciju = [];
        const ks = karte.filter(k => k.projekcija_id === pr.id);
        ks.forEach(k => {
            karteZaProjekciju.push(new Karta(k.red, k.broj, k.cena, pr.sifra, k.zauzeto));
        })
        console.log(ks);

        const termin = new Date(pr.termin).toLocaleString('sr-RS',{dateStyle:'short',timeStyle:'short'});
        const sala = new Sala(s.id, s.naziv, s.redova, s.sedista_po_redu, new Projekcija(pr.id, pr.film, termin, pr.sala_id, pr.sifra, karteZaProjekciju));

        sala.drawSala(salaCont);
    })
}

drawPage(document.body);