namespace Mepsan.Api.Models;

public class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Sku { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Unit { get; set; } = "Adet";
    public decimal UnitPrice { get; set; }
    public int MinStockThreshold { get; set; } = 5;
    public string CompatibleModels { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public class StockLocation
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = "SERVICE_VEHICLE"; // CENTRAL_WAREHOUSE, SERVICE_VEHICLE, REPAIR_ZONE, SCRAP_ZONE
    public string? PlateNumber { get; set; }
    public string? AssignedTechnicianId { get; set; }
    public string City { get; set; } = "İstanbul";
    public bool IsActive { get; set; } = true;
}

public class LocationInventory
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string LocationId { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
}

public class StockMovement
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProductId { get; set; } = string.Empty;
    public string ProductSku { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string MovementType { get; set; } = string.Empty; // VEHICLE_TRANSFER, FIELD_CONSUMPTION, DEFECTIVE_RETURN, PURCHASE_RECEIPT
    public string? SourceLocationId { get; set; }
    public string? TargetLocationId { get; set; }
    public string? WorkOrderId { get; set; }
    public string PerformedByUserId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Notes { get; set; } = string.Empty;
}

public class WorkOrder
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ExternalCallCenterId { get; set; } = string.Empty;
    public string StationName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string DispenserModel { get; set; } = string.Empty;
    public string FaultDescription { get; set; } = string.Empty;
    public string Status { get; set; } = "OPEN"; // OPEN, ASSIGNED, IN_PROGRESS, COMPLETED, CANCELLED
    public string? AssignedTechnicianId { get; set; }
    public string? AssignedVehicleId { get; set; }
    public string TechnicianNotes { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
}

public class Technician
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Role { get; set; } = "FIELD_TECHNICIAN";
    public string? AssignedVehicleId { get; set; }
    public string Region { get; set; } = "Marmara";
    public bool IsActive { get; set; } = true;
}
