using System;

namespace LegacyApp;

public class UserValidation
{
    internal bool Validation(string firstName, string lastName, string email, DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
        
        return (age >= 21 && (email.Contains("@") || email.Contains(".")) &&
                (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName)));
    }
}
