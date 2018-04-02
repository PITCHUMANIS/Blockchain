using System;

namespace PropertyOwnershipRegistration.Model
{
    public interface ITransaction
    {
        string RegistrationNumber { get; set; }
        IdentityProof IdentityProof { get; set; }
        string Surname { get; set; }
        string GivenName { get; set; }
        DateTime DateOfBirth { get; set; }
        decimal PurchaseAmount { get; set; }
        DateTime PurchaseDate { get; set; }
        PaymentType PaymentType { get; set; }

        string CalculateTransactionHash();
    }
}
