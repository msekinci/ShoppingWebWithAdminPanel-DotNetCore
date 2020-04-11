using System;
using System.Collections.Generic;
using System.Text;

namespace MSEkinci.Northwind.Business.Abstract
{
    public interface ICipherService
    {
        string Encrypt(string cipherText);
        string Decrypt(string cipherText);
}
}
