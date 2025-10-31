namespace BusinessLayer.DTO;

public class EquipmentListItemDto
{
    public int EquipmentID { get; set; }
    public string EquipmentName { get; set; } = string.Empty;
    public string SupplierName { get; set; } = string.Empty;
    public string? LocationInHospital { get; set; }
    public DateOnly WarrantyExpiration { get; set; }
    public DateTime? DateAdded { get; set; }
}

