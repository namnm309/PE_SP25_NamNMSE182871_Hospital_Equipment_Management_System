using Microsoft.AspNetCore.SignalR;

namespace HEMS_NamNMSE182871.Hubs;

public class EquipmentHub : Hub
{
    public async Task NotifyEquipmentDeleted(int equipmentId)
    {
        await Clients.All.SendAsync("EquipmentDeleted", equipmentId);
    }
}

