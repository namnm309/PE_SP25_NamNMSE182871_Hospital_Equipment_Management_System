using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string? ContactPerson { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string? WebsiteUrl { get; set; }

    public bool? IsPreferred { get; set; }

    public virtual ICollection<EquipmentInformation> EquipmentInformations { get; set; } = new List<EquipmentInformation>();
}
