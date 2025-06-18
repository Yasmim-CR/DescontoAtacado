var produtos = new List<Produto>
{
    new() { Gtin = 7891024110348, Descricao = "SABONETE OLEO DE ARGAN 90G PALMOLIVE", PrecoVarejo = 2.88m, PrecoAtacado = 2.51m, MinUnidadesAtacado = 12 },
    new() { Gtin = 7891048038017, Descricao = "CHÁ DE CAMOMILA DR.OETKER", PrecoVarejo = 4.40m, PrecoAtacado = 4.37m, MinUnidadesAtacado = 3 },
    new() { Gtin = 7896066334509, Descricao = "TORRADA TRADICIONAL WICKBOLD PACOTE", PrecoVarejo = 5.19m },
    new() { Gtin = 7891700203142, Descricao = "BEBIDA À BASE DE SOJA MAÇÃ ADES CAIXA 200ML", PrecoVarejo = 2.39m, PrecoAtacado = 2.38m, MinUnidadesAtacado = 6 },
    new() { Gtin = 7894321711263, Descricao = "ACHOCOLATADO PÓ ORIGINAL TODDY POTE 400G", PrecoVarejo = 9.79m },
    new() { Gtin = 7896001250611, Descricao = "ADOÇANTE LÍQUIDO SUCRALOSE LINEA CAIXA 25ML", PrecoVarejo = 9.89m, PrecoAtacado = 9.10m, MinUnidadesAtacado = 10 },
    new() { Gtin = 7793306013029, Descricao = "CEREAL MATINAL CHOCOLATE KELLOGGS SUCRILHOS CAIXA 320G", PrecoVarejo = 12.79m, PrecoAtacado = 12.35m, MinUnidadesAtacado = 3 },
    new() { Gtin = 7896004400914, Descricao = "COCO RALADO SOCOCO 50G", PrecoVarejo = 4.20m, PrecoAtacado = 4.05m, MinUnidadesAtacado = 6 },
    new() { Gtin = 7898080640017, Descricao = "LEITE UHT INTEGRAL 1L COM TAMPA ITALAC", PrecoVarejo = 6.99m, PrecoAtacado = 6.89m, MinUnidadesAtacado = 12 },
    new() { Gtin = 7891025301516, Descricao = "DANONINHO PETIT SUISSE COM POLPA DE MORANGO 360G DANONE", PrecoVarejo = 12.99m },
    new() { Gtin = 7891030003115, Descricao = "CREME DE LEITE LEVE 200G MOCOCA", PrecoVarejo = 3.12m, PrecoAtacado = 3.09m, MinUnidadesAtacado = 4 },
};

var vendas = new List<ItemVenda>
{
    new() { Id = 1, Gtin = 7891048038017, Quantidade = 1, Parcial = 4.40m },
    new() { Id = 2, Gtin = 7896004400914, Quantidade = 4, Parcial = 16.80m },
    new() { Id = 3, Gtin = 7891030003115, Quantidade = 1, Parcial = 3.12m },
    new() { Id = 4, Gtin = 7891024110348, Quantidade = 6, Parcial = 17.28m },
    new() { Id = 5, Gtin = 7898080640017, Quantidade = 24, Parcial = 167.76m },
    new() { Id = 6, Gtin = 7896004400914, Quantidade = 8, Parcial = 33.60m },
    new() { Id = 7, Gtin = 7891700203142, Quantidade = 8, Parcial = 19.12m },
    new() { Id = 8, Gtin = 7891048038017, Quantidade = 1, Parcial = 4.40m },
    new() { Id = 9, Gtin = 7793306013029, Quantidade = 3, Parcial = 38.37m },
    new() { Id = 10, Gtin = 7896066334509, Quantidade = 2, Parcial = 10.38m },
};

var descontos = new List<Desconto>();

Console.WriteLine("--- Desconto no Atacado ---\n");

foreach (var grupo in vendas.GroupBy(v => v.Gtin))
{
    var produto = produtos.Single(p => p.Gtin == grupo.Key);

    var totalQtd = grupo.Sum(v => v.Quantidade);

    if (produto.PrecoAtacado.HasValue && produto.MinUnidadesAtacado.HasValue && totalQtd >= produto.MinUnidadesAtacado.Value)
    {
        var desconto = (produto.PrecoVarejo - produto.PrecoAtacado.Value) * totalQtd;
        descontos.Add(new Desconto { Gtin = produto.Gtin, Valor = desconto });
    }
}

Console.WriteLine("Descontos:");
foreach (var d in descontos)
    Console.WriteLine($"{d.Gtin,13}   {d.Valor,12:C2}");

var subtotal = vendas.Sum(v => v.Parcial);
var totalDesconto = descontos.Sum(d => d.Valor);
var totalFinal = subtotal - totalDesconto;

Console.WriteLine($"\n(+) Subtotal  = {subtotal,12:C2}");
Console.WriteLine($"(-) Desconto  = {totalDesconto,12:C2}");
Console.WriteLine($"(=) Total     = {totalFinal,12:C2}");

class Produto
{
    public long Gtin { get; set; }
    public string Descricao { get; set; } = null!;
    public decimal PrecoVarejo { get; set; }
    public decimal? PrecoAtacado { get; set; }
    public int? MinUnidadesAtacado { get; set; }
}

class ItemVenda
{
    public int Id { get; set; }
    public long Gtin { get; set; }
    public int Quantidade { get; set; }
    public decimal Parcial { get; set; }
}

class Desconto
{
    public long Gtin { get; set; }
    public decimal Valor { get; set; }
}