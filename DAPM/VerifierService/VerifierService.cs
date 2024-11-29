using System.Reflection;

namespace VerifierService;

public class VerifierService
{
    public static bool VerifyMethod(object controller, string methodName, string token)
    {
        var method = controller.GetType().GetMethod(methodName);
        if (method == null)
        {
            throw new ArgumentException($"Method '{methodName}' not found in type '{controller.GetType().Name}'");
        }
        
        var verifyAttribute = method.GetCustomAttribute<VerifyAttribute>();
        return verifyAttribute == null || verifyAttribute.Validate(token); // Null here, so if there isn't a verify,
                                                                           // then everyone have access.
    }
    public bool Verify(Roles role, string token)
    {
        var result = token switch
        {
            "user" => Roles.NormalUser,
            "admin" => Roles.Admin,
            _ => Roles.Undefined
        };

        return result >= role;
    }
}