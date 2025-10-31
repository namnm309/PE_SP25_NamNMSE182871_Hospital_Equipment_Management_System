using BusinessLayer.DTO;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HEMS_NamNMSE182871.Pages.Equipment;

[Authorize(Policy = "ManagerOnly")]
public class EditModel : PageModel
{
    private readonly EquipmentService _equipmentService;
    private readonly SupplierService _supplierService;

    public EditModel(EquipmentService equipmentService, SupplierService supplierService)
    {
        _equipmentService = equipmentService;
        _supplierService = supplierService;
    }

    [BindProperty]
    public EquipmentUpsertDto Equipment { get; set; } = new();

    public SelectList Suppliers { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var equipment = await _equipmentService.GetByIdAsync(id);
        if (equipment == null)
        {
            return NotFound();
        }

        Equipment = new EquipmentUpsertDto
        {
            EquipmentID = equipment.EquipmentID,
            EquipmentName = equipment.EquipmentName,
            TechnicalSpecifications = equipment.TechnicalSpecifications,
            Category = equipment.Category,
            ModelNumber = equipment.ModelNumber,
            SupplierID = equipment.SupplierID,
            PurchaseDate = equipment.PurchaseDate,
            WarrantyExpiration = equipment.WarrantyExpiration,
            MaintenanceInstructions = equipment.MaintenanceInstructions,
            LocationInHospital = equipment.LocationInHospital
        };

        var suppliers = await _supplierService.GetAllAsync();
        Suppliers = new SelectList(suppliers, "SupplierID", "SupplierName");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var suppliers = await _supplierService.GetAllAsync();
            Suppliers = new SelectList(suppliers, "SupplierID", "SupplierName");
            return Page();
        }

        await _equipmentService.UpdateAsync(Equipment.EquipmentID, Equipment);
        return RedirectToPage("./Index");
    }
}

