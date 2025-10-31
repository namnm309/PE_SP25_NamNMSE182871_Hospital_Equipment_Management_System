using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTO;

public class EquipmentUpsertDto
{
    public int EquipmentID { get; set; }

    [Required(ErrorMessage = "Equipment Name is required")]
    public string EquipmentName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Technical Specifications is required")]
    [TechnicalSpecificationsValidation]
    public string TechnicalSpecifications { get; set; } = string.Empty;

    [Required(ErrorMessage = "Category is required")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "Model Number is required")]
    public string ModelNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Supplier is required")]
    public int SupplierID { get; set; }

    [Required(ErrorMessage = "Purchase Date is required")]
    public DateOnly PurchaseDate { get; set; }

    [Required(ErrorMessage = "Warranty Expiration is required")]
    public DateOnly WarrantyExpiration { get; set; }

    public string? MaintenanceInstructions { get; set; }

    [Required(ErrorMessage = "Location is required")]
    public string? LocationInHospital { get; set; }
}

