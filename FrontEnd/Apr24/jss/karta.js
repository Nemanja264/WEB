export class Karta{
    constructor(red, sediste, cena, sifra, zauzeto)
    {
        this.red = red;
        this.sediste = sediste;
        this.cena = cena;
        this.sifra = sifra;
        this.zauzeto = zauzeto;
    }

    fillKartaInput(cont)
    {
        const inputs = [["red", this.red], ["sediste",this.sediste], ["cena", this.cena], ["sifra", this.sifra]];

        inputs.forEach(([i, value]) => {
            const input = cont.querySelector(`.${i}-input`);
            input.value = value;
        })
    }
}