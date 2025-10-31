using BusinessLayer.DTO;
using BusinessLayer.Services;
using HEMS_NamNMSE182871.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace HEMS_NamNMSE182871.Pages.Equipment;

[Authorize(Policy = "ManagerOrStaff")]
[IgnoreAntiforgeryToken]
public class IndexModel : PageModel
{
    private readonly EquipmentService _equipmentService;
    private readonly IHubContext<EquipmentHub> _hubContext;

    public IndexModel(EquipmentService equipmentService, IHubContext<EquipmentHub> hubContext)
    {
        _equipmentService = equipmentService;
        _hubContext = hubContext;
    }

    public PagedResult<EquipmentListItemDto> EquipmentList { get; set; } = new();
    
    [BindProperty(SupportsGet = true)]
    public string? TechnicalSpec { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public DateOnly? WarrantyExp { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string? MaintenanceInst { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;

    public bool IsManager => User.HasClaim("Role", "2");

    public async Task OnGetAsync()
    {
        EquipmentList = await _equipmentService.GetPagedListAsync(
            TechnicalSpec, 
            WarrantyExp, 
            MaintenanceInst, 
            CurrentPage, 
            3);
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        if (!IsManager)
        {
            return new JsonResult(new { success = false, message = "You do not have permission to do this function!" });
        }

        var success = await _equipmentService.DeleteAsync(id);
        if (success)
        {
            // Broadcast to all clients via SignalR
            await _hubContext.Clients.All.SendAsync("EquipmentDeleted", id);
            return new JsonResult(new { success = true, message = "Equipment deleted successfully" });
        }

        return new JsonResult(new { success = false, message = "Failed to delete equipment" });
    }
}

