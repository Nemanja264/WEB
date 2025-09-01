import { Racun } from "./racun.js";

function renderPage(host)
{
    const pageCont = document.createElement("div");
    pageCont.className = "page-container";
    host.appendChild(pageCont);

    renderStan(pageCont);
    renderRacuniContainer(pageCont);
}

function renderRacuniContainer(host)
{
    const racuniCont = document.createElement("div");
    racuniCont.className = "racuni-container";
    host.appendChild(racuniCont);
}

function renderStan(host)
{
    const stanCont = document.createElement("div");
    stanCont.className = "stan-container";
    host.appendChild(stanCont);

    stanChooser(stanCont);
}

function stanChooser(host)
{
    const stanChooser = document.createElement("div");
    stanChooser.className = "chooser-container";
    host.appendChild(stanChooser);

    const selectCont = document.createElement("div");
    selectCont.className = "select-container";
    stanChooser.appendChild(selectCont);

    const l = document.createElement("label");
    l.textContent = "Biraj stan:";
    selectCont.appendChild(l);

    const sel = document.createElement("select");
    selectCont.appendChild(sel);

    let opt = document.createElement('option');
    opt.value = '';
    opt.textContent = '';
    sel.appendChild(opt);

    addOptions(sel);

    const btn = document.createElement("button");
    btn.innerHTML = "Prikaz informacija";
    btn.addEventListener("click", () => {renderStanInfo(Number(sel.value), host), showRacune(Number(sel.value))});
    stanChooser.appendChild(btn);
}

function addOptions(sel)
{
    fetchStanove().then(stanovi => {
        const brojevi_stanova = stanovi.map(s => s.broj_stana);

        brojevi_stanova.forEach(stan => {
            const opt = document.createElement('option');
            opt.value = stan;
            opt.textContent = stan;
            sel.appendChild(opt);
        })
    }).catch(err => {
        console.error('Greška pri učitavanju stanova:', err);
    });
}

function renderStanInfo(broj_stana, host)
{
    let cont = document.querySelector(".stanInfo-container");
    if(cont)
        cont.remove();

    fetchStan(broj_stana).then(stan => {
        const stanInfoCont = document.createElement("div");
        stanInfoCont.className = "stanInfo-container";
        host.appendChild(stanInfoCont);

        writeStanInfos(stan, stanInfoCont);
    })
}

function writeStanInfos(stan, host)
{
    const infos = [
        ['Broj stana', stan.broj_stana],
        ['Ime vlasnika', stan.ime_vlasnika],
        ['Površina (m2)', stan['povrsina_m2']],
        ['Broj članova', stan['broj_clanova']],
    ];

    infos.forEach(([labela, value])  => {
        let div = document.createElement("div");
        host.appendChild(div);

        let l = document.createElement("label");
        l.textContent =  labela + ":";
        div.appendChild(l);

        l = document.createElement("label");
        l.textContent = value;
        div.appendChild(l);
    })

    
    const btn = document.createElement("button");
    btn.innerHTML = "Izracunaj dugovanje";
    btn.addEventListener("click", () => {
        const racuni = stan.racuni.filter(r => r.placen === false);
        let suma = 0;
        racuni.forEach(r => suma+=r.voda+r.struja+r.komunalne_usluge)

        alert(suma);
    })
    host.appendChild(btn);
}

function showRacune(broj_stana)
{
    const racuniCont = document.querySelector(".racuni-container");
    racuniCont.replaceChildren();

    fetchRacune(broj_stana).then(racuni => {
        racuni.forEach(r => {
            const racun = new Racun(r.mesec, r.voda, r.struja, r.komunalne_usluge, r.placen);
            racun.renderRacun(racuniCont);
        })
    })
}

function racunInputForm()
{
    
}

async function fetchStanove() {
    try {
        const response = await fetch("../js/data.json");
        if(!response.ok) throw new Error("Failed to fetch");

        const data = await response.json();
        const stanovi = data.stanovi;
        console.log(stanovi);

        return stanovi;

    } catch (error) {
        console.error("Fetch stanove failed:", error);
    }
}


async function fetchRacune(stan_broj) {
    try {
        const response = await fetch("../js/data.json")
        if(!response.ok) throw new Error("Failed to fetch");

        const data = await response.json();
        const stanovi = data.stanovi;
        console.log(stanovi);

        const stan = stanovi.find(s => s.broj_stana === stan_broj);
        
        return stan.racuni;

    } catch (error) {
        console.error("Fetch stanove failed:", error);
    }
}


async function fetchStan(stan_broj) {
    try {
        const response = await fetch("../js/data.json")
        if(!response.ok) throw new Error("Failed to fetch");

        const data = await response.json();
        const stanovi = data.stanovi;

        const stan = stanovi.find(s => s.broj_stana === stan_broj);
        
        return stan;

    } catch (error) {
        console.error("Fetch stanove failed:", error);
    }
}

renderPage(document.body);