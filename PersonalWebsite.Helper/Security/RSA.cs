using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PersonalWebsite.Helper.Security
{
    /// <summary>
    /// RSA编码、解码类
    /// 使用此类需要引用Portable.BouncyCastle包，实现跨平台的加解密一致性
    /// </summary>
    public static class RSA
    {
        #region 加密
        /// <summary>
        /// 使用公钥对输入内容进行加密
        /// </summary>
        /// <param name="input">输入文本</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static string EncryptText(string input, string publicKey)
        {
            if (string.IsNullOrEmpty(input)) { return ""; }
            var rsa = CreateRsaProviderFromPublicKey(publicKey);
            string encryptedData = Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(input), false));
            return encryptedData;
        }

        private static RSACryptoServiceProvider CreateRsaProviderFromPublicKey(string publicKeyString)
        {
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKeyString));
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSAParameters RSAKeyInfo = new RSAParameters();
            RSAKeyInfo.Modulus = publicKeyParam.Modulus.ToByteArrayUnsigned();
            RSAKeyInfo.Exponent = publicKeyParam.Exponent.ToByteArrayUnsigned();
            RSA.ImportParameters(RSAKeyInfo);
            return RSA;
        }
        #endregion

        #region 解密
        /// <summary>
        /// 使用私钥对密文进行解密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>

        public static string RsaDecrypt(string input, string privateKey)
        {
            var rsa = CreateRsaProviderFromPrivateKey(privateKey);
            string plainText = Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(input), false));
            return plainText;
        }

        private static RSACryptoServiceProvider CreateRsaProviderFromPrivateKey(string privateKey)
        {
            RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));
            RSAParameters parameters = new RSAParameters();
            parameters.Modulus = privateKeyParam.Modulus.ToByteArrayUnsigned();
            parameters.Exponent = privateKeyParam.PublicExponent.ToByteArrayUnsigned();
            parameters.P = privateKeyParam.P.ToByteArrayUnsigned();
            parameters.Q = privateKeyParam.Q.ToByteArrayUnsigned();
            parameters.DP = privateKeyParam.DP.ToByteArrayUnsigned();
            parameters.DQ = privateKeyParam.DQ.ToByteArrayUnsigned();
            parameters.InverseQ = privateKeyParam.QInv.ToByteArrayUnsigned();
            parameters.D = privateKeyParam.Exponent.ToByteArrayUnsigned();
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(parameters);
            return rsa;
        }
        #endregion
    }
}
