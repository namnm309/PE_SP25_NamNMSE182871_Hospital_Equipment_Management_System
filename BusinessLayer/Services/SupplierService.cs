using BusinessLayer.DTO;
using DataAccessLayer.Repository;

namespace BusinessLayer.Services;

public class SupplierService
{
    private readonly SupplierRepository _supplierRepo;

    public SupplierService(SupplierRepository supplierRepo)
    {
        _supplierRepo = supplierRepo;
    }

    public async Task<List<SupplierDto>> GetAllAsync()
    {
        var suppliers = await _supplierRepo.GetAllAsync();
        return suppliers.Select(s => new SupplierDto
        {
            SupplierID = s.SupplierId,
            SupplierName = s.SupplierName
        }).ToList();
    }
}

