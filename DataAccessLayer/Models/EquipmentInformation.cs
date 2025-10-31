using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class EquipmentInformation
{
    public int EquipmentId { get; set; }

    public string EquipmentName { get; set; } = null!;

    public string TechnicalSpecifications { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string ModelNumber { get; set; } = null!;

    public int SupplierId { get; set; }

    public DateOnly PurchaseDate { get; set; }

    public DateOnly WarrantyExpiration { get; set; }

    public string? MaintenanceInstructions { get; set; }

    public string? LocationInHospital { get; set; }

    public DateTime? DateAdded { get; set; }

    public virtual Supplier Supplier { get; set; } = null!;
}
