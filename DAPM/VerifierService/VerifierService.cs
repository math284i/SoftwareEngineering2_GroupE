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
        return verifyAttribute == null || verifyAttribute.Validate(token);
    }
    public bool Verify(string role, string token)
    {
        return false;
    }
}