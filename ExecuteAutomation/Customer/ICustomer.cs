namespace ExecuteAutomation.Customer
{
    public interface ICustomer
    {
        string UserName { get; set; }
        string Email { get; set; }
        string Title { get; set; }
        string Initial { get; set; }
        string Gender { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Password { get; set; }
        string Country { get; set; }
    }
}
