using System;

namespace LegacyApp
{
    public class UserService(IClientRepository clientRepository, ICreditLimitService clientService) : UserValidation
    {
        public UserService() : this(new ClientRepository(), new UserCreditService())
        {
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            //BL - walidacja użytkownika, BL - walidacja maila, BL - walidacja wieku
            if (!Validation(firstName, lastName, email, dateOfBirth))
            {
                return false;
            }

            //INF
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            
            //BL + INF
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                int creditLimit = clientService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            }
            else
            {
                user.HasCreditLimit = true;
                int creditLimit = clientService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }
            
            //BL - walidacja CreditLimit
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            //INF
            UserDataAccess.AddUser(user);
            
            return true;
        }
    }
}
