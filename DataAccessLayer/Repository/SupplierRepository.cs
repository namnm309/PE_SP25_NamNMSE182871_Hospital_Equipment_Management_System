using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository;

public class SupplierRepository
{
    private readonly Sp25HospitalEquipmentDbContext _db;

    public SupplierRepository(Sp25HospitalEquipmentDbContext db)
    {
        _db = db;
    }

    public async Task<List<Supplier>> GetAllAsync()
    {
        return await _db.Suppliers
            .OrderBy(s => s.SupplierName)
            .ToListAsync();
    }
}

