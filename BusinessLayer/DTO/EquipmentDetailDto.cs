namespace BusinessLayer.DTO;

public class EquipmentDetailDto
{
    public int EquipmentID { get; set; }
    public string EquipmentName { get; set; } = string.Empty;
    public string TechnicalSpecifications { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string ModelNumber { get; set; } = string.Empty;
    public int SupplierID { get; set; }
    public DateOnly PurchaseDate { get; set; }
    public DateOnly WarrantyExpiration { get; set; }
    public string? MaintenanceInstructions { get; set; }
    public string? LocationInHospital { get; set; }
}

