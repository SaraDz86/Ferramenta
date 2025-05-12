using Ferramenta.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

var app = builder.Build();

#region Endpoint personalizzati

List<Prodotto> elenco = new List<Prodotto>()
{
    new Prodotto(){
        Nome = "Martello in acciaio",
        Cod = "FERR001",
        Descrizione = "Martello con impugnatura ergonomica, ideale per lavori di carpenteria e costruzioni.",
        Prezzo = 15.99m,
        Categoria = "Attrezzi"
    },
    new Prodotto(){
        Nome = "Trapano cordless 18V",
        Cod = "FERR002",
        Descrizione = "Trapano senza fili ad alte prestazioni, adatto per foratura e avvitamento in diversi contesti.",
        Prezzo = 89.50m,
        Categoria = "Attrezzi"
    },
    new Prodotto(){
        Nome = "Cavo elettrico 50m",
        Cod = "FERR003",
        Descrizione = "Cavo elettrico isolato, resistente e ideale per impianti industriali ed edili.",
        Prezzo = 49.99m,
        Categoria = "Materiale Elettrico"
    }
};

app.MapGet("/", () => elenco);

app.MapGet("/{cod}", (string cod) =>
{
    Prodotto? prod = elenco.FirstOrDefault(p => p.Cod == cod);
    if (prod is not null)
        return Results.Ok(prod);
    return Results.NotFound();
});

app.MapPost("/", (Prodotto prodotto) =>
{
    if (string.IsNullOrEmpty(prodotto.Nome) || string.IsNullOrEmpty(prodotto.Cod))
        return Results.BadRequest("Il campo Nome e Codice sono obbligatori.");

    if (elenco.Any(p => p.Cod == prodotto.Cod))
        return Results.BadRequest("Il Codice deve essere univoco.");

    elenco.Add(prodotto);
    return Results.Ok(prodotto);
});

app.MapDelete("/{cod}", (string cod) =>
{
    Prodotto? prod = elenco.FirstOrDefault(p => p.Cod == cod);
    if (prod is not null)
    {
        elenco.Remove(prod);
        return Results.Ok();
    }
    return Results.NotFound();
});

app.MapPut("/{cod}", (string cod, Prodotto prodottoAggiornato) =>
{
    if (string.IsNullOrEmpty(prodottoAggiornato.Nome) || string.IsNullOrEmpty(prodottoAggiornato.Cod))
        return Results.BadRequest("Il campo Nome e il Codice sono obbligatori.");

    Prodotto? prod = elenco.FirstOrDefault(p => p.Cod == cod);
    if (prod is null)
        return Results.NotFound();

    prod.Nome = prodottoAggiornato.Nome;
    prod.Descrizione = prodottoAggiornato.Descrizione;
    prod.Prezzo = prodottoAggiornato.Prezzo;
    prod.Categoria = prodottoAggiornato.Categoria;

    return Results.Ok(prod);
});

#endregion

app.UseCors(policy => policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());

app.Run();

