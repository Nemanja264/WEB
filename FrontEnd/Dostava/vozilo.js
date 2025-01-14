export class Vozilo
{
    constructor(naziv, kapacitet, maxTezina, slikaLink)
    {
        this.naziv = naziv;
        this.kapacitet = kapacitet;
        this.maxTezina = maxTezina;
        this.slikaLink = slikaLink;
    }

    crtajSliku(host)
    {
        let img = document.createElement("img");
        img.src = this.slikaLink;
        host.appendChild(img);
    }
}