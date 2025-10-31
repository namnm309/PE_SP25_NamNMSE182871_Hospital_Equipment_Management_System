using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository;

public class AccountRepository
{
    private readonly Sp25HospitalEquipmentDbContext _db;

    public AccountRepository(Sp25HospitalEquipmentDbContext db)
    {
        _db = db;
    }

    public async Task<StoreAccount?> LoginAsync(string email, string password)
    {
        var user = await _db.StoreAccounts.AsNoTracking()
           .FirstOrDefaultAsync(u => u.EmailAddress == email && u.StoreAccountPassword == password);
        
        return user;
    }
}
