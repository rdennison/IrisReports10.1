﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace Iris10ReportUI.Helpers
{
    public class APIKeyHelper
    {
        public string GenerateKey()
        {
            int length = 64; //if this is too long use 32
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int) (num % (uint) valid.Length)]);
                }
            }

            return res.ToString();
        }

    }
}