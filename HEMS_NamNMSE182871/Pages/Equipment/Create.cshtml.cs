using BusinessLayer.DTO;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HEMS_NamNMSE182871.Pages.Equipment;

[Authorize(Policy = "ManagerOnly")]
public class CreateModel : PageModel
{
    private readonly EquipmentService _equipmentService;
    private readonly SupplierService _supplierService;

    public CreateModel(EquipmentService equipmentService, SupplierService supplierService)
    {
        _equipmentService = equipmentService;
        _supplierService = supplierService;
    }

    [BindProperty]
    public EquipmentUpsertDto Equipment { get; set; } = new();

    public SelectList Suppliers { get; set; } = null!;

    public async Task OnGetAsync()
    {
        var suppliers = await _supplierService.GetAllAsync();
        Suppliers = new SelectList(suppliers, "SupplierID", "SupplierName");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var suppliers = await _supplierService.GetAllAsync();
            Suppliers = new SelectList(suppliers, "SupplierID", "SupplierName");
            return Page();
        }

        await _equipmentService.CreateAsync(Equipment);
        return RedirectToPage("./Index");
    }
}

