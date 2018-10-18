using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SapphireLo.Tools
{
    public class DESEncrytionLibrary
    {
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 加密方法 
        /// </summary>
        /// <param name="pToEncrypt">要加密内容</param>
        /// <param name="sKey">密钥[长度为8]</param>
        /// <returns>加密后的内容</returns>
        public static string EncryptUTF8(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //把字符串放到byte数组中 
            byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);

            //建立加密对象的密钥和偏移量 
            //原文使用ASCIIEncoding.ASCII方法的GetBytes方法 
            //使得输入密码必须输入英文文本 
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            //Write  the  byte  array  into  the  crypto  stream 
            //(It  will  end  up  in  the  memory  stream) 
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //Get  the  data  back  from  the  memory  stream,  and  into  a  string 
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                //Format  as  hex 
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="pToDecrypt">要解密的字符串</param>
        /// <param name="sKey">密钥[长度为8]</param>
        /// <returns>解密的内容</returns>
        public static string DecryptUTF8(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //Put  the  input  string  into  the  byte  array 
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            //建立加密对象的密钥和偏移量，此值重要，不能修改 
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            //Flush  the  data  through  the  crypto  stream  into  the  memory  stream 
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //Get  the  decrypted  data  back  from  the  memory  stream 
            //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象 
            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }


        public static string DecryptUTF8JavaResource(string rsource, string rkey)
        {
            string source = Encoding.UTF8.GetString(Convert.FromBase64String(rsource));
            string key = Encoding.ASCII.GetString(Convert.FromBase64String(rkey));

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] sources = System.Text.Encoding.UTF8.GetBytes(source);

            //建立加密对象的密钥和偏移量，此值重要，不能修改 
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            //Flush  the  data  through  the  crypto  stream  into  the  memory  stream 
            cs.Write(sources, 0, sources.Length);
            cs.FlushFinalBlock();

            //Get  the  decrypted  data  back  from  the  memory  stream 
            //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象 
            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.UTF8.GetString(ms.ToArray());

        }
    }

}
