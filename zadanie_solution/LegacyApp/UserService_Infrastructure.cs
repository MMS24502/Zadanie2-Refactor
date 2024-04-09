using System;

namespace LegacyApp;

public interface IClientRepository
{
    Client GetById(int idClient);
}

public interface ICreditLimitService
{
    int GetCreditLimit(string lastName, DateTime birthday );
}