import { Prodavnica } from "./prodavnica.js";
import { Proizvod } from "./proizvod.js";

function drawProdProiz(host, shop)
{
    let ProdProiz = document.createElement("div");
    ProdProiz.className = "ProdProiz";
    host.appendChild(ProdProiz);

    let contProiz = document.createElement("div");
    contProiz.className = "Proizvod";
    ProdProiz.appendChild(contProiz);

    let contProdavnica = document.createElement("div");
    contProdavnica.className = "Prodavnica";
    ProdProiz.appendChild(contProdavnica);

    let h4 = document.createElement("h4");
    h4.innerHTML = "Upis proizvoda";
    contProiz.appendChild(h4);

    let div = document.createElement("div");
    contProiz.appendChild(div);
    let l = document.createElement("label");
    l.innerHTML = "Naziv:";
    div.appendChild(l);
    let input = document.createElement("input");
    input.type = "text";
    input.className = "nazivInput";
    div.appendChild(input);

    div = document.createElement("div");
    div.className = "categoryInput";
    contProiz.appendChild(div);
    l = document.createElement("label");
    l.innerHTML = "Kategorija:";
    div.appendChild(l);
    let sel = document.createElement("select");
    div.appendChild(sel);

    let categories = ["knjiga", "igracka", "pribor", "ostalo"];
    let opt;
    categories.forEach((c, index) => {
        opt = document.createElement("option");
        opt.value = c;
        opt.innerHTML = c;
        if(index == 0)
            opt.selected = true;
        sel.appendChild(opt);
    })

    div = document.createElement("div");
    contProiz.appendChild(div);
    l = document.createElement("label");
    l.innerHTML = "Cena:";
    div.appendChild(l);
    input = document.createElement("input");
    input.type = "number";
    input.min = '0';
    input.step = '1';
    input.className = "cenaInput";
    div.appendChild(input);

    div = document.createElement("div");
    contProiz.appendChild(div);
    l = document.createElement("label");
    l.innerHTML = "Kolicina:";
    div.appendChild(l);
    input = document.createElement("input");
    input.type = "number";
    input.min = '0';
    input.step = '1';
    input.max = '100';
    input.className = "kolicinaInput";
    div.appendChild(input);

    let btnDodaj = document.createElement("button");
    btnDodaj.innerHTML = "Dodaj proizvod";
    contProiz.appendChild(btnDodaj);

    let prodavnica = new Prodavnica(shop.naziv, shop.lokacija, shop.brtel);
    prodavnica.crtajProdavnicu(contProdavnica);

    btnDodaj.onclick=(ev)=>dodaj(contProiz, prodavnica);
}

async function drawApp(host)
{
    try 
    {
        const response = await fetch('shops.json');
        if(!response.ok) throw new Error("Failed to fetch");

        const shops = await response.json();

        let contApp = document.createElement("div");
        host.appendChild(contApp);

        shops.forEach(shop => {
            drawProdProiz(contApp, shop);
        })
        
    } 
    catch (error) 
    {
        console.error("Fetch failed: ", error);
    }
    
}

function dodaj(contProiz, prodavnica)
{
    let naziv = contProiz.querySelector(".nazivInput").value;
    let cenaNot = contProiz.querySelector(".cenaInput").value;
    let cenaParsed = parseInt(cenaNot, 10);
    let kolicinaNot = contProiz.querySelector(".kolicinaInput").value;
    let kolicinaParsed = parseInt(kolicinaNot, 10);
    let kategorija = contProiz.querySelector("select").value;

    let proizvod = new Proizvod(naziv, kategorija, cenaParsed, kolicinaParsed);

    prodavnica.dodajProizvod(proizvod);
}

drawApp(document.body);

