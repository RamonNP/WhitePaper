using System.Security.Cryptography; 
using UnityEngine;

public class RSAManagement
{
    static public void GenerateRsa(){
        try
        {
            //Create a new RSACryptoServiceProvider object.
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(512))
            {
                //Export the key information to an RSAParameters object.
                //Pass false to export the public key information or pass
                //true to export public and private key information.
                RSAParameters RSAParams = RSA.ExportParameters(false);
            }
        }
        catch (CryptographicException e)
        {
            //Catch this exception in case the encryption did
            //not succeed.
            Debug.Log(e.Message);
        }
 

    }
    static public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)  
    {  
        try  
        {  
            byte[] encryptedData;  
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider()){
                //Import the RSA Key information. This only needs
                //toinclude the public key information.  
                RSA.ImportParameters(RSAKey); 

                //Encrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later. 
                encryptedData = RSA.Encrypt(Data, DoOAEPPadding);  
            }   
            return encryptedData;  
        }  
        catch (CryptographicException e){  
            Debug.Log(e.Message);  
            return null;  
        }  
    } 

    static public byte[] Decryption(byte[]Data, RSAParameters RSAKey, bool DoOAEPPadding)  
    {  
        try  
        {  
            byte[] decryptedData;

            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider()){
                //Import the RSA Key information. This needs
                //to include the private key information.  
                RSA.ImportParameters(RSAKey);  

                //Decrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.
                decryptedData = RSA.Decrypt(Data, DoOAEPPadding);  
            }  
            return decryptedData;  
        }catch (CryptographicException e){  
            Debug.Log(e.ToString());  
            return null;  
        }          
    }
}