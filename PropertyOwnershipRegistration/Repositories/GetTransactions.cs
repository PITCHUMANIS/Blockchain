using System;
using PropertyOwnershipRegistration.Model;

namespace PropertyOwnershipRegistration.Repositories
{
    public class GetTransactions
    {
        public static ITransaction GetTransactionRecords(TransactionPool transactionPool)
        {
            ITransaction txn1 = new Transaction("COF123", IdentityProof.DrivingLicence, "surname1", "givenname1", DateTime.Now, 320000.00m, DateTime.Now, PaymentType.Cash);
            ITransaction txn2 = new Transaction("COF456", IdentityProof.Passport, "surname2", "givenname2", DateTime.Now, 1200000.00m, DateTime.Now, PaymentType.Cheque);
            ITransaction txn3 = new Transaction("COF789", IdentityProof.BirthCertificate, "surname3", "givenname3", DateTime.Now, 340000.00m, DateTime.Now, PaymentType.Card);
            ITransaction txn4 = new Transaction("COF101", IdentityProof.DrivingLicence, "surname4", "givenname4", DateTime.Now, 400000.00m, DateTime.Now, PaymentType.Card);
            ITransaction txn5 = new Transaction("COF112", IdentityProof.DrivingLicence, "surname5", "givenname5", DateTime.Now, 500000.00m, DateTime.Now, PaymentType.Cash);
            ITransaction txn6 = new Transaction("COF131", IdentityProof.Passport, "surname6", "givenname6", DateTime.Now, 6000000, DateTime.Now, PaymentType.Cheque);
            ITransaction txn7 = new Transaction("COF415", IdentityProof.BirthCertificate, "surname7", "givenname7", DateTime.Now, 700000.00m, DateTime.Now, PaymentType.Cheque);
            ITransaction txn8 = new Transaction("COF161", IdentityProof.BirthCertificate, "surname8", "givenname8", DateTime.Now, 800000.00m, DateTime.Now, PaymentType.Card);
            ITransaction txn9 = new Transaction("COF718", IdentityProof.DrivingLicence, "surname9", "givenname9", DateTime.Now, 1400000.00m, DateTime.Now, PaymentType.Cash);
            ITransaction txn10 = new Transaction("COF192", IdentityProof.BirthCertificate, "surname10", "givenname10", DateTime.Now, 520000.00m, DateTime.Now,PaymentType.Cash);
            ITransaction txn11 = new Transaction("COF021", IdentityProof.Passport, "surname11", "givenname11", DateTime.Now, 807300.00m, DateTime.Now, PaymentType.Cash);
            ITransaction txn12 = new Transaction("COF222", IdentityProof.Passport, "surname12", "givenname12", DateTime.Now, 902300.00m, DateTime.Now, PaymentType.Card);
            ITransaction txn13 = new Transaction("COF324", IdentityProof.DrivingLicence, "surname13", "givenname13", DateTime.Now, 125000.00m, DateTime.Now, PaymentType.Cash);
            ITransaction txn14 = new Transaction("COF252", IdentityProof.Passport, "surname14", "givenname14", DateTime.Now, 760000.00m, DateTime.Now, PaymentType.Cheque);
            ITransaction txn15 = new Transaction("COF627", IdentityProof.BirthCertificate, "surname15", "givenname15", DateTime.Now, 870000.00m, DateTime.Now, PaymentType.Cheque);
            ITransaction txn16 = new Transaction("COF282", IdentityProof.DrivingLicence, "surname16", "givenname16", DateTime.Now, 808000.00m, DateTime.Now, PaymentType.Card);
            ITransaction txn17 = new Transaction("COF930", IdentityProof.Passport, "surname17", "givenname17", DateTime.Now, 968000.00m, DateTime.Now, PaymentType.Cash);

            transactionPool.AddTransaction(txn1);
            transactionPool.AddTransaction(txn2);
            transactionPool.AddTransaction(txn3);
            transactionPool.AddTransaction(txn4);
            transactionPool.AddTransaction(txn5);
            transactionPool.AddTransaction(txn6);
            transactionPool.AddTransaction(txn7);
            transactionPool.AddTransaction(txn8);
            transactionPool.AddTransaction(txn9);
            transactionPool.AddTransaction(txn10);
            transactionPool.AddTransaction(txn11);
            transactionPool.AddTransaction(txn12);
            transactionPool.AddTransaction(txn13);
            transactionPool.AddTransaction(txn14);
            transactionPool.AddTransaction(txn15);
            transactionPool.AddTransaction(txn16);
            transactionPool.AddTransaction(txn17);

            return txn10;
        }
    }
}
