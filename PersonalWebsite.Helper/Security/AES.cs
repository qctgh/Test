using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PersonalWebsite.Helper.Security
{
    /// <summary>
    /// AES编码、解码类
    /// 使用固定的模式（AES/CBC/NoPadding）实现跨平台的加密解密一致性
    /// </summary>
    public static class AES
    {
        #region 加密
        /// <summary>
        /// 使用指定的key对输入文本进行加密
        /// </summary>
        /// <param name="input">输入文本</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string EncryptText(string input, string key)
        {
            if (string.IsNullOrEmpty(input)) { return ""; }
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = SecurityHelper.CreateKeyByte(key, KeyTransMode.Md5, 128);
            byte[] bytesEncrypted = AESEncryptBytes(bytesToBeEncrypted, passwordBytes);
            string result = Convert.ToBase64String(bytesEncrypted);
            return result;
        }
        private static byte[] AESEncryptBytes(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            using (var ms = new MemoryStream())
            {
                using (var AES = new RijndaelManaged())
                {
                    AES.Key = passwordBytes;
                    AES.IV = passwordBytes;
                    AES.Mode = CipherMode.CBC;
                    AES.Padding = PaddingMode.Zeros;
                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }
        #endregion

        #region 解密
        /// <summary>
        /// 使用指定的key对密文解密
        /// </summary>
        /// <param name="input">密文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string DecryptText(string input, string key)
        {
            byte[] toEncryptArray = Convert.FromBase64String(input);
            byte[] passwordBytes = SecurityHelper.CreateKeyByte(key, KeyTransMode.Md5, 128);
            return AESDecryptBytes(toEncryptArray, passwordBytes);
        }
        private static string AESDecryptBytes(byte[] bytes, byte[] passwordBytes)
        {
            string plaintext = null;
            using (var AES = new RijndaelManaged())
            {
                AES.Key = passwordBytes;
                AES.IV = passwordBytes;
                AES.Mode = CipherMode.CBC;
                AES.Padding = PaddingMode.Zeros;
                ICryptoTransform cTransform = AES.CreateDecryptor();

                byte[] result = cTransform.TransformFinalBlock(bytes, 0, bytes.Length);
                plaintext = UTF8Encoding.UTF8.GetString(result);
            }
            return plaintext;
        }

        #endregion
    }
}
