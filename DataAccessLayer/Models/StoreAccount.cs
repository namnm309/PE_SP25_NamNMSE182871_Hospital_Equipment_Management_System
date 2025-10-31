using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class StoreAccount
{
    public int StoreAccountId { get; set; }

    public string FullName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string StoreAccountPassword { get; set; } = null!;

    public int StoreAccountRole { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Department { get; set; }

    public DateTime? DateCreated { get; set; }
}
