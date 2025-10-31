using BusinessLayer.DTO;
using DataAccessLayer.Repository;

namespace BusinessLayer.Services;

public class AuthService
{
    private readonly AccountRepository _accountRepo;

    public AuthService(AccountRepository accountRepo)
    {
        _accountRepo = accountRepo;
    }

    public async Task<AccountSessionDto?> LoginAsync(string email, string password)
    {
        var account = await _accountRepo.LoginAsync(email, password);
        if (account == null) return null;

        return new AccountSessionDto
        {
            StoreAccountID = account.StoreAccountId,
            FullName = account.FullName,
            EmailAddress = account.EmailAddress,
            StoreAccountRole = account.StoreAccountRole
        };
    }
}

