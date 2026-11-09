using Microsoft.EntityFrameworkCore;
using Mepsan.Api.Data;
using Mepsan.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// In-Memory Database for fast local dev & demo, or Postgres if connected
builder.Services.AddDbContext<MepsanDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (!string.IsNullOrEmpty(connectionString) && builder.Environment.IsProduction())
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        options.UseInMemoryDatabase("MepsanDevDb");
    }
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MepsanDbContext>();
    db.Database.EnsureCreated();

    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new Product { Id = "prod-1", Sku = "MEP-ELC-001", Barcode = "8690001001", Name = "E-30 Elektronik Pompa Anakartı", Category = "Elektronik", UnitPrice = 14500m, MinStockThreshold = 4, CompatibleModels = "Smartline, Matrix, Ocean" },
            new Product { Id = "prod-2", Sku = "MEP-PLS-002", Barcode = "8690001002", Name = "Akıllı Sayaç Pulser (Çift Kanallı)", Category = "Otomasyon", UnitPrice = 3850m, MinStockThreshold = 8, CompatibleModels = "Tüm Modeller" },
            new Product { Id = "prod-3", Sku = "MEP-NOZ-003", Barcode = "8690001003", Name = "ZVA Otomatik Akaryakıt Tabancası (DN 19)", Category = "Mekanik", UnitPrice = 4200m, MinStockThreshold = 10, CompatibleModels = "EuroDizel, Benzin 95" },
            new Product { Id = "prod-4", Sku = "MEP-FLT-004", Barcode = "8690001004", Name = "25 Mikron Yakıt Emiş Filtresi", Category = "Filtre", UnitPrice = 650m, MinStockThreshold = 25, CompatibleModels = "Matrix Serisi" },
            new Product { Id = "prod-5", Sku = "MEP-HOS-005", Barcode = "8690001005", Name = "Elaflex 4.5m Antistatik Dolum Hortumu", Category = "Mekanik", UnitPrice = 2800m, MinStockThreshold = 6, CompatibleModels = "Standart Dağıtıcı" },
            new Product { Id = "prod-6", Sku = "MEP-DSP-006", Barcode = "8690001006", Name = "LED Fiyat ve Litre Ekran Modülü", Category = "Elektronik", UnitPrice = 5200m, MinStockThreshold = 5, CompatibleModels = "Ocean Plus" }
        );

        db.StockLocations.AddRange(
            new StockLocation { Id = "loc-main", Code = "DEP-01", Name = "Mepsan Merkez Ana Depo", Type = "CENTRAL_WAREHOUSE", City = "İstanbul" },
            new StockLocation { Id = "loc-v1", Code = "ARC-34ME101", Name = "34 ME 101 - Marmara Servis Aracı 1", Type = "SERVICE_VEHICLE", PlateNumber = "34 ME 101", City = "İstanbul", AssignedTechnicianId = "tech-1" },
            new StockLocation { Id = "loc-v2", Code = "ARC-06ME202", Name = "06 ME 202 - İç Anadolu Servis Aracı 2", Type = "SERVICE_VEHICLE", PlateNumber = "06 ME 202", City = "Ankara", AssignedTechnicianId = "tech-2" }
        );

        db.Technicians.AddRange(
            new Technician { Id = "tech-1", FullName = "Ahmet Yılmaz", Email = "ahmet.yilmaz@mepsan.com.tr", PhoneNumber = "0532 100 2030", Role = "FIELD_TECHNICIAN", AssignedVehicleId = "loc-v1", Region = "Marmara" },
            new Technician { Id = "tech-2", FullName = "Mehmet Kaya", Email = "mehmet.kaya@mepsan.com.tr", PhoneNumber = "0533 200 3040", Role = "FIELD_TECHNICIAN", AssignedVehicleId = "loc-v2", Region = "İç Anadolu" }
        );

        db.LocationInventories.AddRange(
            new LocationInventory { Id = "inv-1", LocationId = "loc-main", ProductId = "prod-1", Quantity = 45 },
            new LocationInventory { Id = "inv-2", LocationId = "loc-main", ProductId = "prod-2", Quantity = 80 },
            new LocationInventory { Id = "inv-3", LocationId = "loc-main", ProductId = "prod-3", Quantity = 120 },
            new LocationInventory { Id = "inv-4", LocationId = "loc-main", ProductId = "prod-4", Quantity = 350 },
            new LocationInventory { Id = "inv-5", LocationId = "loc-v1", ProductId = "prod-1", Quantity = 2 },
            new LocationInventory { Id = "inv-6", LocationId = "loc-v1", ProductId = "prod-2", Quantity = 4 },
            new LocationInventory { Id = "inv-7", LocationId = "loc-v1", ProductId = "prod-3", Quantity = 3 },
            new LocationInventory { Id = "inv-8", LocationId = "loc-v1", ProductId = "prod-4", Quantity = 12 }
        );

        db.WorkOrders.AddRange(
            new WorkOrder 
            { 
                Id = "wo-101", 
                ExternalCallCenterId = "CC-2026-9412", 
                StationName = "Opet Kadıköy Sahil İstasyonu", 
                City = "İstanbul", 
                DispenserModel = "Mepsan Matrix H-4", 
                FaultDescription = "3 numaralı pompada pulse sinyali kesiliyor, litre sayacı ilerlemiyor.", 
                Status = "ASSIGNED", 
                AssignedTechnicianId = "tech-1", 
                AssignedVehicleId = "loc-v1" 
            },
            new WorkOrder 
            { 
                Id = "wo-102", 
                ExternalCallCenterId = "CC-2026-9418", 
                StationName = "Shell Maslak İstasyonu", 
                City = "İstanbul", 
                DispenserModel = "Mepsan Ocean 8", 
                FaultDescription = "E-30 pompa ekranında E-14 hata kodu, iletişim kopukluğu.", 
                Status = "IN_PROGRESS", 
                AssignedTechnicianId = "tech-1", 
                AssignedVehicleId = "loc-v1" 
            }
        );

        db.SaveChanges();
    }
}

// REST Endpoints
app.MapGet("/api/health", () => Results.Ok(new { status = "healthy", service = "Mepsan.Api", version = "1.0.0", timestamp = DateTime.UtcNow }));

app.MapGet("/api/dashboard/stats", async (MepsanDbContext db) =>
{
    var totalProducts = await db.Products.CountAsync();
    var activeWorkOrders = await db.WorkOrders.CountAsync(w => w.Status != "COMPLETED" && w.Status != "CANCELLED");
    var totalVehicles = await db.StockLocations.CountAsync(l => l.Type == "SERVICE_VEHICLE");
    var centralWarehouseStock = await db.LocationInventories
        .Where(i => i.LocationId == "loc-main")
        .SumAsync(i => i.Quantity);

    return Results.Ok(new
    {
        totalProducts,
        activeWorkOrders,
        totalVehicles,
        centralWarehouseStock
    });
});

app.MapGet("/api/products", async (MepsanDbContext db) => await db.Products.ToListAsync());

app.MapGet("/api/products/barcode/{barcode}", async (string barcode, MepsanDbContext db) =>
{
    var prod = await db.Products.FirstOrDefaultAsync(p => p.Barcode == barcode || p.Sku == barcode);
    return prod != null ? Results.Ok(prod) : Results.NotFound(new { message = "Ürün bulunamadı" });
});

app.MapGet("/api/stocks/locations", async (MepsanDbContext db) => await db.StockLocations.ToListAsync());

app.MapGet("/api/stocks/location/{locationId}", async (string locationId, MepsanDbContext db) =>
{
    var items = await (from inv in db.LocationInventories
                       join prod in db.Products on inv.ProductId equals prod.Id
                       where inv.LocationId == locationId
                       select new
                       {
                           inv.Id,
                           inv.LocationId,
                           inv.ProductId,
                           inv.Quantity,
                           prod.Name,
                           prod.Sku,
                           prod.Barcode,
                           prod.Category,
                           prod.Unit
                       }).ToListAsync();

    return Results.Ok(items);
});

app.MapGet("/api/work-orders", async (MepsanDbContext db) => await db.WorkOrders.OrderByDescending(w => w.CreatedAt).ToListAsync());

app.MapGet("/api/work-orders/technician/{techId}", async (string techId, MepsanDbContext db) =>
{
    var orders = await db.WorkOrders
        .Where(w => w.AssignedTechnicianId == techId)
        .OrderByDescending(w => w.CreatedAt)
        .ToListAsync();
    return Results.Ok(orders);
});

app.MapPost("/api/stocks/transfer", async (StockMovement req, MepsanDbContext db) =>
{
    if (string.IsNullOrEmpty(req.SourceLocationId) || string.IsNullOrEmpty(req.TargetLocationId))
    {
        return Results.BadRequest(new { message = "Kaynak ve hedef lokasyon zorunludur." });
    }

    var sourceInv = await db.LocationInventories.FirstOrDefaultAsync(i => i.LocationId == req.SourceLocationId && i.ProductId == req.ProductId);
    if (sourceInv == null || sourceInv.Quantity < req.Quantity)
    {
        return Results.BadRequest(new { message = "Kaynak lokasyonda yeterli stok yok." });
    }

    sourceInv.Quantity -= req.Quantity;

    var targetInv = await db.LocationInventories.FirstOrDefaultAsync(i => i.LocationId == req.TargetLocationId && i.ProductId == req.ProductId);
    if (targetInv == null)
    {
        targetInv = new LocationInventory
        {
            LocationId = req.TargetLocationId,
            ProductId = req.ProductId,
            Quantity = req.Quantity
        };
        db.LocationInventories.Add(targetInv);
    }
    else
    {
        targetInv.Quantity += req.Quantity;
    }

    req.Id = Guid.NewGuid().ToString();
    req.Timestamp = DateTime.UtcNow;
    db.StockMovements.Add(req);

    await db.SaveChangesAsync();
    return Results.Ok(new { success = true, movementId = req.Id });
});

app.Run();
