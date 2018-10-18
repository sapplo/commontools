//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapphireLo.Tools
{
    public class CommonTools
    {
        /// <summary>
        /// 序列化对象为JSON的字符串内容
        /// </summary>
        /// <param name="obj">要转换成JSON的对象</param>
        /// <returns></returns>
        //public static string ObjectToJson(object obj)
        //{
        //    if (obj == null)
        //    {
        //        return null;
        //    }
        //    return JsonConvert.SerializeObject(obj);
        //}

        /// <summary>
        /// 反序列化JSON的字符串内容为对象
        /// </summary>
        /// <typeparam name="T">要反序列的对象</typeparam>
        /// <param name="jsonString">反序的字符串内容</param>
        /// <returns></returns>
        //public static T JsonToObject<T>(string jsonString)
        //{
        //    if (jsonString.IsNullOrEmpty())
        //    {
        //        return default(T);
        //    }
        //    return JsonConvert.DeserializeObject<T>(jsonString);
        //}

        /// <summary>
        /// 64位解码
        /// </summary>
        public static string GetFromBase64String(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }

        /// <summary>
        /// 64位编码
        /// </summary>
        public static string GetToBase64String(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }
    }
}
