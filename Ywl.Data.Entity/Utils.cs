using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ywl.Data.Entity
{
    public class Utils
    {
        public static int? StrToInt(string expression, int? defValue)
        {
            if (string.IsNullOrEmpty(expression) || expression.Trim().Length >= 11 || !Regex.IsMatch(expression.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            if (Int32.TryParse(expression, out int rv))
                return rv;

            return null;
        }
        public static long? StrToLong(string expression, long? defValue)
        {
            if (string.IsNullOrEmpty(expression) || expression.Trim().Length >= 11 || !Regex.IsMatch(expression.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            if (long.TryParse(expression, out long rv))
                return rv;

            return null;
        }
        /// <summary>
        /// 字符串转布尔
        /// </summary>
        /// <param name="expression">需要转换的表达式</param>
        /// <param name="defValue">默认值</param>
        /// <returns></returns>
        public static bool? StrToBool(string expression, bool? defValue)
        {
            if (string.IsNullOrEmpty(expression) || string.IsNullOrWhiteSpace(expression))
                return defValue;
            if (expression.Trim() == "1") return true;
            if (expression.Trim() == "0") return false;
            if (expression.Trim().ToLower() == "true") return true;
            if (expression.Trim().ToLower() == "false") return false;

            if (bool.TryParse(expression, out bool rv))
                return rv;

            return null;
        }
        /// <summary>
        /// 对字符串SHA1加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>加密后的十六进制字符串</returns>
        public static string Sha1Encrypt(string source, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;

            // 第一种方式
            SHA1 algorithm = SHA1.Create();
            byte[] byteArray = encoding.GetBytes(source);
            byte[] data = algorithm.ComputeHash(byteArray);

            StringBuilder stringBuilder = new StringBuilder(256);
            foreach (byte item in data)
            {
                stringBuilder.AppendFormat("{0:x2}", item);
            }
            algorithm.Clear();
            return stringBuilder.ToString();

            //string sh1 = "";
            //for (int i = 0; i < data.Length; i++)
            //{
            //    sh1 += data[i].ToString("x2").ToUpperInvariant();
            //}
            //hashcode = sh1;
            //hashcode = hashcode.ToLower();

            //// 第二种方式
            //byte[] byteArray = encoding.GetBytes(source);
            //using (HashAlgorithm hashAlgorithm = new SHA1CryptoServiceProvider())
            //{
            //    byteArray = hashAlgorithm.ComputeHash(byteArray);
            //    StringBuilder stringBuilder = new StringBuilder(256);
            //    foreach (byte item in byteArray)
            //    {
            //        stringBuilder.AppendFormat("{0:x2}", item);
            //    }
            //    hashAlgorithm.Clear();
            //    return stringBuilder.ToString();
            //}

            //// 第三种方式
            //using (SHA1 sha1 = SHA1.Create())
            //{
            //    byte[] hash = sha1.ComputeHash(encoding.GetBytes(source));
            //    StringBuilder stringBuilder = new StringBuilder();
            //    for (int index = 0; index < hash.Length; ++index)
            //        stringBuilder.Append(hash[index].ToString("x2"));
            //    sha1.Clear();
            //    return stringBuilder.ToString();
            //}
        }
    }
}
