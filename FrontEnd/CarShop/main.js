import { Cars } from "./cars.js";

function drawApp(host)
{
    let contApp = document.createElement("div");
    contApp.className = "App";
    host.appendChild(contApp);

    let contSearch = document.createElement("div");
    contSearch.className = "Pretraga";
    contApp.appendChild(contSearch);

    let contCars = document.createElement("div");
    contCars.className = "Cars";
    contApp.appendChild(contCars);

    let d = document.createElement("div");
    d.className = "marka";
    contSearch.appendChild(d);
    let l = document.createElement("label");
    l.innerHTML = "Marka: ";
    d.appendChild(l);
    let selBrand = document.createElement("select");
    selBrand.className = "selMarka";
    d.appendChild(selBrand)
    
    d = document.createElement("div");
    d.className = "model";
    contSearch.appendChild(d);
    l = document.createElement("label");
    l.innerHTML = "Model: ";
    d.appendChild(l);
    let selModel = document.createElement("select");
    selModel.className = "selModel";
    d.appendChild(selModel)

    d = document.createElement("div");
    d.className = "boja";
    contSearch.appendChild(d);
    l = document.createElement("label");
    l.innerHTML = "Boja: ";
    d.appendChild(l);
    let selBoja = document.createElement("select");
    selBoja.className = "selBoja";
    d.appendChild(selBoja);

    fetchData(selBrand, selModel, selBoja);

    let btnPronadji = document.createElement("button");
    btnPronadji.className = "btnPronadji";
    btnPronadji.innerHTML = "Pronadji";
    btnPronadji.onclick=(ev)=>showCars(contCars, contSearch);
    contSearch.appendChild(btnPronadji);
}

function defaultVr(selOpt)
{
    let opt = document.createElement("option");
    opt.value = "";
    opt.innerHTML = "";
    opt.selected = true;
    opt.disabled = true;

    selOpt.appendChild(opt);
}

let carsArr = [];
function dodajKola(cars)
{
    cars.forEach(car => {
        car = new Cars(car.naziv, car.model, car.boja, car.kolicina, car.imgLink, car.cena);
        carsArr.push(car);
    })
}

async function fetchData(selBrand, selModel, selBoja)
{
    const cars = await getCars('cars.json');
    dodajKola(cars);

    let brand;
    let model;
    let boja;

    defaultVr(selModel);
    defaultVr(selBoja);

    let opcijeBrand = [];
    let boje = [];
    
    cars.forEach(c => {
        if(!opcijeBrand.includes(c.naziv))
        {
            opcijeBrand.push(c.naziv);
            brand = document.createElement("option");
            brand.value = c.naziv;
            brand.innerHTML = c.naziv;
            selBrand.appendChild(brand);
        }

        if(cars[0].naziv == c.naziv)
        {
            model = document.createElement("option");
            model.value = c.model;
            model.innerHTML = c.model;
            selModel.appendChild(model);

            boje.push(c.boja);
            boja = document.createElement("option");
            boja.value = c.boja;
            boja.innerHTML = c.boja;
            selBoja.appendChild(boja);
        }
    })

    selBrand.addEventListener("change",() =>{
        const selectedBrand = selBrand.value;
        
        while(selModel.options.length > 0)
            selModel.remove(0);
        while(selBoja.options.length > 0)
            selBoja.remove(0);

        defaultVr(selModel);
        defaultVr(selBoja);
        
        cars.forEach(c => {
            if(c.naziv == selectedBrand)
            {
                model = document.createElement("option");
                model.value = c.model;
                model.innerHTML = c.model;
                selModel.appendChild(model);

                boja = document.createElement("option");
                boja.value = c.boja;
                boja.innerHTML = c.boja;
                selBoja.appendChild(boja);
            }
        })
    })

    selModel.addEventListener("change",() =>{
        if(selBoja.value != "")
            return;

        const selectedModel = selModel.value;
        
        while(selBoja.options.length > 0)
            selBoja.remove(0);

        defaultVr(selBoja);
        cars.forEach(c => {
            if(c.model == selectedModel)
            {
                boja = document.createElement("option");
                boja.value = c.boja;
                boja.innerHTML = c.boja;
                selBoja.appendChild(boja);
            }
        })
    })

    selBoja.addEventListener("change",() =>{
        if(selModel.value != "")
            return;

        const selectedBrand = selBrand.value;
        const selectedBoja = selBoja.value;
        
        while(selModel.options.length > 0)
            selModel.remove(0);
        
        defaultVr(selModel);
        cars.forEach(c => {
            if(c.boja == selectedBoja && c.naziv == selectedBrand)
            {
                model = document.createElement("option");
                model.value = c.model;
                model.innerHTML = c.model;
                selModel.appendChild(model);
            }
        })
    })
}


async function showCars(host)
{
    obrisiPrethodno();

    let foundCars;

    let brand = document.querySelector(".selMarka").value;

    let model = document.querySelector(".selModel").value;
    let boja = document.querySelector(".selBoja").value;
    
    if(model == "" && boja == "")
        foundCars = carsArr.filter(car => car.naziv == brand);
    else if(boja == "" && model != "")
        foundCars = carsArr.filter(car => car.naziv == brand && car.model == model);
    else if(boja != "" && model == "")
        foundCars = carsArr.filter(car => car.naziv == brand && car.boja == boja);
    else
        foundCars = carsArr.filter(car => car.naziv == brand && car.model == model && car.boja == boja);

    foundCars.forEach(car => {
        car.crtaj(host);
    })
}

async function getCars(link) {
    try 
    {
        const response = await fetch(link);
        if(!response.ok)
        {
            throw new Error("FetchSearch failed");
        }

        const cars = await response.json();

        return cars;
    } 
    catch (error) {
        console.error("Fetch failed: ", error);
    }
}

function obrisiPrethodno()
{
    var cars = document.querySelectorAll(".Car");

    let parent;
    cars.forEach(c => {
        parent = c.parentNode;
        parent.removeChild(c);
    })
}

drawApp(document.body);