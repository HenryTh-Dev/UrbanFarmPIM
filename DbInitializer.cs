using UrbanFarm.Models;
using System;
using System.Linq;

public class DbInitializer
{
    private readonly FarmContext _context;

    public DbInitializer(FarmContext context)
    {
        _context = context;
    }

    public void Initialize()
    {
        _context.Database.EnsureCreated();

        if (_context.Clients.Any() || _context.Employees.Any() || _context.PlantingAreas.Any() || _context.Resources.Any())
        {
            return; // Evita duplicação se os dados já existirem
        }

        // Inserindo uma Fazenda
        var farm = new Farm
        {
            Name = "Fazenda Esperança",
            Location = "Zona Rural, Município de Ibiúna, SP"
        };
        _context.Farms.Add(farm);
        _context.SaveChanges();

        // Inserindo Clientes
        var clients = new[]
        {
            new Client { Name = "AgroFarm Ltda", CNPJ = "12.345.678/0001-90", Address = "Rua das Palmeiras, 123 - São Paulo, SP", Phone = "(11) 98765-4321" },
            new Client { Name = "GreenHouse SA", CNPJ = "23.456.789/0001-01", Address = "Av. Paulista, 456 - São Paulo, SP", Phone = "(11) 91234-5678" },
            new Client { Name = "Organicos Brasil", CNPJ = "34.567.890/0001-02", Address = "Rua Verde, 789 - Campinas, SP", Phone = "(19) 99876-5432" },
            new Client { Name = "Colheita Feliz", CNPJ = "45.678.901/0001-03", Address = "Estrada da Fazenda, 100 - Ibiúna, SP", Phone = "(15) 98765-4321" },
            new Client { Name = "Agro Terra", CNPJ = "56.789.012/0001-04", Address = "Rua das Flores, 321 - São Carlos, SP", Phone = "(16) 93456-7890" }
        };
        _context.Clients.AddRange(clients);

        // Inserindo Funcionários
        var employees = new[]
        {
            new Employee { Name = "João Silva", CPF = "123.456.789-10", Position = "Gerente de Produção", Salary = 4500.00m, HireDate = new DateTimeOffset(2022, 1, 15, 0, 0, 0, TimeSpan.Zero) },
            new Employee { Name = "Maria Oliveira", CPF = "234.567.890-20", Position = "Agrônoma", Salary = 3800.00m, HireDate = new DateTimeOffset(2021, 5, 10, 0, 0, 0, TimeSpan.Zero) },
            new Employee { Name = "Carlos Souza", CPF = "345.678.901-30", Position = "Técnico Agrícola", Salary = 3200.00m, HireDate = new DateTimeOffset(2023, 2, 5, 0, 0, 0, TimeSpan.Zero) },
            new Employee { Name = "Ana Lima", CPF = "456.789.012-40", Position = "Assistente Administrativo", Salary = 2500.00m, HireDate = new DateTimeOffset(2020, 11, 20, 0, 0, 0, TimeSpan.Zero) },
            new Employee { Name = "Pedro Ramos", CPF = "567.890.123-50", Position = "Motorista", Salary = 2800.00m, HireDate = new DateTimeOffset(2022, 7, 25, 0, 0, 0, TimeSpan.Zero) }
        };
        _context.Employees.AddRange(employees);

        // Inserindo Recursos específicos para cada cultura
        var resources = new[]
        {
            new Resource { Name = "Milho", Type = "Colheita", Description = "Grãos de milho", Quantity = 1000, Price = 3.50 },
            new Resource { Name = "Soja", Type = "Colheita", Description = "Grãos de soja", Quantity = 800, Price = 4.00 },
            new Resource { Name = "Trigo", Type = "Colheita", Description = "Grãos de trigo", Quantity = 700, Price = 2.80 },
            new Resource { Name = "Feijão", Type = "Colheita", Description = "Grãos de feijão", Quantity = 600, Price = 5.00 },
            new Resource { Name = "Arroz", Type = "Colheita", Description = "Grãos de arroz", Quantity = 1200, Price = 2.50 }
        };
        _context.Resources.AddRange(resources);
        _context.SaveChanges();

        // Inserindo Áreas de Plantio e associando com o recurso correspondente
        var plantingAreas = new[]
        {
            new PlantingArea { Name = "Área de Milho", Size = 150.5, FarmId = farm.FarmId },
            new PlantingArea { Name = "Área de Soja", Size = 200.0, FarmId = farm.FarmId },
            new PlantingArea { Name = "Área de Trigo", Size = 100.0, FarmId = farm.FarmId },
            new PlantingArea { Name = "Área de Feijão", Size = 180.0, FarmId = farm.FarmId },
            new PlantingArea { Name = "Área de Arroz", Size = 250.0, FarmId = farm.FarmId }
        };
        _context.PlantingAreas.AddRange(plantingAreas);
        _context.SaveChanges();

        // Inserindo Fornecedores
        var suppliers = new[]
        {
            new Supplier { Name = "Fornecedor Rural SA", CNPJ = "98.765.432/0001-09", Address = "Av. das Flores, 789 - Campinas, SP", Phone = "(19) 3344-5566" },
            new Supplier { Name = "AgroInsumos Ltda", CNPJ = "87.654.321/0001-08", Address = "Rua Verde, 500 - São José, SP", Phone = "(12) 3456-7890" },
            new Supplier { Name = "Cultivar Sementes", CNPJ = "76.543.210/0001-07", Address = "Estrada do Campo, 1500 - Ibitinga, SP", Phone = "(16) 9876-5432" },
            new Supplier { Name = "Campo Forte", CNPJ = "65.432.109/0001-06", Address = "Rua do Sol, 20 - Ribeirão Preto, SP", Phone = "(17) 9999-9999" },
            new Supplier { Name = "AgroPlantio", CNPJ = "54.321.098/0001-05", Address = "Av. das Colheitas, 250 - Limeira, SP", Phone = "(19) 9234-5678" }
        };
        _context.Suppliers.AddRange(suppliers);

        // Inserindo Vendas e Itens de Venda
        for (int i = 0; i < clients.Length; i++)
        {
            var sale = new Sale
            {
                SaleDate = DateTimeOffset.Now.AddDays(-10 * i),
                ClientId = clients[i].ClientId,
                TotalAmount = 1500.00m + (100 * i)
            };
            _context.Sales.Add(sale);
            _context.SaveChanges();

            var saleItem = new SaleItem
            {
                SaleId = sale.SaleId,
                ResourceId = resources[i].ResourceId,
                Quantity = 50 + (10 * i),
                UnitPrice = (decimal)resources[i].Price
            };
            _context.SaleItems.Add(saleItem);
        }

        // Inserindo Plantios para cada área de plantio com seu recurso correspondente
        for (int i = 0; i < plantingAreas.Length; i++)
        {
            var planting = new Planting
            {
                PlantingDate = DateTimeOffset.Now.AddMonths(-i),
                PlantingAreaId = plantingAreas[i].PlantingAreaId,
                ResourceId = resources[i].ResourceId
            };
            _context.Plantings.Add(planting);
        }

        _context.SaveChanges();
    }
}
