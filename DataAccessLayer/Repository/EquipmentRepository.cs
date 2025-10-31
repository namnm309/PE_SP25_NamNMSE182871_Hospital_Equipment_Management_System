using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository;

public class EquipmentRepository
{
    private readonly Sp25HospitalEquipmentDbContext _db; 

    public EquipmentRepository(Sp25HospitalEquipmentDbContext db)
    {
        _db = db;
    }

    public async Task<(List<EquipmentInformation> Items, int Total)> GetPagedListAsync(string? technicalSpec, DateOnly? warrantyExp, string? maintenanceInst, int page, int pageSize)
    {
        var query = _db.EquipmentInformations.Include(e => e.Supplier).AsQueryable();

        // Search filters
        if (!string.IsNullOrWhiteSpace(technicalSpec))
        {
            query = query.Where(e => e.TechnicalSpecifications.Contains(technicalSpec));
        }

        if (warrantyExp.HasValue)
        {
            query = query.Where(e => e.WarrantyExpiration == warrantyExp.Value);
        }

        if (!string.IsNullOrWhiteSpace(maintenanceInst))
        {
            query = query.Where(e => e.MaintenanceInstructions != null && e.MaintenanceInstructions.Contains(maintenanceInst));
        }

        var total = await query.CountAsync();
        var items = await query
            .OrderByDescending(e => e.DateAdded)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return (items, total);
    }

    public async Task<EquipmentInformation?> GetByIdAsync(int id)
    {
        return await _db.EquipmentInformations
            .Include(e => e.Supplier)
            .FirstOrDefaultAsync(e => e.EquipmentId == id);
    }

    public async Task<EquipmentInformation> CreateAsync(EquipmentInformation equipment)
    {
        equipment.DateAdded = DateTime.UtcNow;
        _db.EquipmentInformations.Add(equipment);
        await _db.SaveChangesAsync();
        return equipment;
    }

    public async Task<EquipmentInformation> UpdateAsync(EquipmentInformation equipment)
    {
        _db.EquipmentInformations.Update(equipment);
        await _db.SaveChangesAsync();
        return equipment;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var equipment = await _db.EquipmentInformations.FindAsync(id);
        if (equipment == null) return false;

        _db.EquipmentInformations.Remove(equipment);
        await _db.SaveChangesAsync();
        return true;
    }
}
