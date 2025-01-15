import { Prodavnica } from "./prodavnica.js";
import { Slika } from "./slika.js";

function drawForm(host)
{
    let contApp = document.createElement("div");
    contApp.className = "app";
    host.appendChild(contApp);
    
    drawHeader(contApp);

    let contForm = document.createElement("div");
    contForm.className = "form";
    contApp.appendChild(contForm);

    let contPretraga = document.createElement("div");
    contPretraga.className = "pretraga";
    contForm.appendChild(contPretraga);

    let contPrikaz = document.createElement("div");
    contPrikaz.className = "prikaz";
    contForm.appendChild(contPrikaz);

    drawPretraga(contPretraga);
}

function drawPretraga(host)
{
    let d = document.createElement("div");
    d.className = "dimenzija";
    host.appendChild(d);
    let l = document.createElement("label");
    l.innerHTML = "Dimenzija:";
    d.appendChild(l);
    let sel = document.createElement("select");
    d.appendChild(sel);

    d = document.createElement("div");
    d.className = "papir";
    host.appendChild(d);
    l = document.createElement("label");
    l.innerHTML = "Papir:";
    d.appendChild(l);
    sel = document.createElement("select");
    d.appendChild(sel);

    d = document.createElement("div");
    d.className = "ram";
    host.appendChild(d);
    l = document.createElement("label");
    l.innerHTML = "Ram:";
    d.appendChild(l);
    sel = document.createElement("select");
    d.appendChild(sel);

    let btnPrikazi = document.createElement("button");
    btnPrikazi.innerHTML = "Prikazi";
    btnPrikazi.onclick=(ev)=>prikaziSlike(host);
    host.appendChild(btnPrikazi);

    fetchData(host)
    fetchRams(host);
}

function prikaziSlike(host)
{
    let papir = host.querySelector(".papir select").value;
    let dimenzija = host.querySelector(".dimenzija select").value;

    let photos;

    prodavnica.crtajSlike(dimenzija, papir);

    /*let dimenzijaSel = host.querySelector(".dimenzija select");
    let papirSel = host.querySelector(".papir select");
    let ramSel = host.querySelector(".ram select");

    dimenzijaSel.options[0].selected = true;
    papirSel.options[0].selected = true;
    dimenzijaSel.options[0].value = "";
    papirSel.options[0].value = "";*/
}

async function drawHeader(host) {
    try 
    {
        let divHeader = document.createElement("div");
        divHeader.className = "header";
        host.appendChild(divHeader);

        const response = await fetch('js/prodavnica.json');
        if(!response.ok) throw new Error("Failed to fetch");

        const shop = await response.json();

        let l = document.createElement("label");
        l.innerHTML = `${shop.naziv}: ${shop.zarada}`;
        divHeader.appendChild(l);
    } 
    catch (error) {
        console.error("Fetch failed:", error);
    }
}

let prodavnica = null;
async function fetchData(host) {
    try 
    {
        const response = await fetch('js/prodavnica.json');
        if(!response.ok) throw new Error("Failed to fetch");

        const shop = await response.json();

        let opt;
        let dimenzijaSel = host.querySelector(".dimenzija select");
        let papirSel = host.querySelector(".papir select");

        opt = document.createElement("option");
        opt.innerHTML = "";
        dimenzijaSel.appendChild(opt);
        opt = document.createElement("option");
        opt.innerHTML = "";
        papirSel.appendChild(opt);
        

        dimenzijaSel.options[0].selected = true;
        papirSel.options[0].selected = true;
        dimenzijaSel.options[0].value = "";
        papirSel.options[0].value = "";

        let dims = new Set();
        let papers = new Set();
        shop.slike.forEach(s => {
            if(!dims.has(s.dimenzija))
            {
                dims.add(s.dimenzija);
                opt = document.createElement("option");
                opt.innerHTML = s.dimenzija;
                dimenzijaSel.appendChild(opt);
            }    
            
            if(!papers.has(s.papir))
            {
                papers.add(s.papir);
                opt = document.createElement("option");
                opt.innerHTML = s.papir;
                papirSel.appendChild(opt);
            }
        })
        

        let prikaz = document.body.querySelector(".prikaz");
        prodavnica = new Prodavnica(shop.naziv, prikaz);
        let photo;
        shop.slike.forEach(s => {
            photo = new Slika(s.naziv, s.dimenzija, s.papir, s.cena);
            prodavnica.slike.push(photo);
        })
    } 
    catch (error) {
        console.error("Fetch failed:", error);
    }
}

async function fetchRams(host) {
    try 
    {
        const response = await fetch("js/ram.json");
        if(!response.ok) throw new Error("Failed to fetch");

        const ramovi = await response.json();
        
        let ramSel = host.querySelector(".ram select");
        let opt;
        opt = document.createElement("option");
        opt.innerHTML = "";
        ramSel.appendChild(opt);
        
        ramovi.forEach(ram => {
            opt = document.createElement("option");
            opt.innerHTML = ram;
            ramSel.appendChild(opt);
        })
    }
    catch (error) {
        console.error("Fetch failed:", error);
    }
}

drawForm(document.body);