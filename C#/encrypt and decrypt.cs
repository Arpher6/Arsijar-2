using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Yokisakyok
{
    class Program
    {
        static void Main()
        {
            string text;
            Console.WriteLine("write your message : "); 
            text = Console.ReadLine();

            var encrypt = encryptstring(text);
            var decrypt = decryptstring(encrypt);

            Console.WriteLine("Message = " + text);
            Console.WriteLine("Encryption = " + encrypt);
            Console.WriteLine("Decryption = " + decrypt);
            Console.ReadLine();
        }
        //security key for encryption
        private const string Key = "adadaohbcbabcauhhouwhqdibqibdnaucuabywehadbacbaihahwidbgibaicieihhdhiawbndjaicnjiuejhouoqhohwhquhdnquo2238u919y389189838t86t41719y1y9";
        // encryption method plain to unreadable
        public static string encryptstring (string PlainText)
        {
            //getting byte of input string
            byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(PlainText);

            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();
            
            // get byte from key and pass it to compute hash value

            byte[] keyarray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
            //clear memory after encrypt
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            //assign key to triple des
            objTripleDESCryptoService.Key = keyarray;

            //crypto service mode electronic code book
            objTripleDESCryptoService.Mode = CipherMode.ECB;

            //padding mode pkcs 7 store encrypted data and check any extra byte added
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCryptoTransform = objTripleDESCryptoService.CreateEncryptor();

            //transform byte array to result array
            byte[] resultarray = objCryptoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
            objTripleDESCryptoService.Clear();
            return Convert.ToBase64String(resultarray, 0, resultarray.Length);
        }

        //decryption method unreadable to readable
        public static string decryptstring (string CipherText)
        {
            byte[] toEncryptArray = Convert.FromBase64String(CipherText);
            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();

            //get byte from key and pass it to compute hash value
            byte[] keyarray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            //assign key to triple des
            objTripleDESCryptoService.Key = keyarray;

            //crypto service mode electronic code book
            objTripleDESCryptoService.Mode = CipherMode.ECB;

            //padding mode pkcs 7 store encrypted data and check any extra byte added
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCryptoTransform = objTripleDESCryptoService.CreateDecryptor();
            // transform byte array to result array
            byte[] resultarray = objCryptoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            objTripleDESCryptoService.Clear();
            return UTF8Encoding.UTF8.GetString(resultarray);
        }
    }
}
