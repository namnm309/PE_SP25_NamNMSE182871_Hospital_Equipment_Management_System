using BusinessLayer.DTO;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;

namespace BusinessLayer.Services;

public class EquipmentService
{
    private readonly EquipmentRepository _equipmentRepo;

    public EquipmentService(EquipmentRepository equipmentRepo)
    {
        _equipmentRepo = equipmentRepo;
    }

    public async Task<PagedResult<EquipmentListItemDto>> GetPagedListAsync(string? technicalSpec, DateOnly? warrantyExp, string? maintenanceInst, int page, int pageSize = 3)
    {
        var (items, total) = await _equipmentRepo.GetPagedListAsync(technicalSpec, warrantyExp, maintenanceInst, page, pageSize);

        var dtos = items.Select(e => new EquipmentListItemDto
        {
            EquipmentID = e.EquipmentId,
            EquipmentName = e.EquipmentName,
            SupplierName = e.Supplier.SupplierName,
            LocationInHospital = e.LocationInHospital,
            WarrantyExpiration = e.WarrantyExpiration,
            DateAdded = e.DateAdded
        }).ToList();

        return new PagedResult<EquipmentListItemDto>
        {
            Items = dtos,
            TotalCount = total,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<EquipmentDetailDto?> GetByIdAsync(int id)
    {
        var equipment = await _equipmentRepo.GetByIdAsync(id);
        if (equipment == null) return null;

        return new EquipmentDetailDto
        {
            EquipmentID = equipment.EquipmentId,
            EquipmentName = equipment.EquipmentName,
            TechnicalSpecifications = equipment.TechnicalSpecifications,
            Category = equipment.Category,
            ModelNumber = equipment.ModelNumber,
            SupplierID = equipment.SupplierId,
            PurchaseDate = equipment.PurchaseDate,
            WarrantyExpiration = equipment.WarrantyExpiration,
            MaintenanceInstructions = equipment.MaintenanceInstructions,
            LocationInHospital = equipment.LocationInHospital
        };
    }

    public async Task<int> CreateAsync(EquipmentUpsertDto dto)
    {
        var equipment = new EquipmentInformation
        {
            EquipmentName = dto.EquipmentName,
            TechnicalSpecifications = dto.TechnicalSpecifications,
            Category = dto.Category,
            ModelNumber = dto.ModelNumber,
            SupplierId = dto.SupplierID,
            PurchaseDate = dto.PurchaseDate,
            WarrantyExpiration = dto.WarrantyExpiration,
            MaintenanceInstructions = dto.MaintenanceInstructions,
            LocationInHospital = dto.LocationInHospital
        };

        var created = await _equipmentRepo.CreateAsync(equipment);
        return created.EquipmentId;
    }

    public async Task<bool> UpdateAsync(int id, EquipmentUpsertDto dto)
    {
        var equipment = await _equipmentRepo.GetByIdAsync(id);
        if (equipment == null) return false;

        equipment.EquipmentName = dto.EquipmentName;
        equipment.TechnicalSpecifications = dto.TechnicalSpecifications;
        equipment.Category = dto.Category;
        equipment.ModelNumber = dto.ModelNumber;
        equipment.SupplierId = dto.SupplierID;
        equipment.PurchaseDate = dto.PurchaseDate;
        equipment.WarrantyExpiration = dto.WarrantyExpiration;
        equipment.MaintenanceInstructions = dto.MaintenanceInstructions;
        equipment.LocationInHospital = dto.LocationInHospital;

        await _equipmentRepo.UpdateAsync(equipment);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _equipmentRepo.DeleteAsync(id);
    }
}

