export class Racun
{
    constructor(mesec, voda, struja, komunalije, placen)
    {
        this.mesec = mesec;
        this.voda = voda;
        this.struja = struja;
        this.komunalije = komunalije;
        this.placen = placen;
        this.container = null;
    }

    renderRacun(host)
    {
        this.container = document.createElement("div");
        this.container.className = "racun";
        this.container.style.backgroundColor = this.placen ? "#16a34a" : "#dc2626";
        host.appendChild(this.container);

        const billData=[
            ['Mesec', this.mesec],
            ['Struja', this.struja],
            ['Komunalije', this.komunalije],
            ['Placeno', this.placen ? 'Da' : 'Ne'],
        ];
        this.renderInfo(this.container, billData);
    }

    renderInfo(host, pairs)
    {
        let label, value;
        const dl = document.createElement("dl");
        pairs.forEach(([label, value]) => {
            const dt = document.createElement('dt');
            dt.textContent = label + ":";

            const dd = document.createElement('dd');
            dd.textContent = value;
            dl.append(dt,dd);
        })
        

        host.appendChild(dl);
    }
}