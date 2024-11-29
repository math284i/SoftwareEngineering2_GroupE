namespace VerifierService;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class VerifyAttribute : Attribute
{
    public Roles Role { get; }

    public VerifyAttribute(Roles role)
    {
        Role = role;
    }
    
    public bool Validate(string token)
    {
        var verifierService = new VerifierService();
        return verifierService.Verify(Role, token);
    }
}