namespace BusinessLayer.DTO;

public class AccountSessionDto
{
    public int StoreAccountID { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public int StoreAccountRole { get; set; }
}

