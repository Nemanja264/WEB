import { Vozilo } from "./vozilo.js";
import { Firma } from "./firma.js";

function drawForm(host)
{
    let appCont = document.createElement("div");
    appCont.className = "appCont";
    host.appendChild(appCont);

    let divForm = document.createElement("div");
    divForm.className = "Forma";
    appCont.appendChild(divForm);

    let divFirme = document.createElement("div");
    divFirme.className = "Firme";
    appCont.appendChild(divFirme);

    let d = document.createElement("div");
    d.className = "zapremina";
    divForm.appendChild(d);
    let l = document.createElement("label");
    l.innerHTML = "Zapremina (cm^3):"
    d.appendChild(l);
    let input = document.createElement("input");
    input.type = "number";
    input.value = 10000;
    d.appendChild(input);

    d = document.createElement("div");
    d.className = "tezina";
    divForm.appendChild(d);
    l = document.createElement("label");
    l.innerHTML = "Tezina (kg):"
    d.appendChild(l);
    input = document.createElement("input");
    input.type = "number";
    input.value = 1500;
    d.appendChild(input);

    d = document.createElement("div");
    d.className = "datumPrijema";
    divForm.appendChild(d);
    l = document.createElement("label");
    l.innerHTML = "Datum prijema:"
    d.appendChild(l);
    input = document.createElement("input");
    input.type = "date";
    input.value = '2025-01-06';
    d.appendChild(input);

    d = document.createElement("div");
    d.className = "datumDostave";
    divForm.appendChild(d);
    l = document.createElement("label");
    l.innerHTML = "Datum dostave:"
    d.appendChild(l);
    input = document.createElement("input");
    input.type = "date";
    input.value = '2025-01-10';
    d.appendChild(input);

    d = document.createElement("div");
    d.className = "cenaLow";
    divForm.appendChild(d);
    l = document.createElement("label");
    l.innerHTML = "Cena od:"
    d.appendChild(l);
    input = document.createElement("input");
    input.type = "number";
    input.value = 100000;
    d.appendChild(input);

    d = document.createElement("div");
    d.className = "cenaMax";
    divForm.appendChild(d);
    l = document.createElement("label");
    l.innerHTML = "Cena do:"
    d.appendChild(l);
    input = document.createElement("input");
    input.type = "number";
    input.value = 200000;
    d.appendChild(input);

    let btnPronadji = document.createElement("button");
    btnPronadji.innerHTML = "Pronadji";
    btnPronadji.onclick=(ev)=>pronadjiFirme();
    divForm.appendChild(btnPronadji);
}

function pronadjiFirme()
{
    obrisiPrethodno();

    let forma = document.querySelector(".Forma");
    let contFirme = document.querySelector(".Firme");

    let v = forma.querySelector(".zapremina input").value;
    let m = forma.querySelector(".tezina input").value;
    let prijem = forma.querySelector(".datumPrijema input").value;
    let dostava = forma.querySelector(".datumDostave input").value;
    let cenaLow = forma.querySelector(".cenaLow input").value;
    let cenaMax = forma.querySelector(".cenaMax input").value;

    console.log(prijem);

    let firme = companies.filter(company => (company.cena > cenaLow && company.cena < cenaMax)
        && (company.lowDate < prijem && company.upperDate > dostava));
    
    console.log(firme);
    firme.forEach(f => {
        f.crtajTrazeno(contFirme, v, m);
    })
}

async function ucitajFirme()
{
    try 
    {
        const response = await fetch('js/firme.json');
        if(!response.ok) throw new Error("Failed to fetch");

        const firme = await response.json();

        let firma;
        firme.forEach(f => {
            firma = new Firma(f.naziv, f.cena, f.lowDate, f.upperDate);

            let vozilo;
            f.vozila.forEach(v => {
                vozilo = new Vozilo(v.naziv, v.kapacitet, v.maxTezina, v.imgUrl);
                firma.dodajVozilo(vozilo);
            })

            companies.push(firma);
        });

        console.log(companies);   
    }
    catch (error) 
    {
        console.error("Fetch failed: ", error);
    }
}

let companies = [] // nije clean code,
        // ali nemam kreiranu full bazu sa podacima
        // pa ovako mora radi lakseg updatea i pristupanja

function obrisiPrethodno()
{
    var vozila = document.querySelectorAll(".contVozilo");

    let parent;
    vozila.forEach(v => {
        parent = v.parentNode;
        parent.removeChild(v);
    })
}

ucitajFirme();
drawForm(document.body);