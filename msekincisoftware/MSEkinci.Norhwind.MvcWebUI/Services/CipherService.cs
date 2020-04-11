using Microsoft.Extensions.DependencyInjection;
using MSEkinci.Northwind.Business.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSEkinci.Northwind.MvcWebUI.Services
{
    public class CipherService : ICipherService
    {
        public string Decrypt(string cipherText)
        {
            var SCollection = new ServiceCollection();
            //add protection services
            SCollection.AddDataProtection();
            var LockerKey = SCollection.BuildServiceProvider();
            var locker = ActivatorUtilities.CreateInstance<Security>(LockerKey);
            string dencryptKey = locker.Decrypt(cipherText);
            return dencryptKey;
        }

        public string Encrypt(string cipherText)
        {
            var SCollection = new ServiceCollection();

            //add protection services
            SCollection.AddDataProtection();
            var LockerKey = SCollection.BuildServiceProvider();

            var locker = ActivatorUtilities.CreateInstance<Security>(LockerKey);
            string encryptKey = locker.Encrypt(cipherText);
            return encryptKey;
        }

    }
}
