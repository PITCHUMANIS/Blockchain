using System;
using System.Text;
using PropertyOwnershipRegistration.Cryptography;

namespace PropertyOwnershipRegistration.Model
{
    public class Transaction : ITransaction
    {
        public string RegistrationNumber { get; set; }
        public IdentityProof IdentityProof { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal PurchaseAmount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public PaymentType PaymentType { get; set; }

        public Transaction(string registrationNumber, IdentityProof identityProof, string surname, string givenName, 
            DateTime dateOfBirth, decimal purchaseAmount, DateTime purchaseDate, PaymentType paymentType)
        {
            RegistrationNumber = registrationNumber;
            IdentityProof = identityProof;
            Surname = surname;
            GivenName = givenName;
            DateOfBirth = dateOfBirth;
            PurchaseAmount = purchaseAmount;
            PurchaseDate = purchaseDate;
            PaymentType = paymentType;
        }

        public string CalculateTransactionHash()
        {
            var propertyRegistrationDetails = RegistrationNumber + IdentityProof + Surname + GivenName + 
                DateOfBirth + PurchaseAmount + PurchaseDate + PaymentType;
            var transactionHash =
                Convert.ToBase64String(HashData.ComputeHashSha256(Encoding.UTF8.GetBytes(propertyRegistrationDetails)));
            return transactionHash;
        }
    }
}
