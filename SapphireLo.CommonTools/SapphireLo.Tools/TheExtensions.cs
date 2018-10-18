using System;
using System.Collections.Generic;
using System.Linq;
//using Newtonsoft.Json;
using System.Text;

namespace SapphireLo.Tools
{
    public static class TheExtensions
    {
        public static int ToInt(this Enum source)
        {
            return ((IConvertible)source).ToInt32(null);
        }
        public static T ToEnum<T>(this int source)
        {
            return (T)Enum.ToObject(typeof(T), source);
        }


        /// <summary>
        /// <paramref name="source"/>为null或empty,返回true,否则返回false
        /// </summary>
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }
        /// <summary>
        /// <paramref name="source"/>为null或empty,返回false,否则返回true
        /// </summary>
        public static bool IsNotNullOrEmpty(this string source)
        {
            return !string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// 将<paramref name="source"/>转换为<c>Decimal</c>.转换失败返回0
        /// </summary>
        public static int ToInt(this string source) { return ToInt(source, 0); }
        /// <summary>
        /// 将<paramref name="source"/>转换为<c>int</c>.转换失败返回<paramref name="defalutVaue"/>
        /// </summary>
        public static int ToInt(this string source, int defalutVaue)
        {
            if (source.IsNullOrEmpty())
                return defalutVaue;
            int r;
            if (int.TryParse(source, out r))
                return r;
            return defalutVaue;
        }

        /// <summary>
        /// 将<paramref name="source"/>转换为<c>Decimal</c>.转换失败返回0
        /// </summary>
        public static decimal ToDecimal(this string source) { return ToDecimal(source, 0); }
        /// <summary>
        /// 将<paramref name="source"/>转换为<c>Decimal</c>.转换失败返回<paramref name="defalutVaue"/>
        /// </summary>
        public static decimal ToDecimal(this string source, decimal defalutVaue)
        {
            if (source.IsNullOrEmpty())
                return defalutVaue;
            decimal r;
            if (decimal.TryParse(source, out r))
                return r;
            return defalutVaue;
        }

        /// <summary>
        ///  调用 <c>String.Format(<paramref name="source"/>, <paramref name="objs"/>)</c>
        /// </summary>
        public static string FormatExt(this string source, params object[] objs)
        {
            return String.Format(source, objs);
        }

        /// <summary>
        /// <paramref name="source"/> 为null 或 empty 返回true. <paramref name="source"/>不包含 <paramref name="c"/>  返回true. 否则返回false
        /// </summary>
        public static bool NotContainsEx(this string source, char c)
        {
            if (String.IsNullOrEmpty(source))
                return true;
            return !source.Contains(c);
        }
        /// <summary>
        /// <paramref name="source"/> 为null 或 empty 返回true. <paramref name="source"/>不包含 <paramref name="str"/>  返回true. 否则返回false
        /// </summary>
        public static bool NotContainsEx(this string source, string str)
        {
            if (String.IsNullOrEmpty(source))
                return true;
            return !source.Contains(str);
        }
        /// <summary>
        /// <paramref name="source"/> 为null 或 empty 返回false. <paramref name="source"/>不包含 <paramref name="c"/>  返回false. 否则返回true
        /// </summary>
        public static bool ContainsEx(this string source, char c)
        {
            if (String.IsNullOrEmpty(source))
            {
                return false;
            }
            return source.Contains(c);
        }
        /// <summary>
        /// <paramref name="source"/> 为null 或 empty 返回false. <paramref name="source"/>不包含 <paramref name="str"/>  返回false. 否则返回true
        /// </summary>
        public static bool ContainsEx(this string source, string str)
        {
            if (String.IsNullOrEmpty(source))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            return source.Contains(str);
        }

        /// <summary>
        /// 使用此方法将忽略大小写
        /// </summary>
        /// <param name="source">为null 或 empty 返回false. </param>
        /// <param name="str">为null 或 empty 返回false. </param>
        /// <returns></returns>
        public static bool ContainsExIgnoreCase(this string source, string str)
        {
            if (String.IsNullOrEmpty(source) || string.IsNullOrEmpty(str))
                return false;
            return source.ToUpper().Contains(str.ToUpper());
        }
        ///// <summary>
        ///// 调用 bk.comm中Newtonsoft.Json组件方法,将对象序列化为json
        ///// </summary>
        ///// <param name="source"></param>
        ///// <returns></returns>
        //public static string ToJson(this object source)
        //{
        //    return JsonConvert.SerializeObject(source);
        //}
        ///// <summary>
        ///// 调用 bk.comm中Newtonsoft.Json组件方法,将json字符串序列化为指定对象
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static T JsonToObj<T>(this string str)
        //{
        //    return JsonConvert.DeserializeObject<T>(str);
        //}
        ///// <summary>
        ///// 调用 bk.comm中Newtonsoft.Json组件方法,将json字符串序列化为指定对象
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static T ToJson<T>(this string str)
        //{
        //    try
        //    {
        //        return JsonConvert.DeserializeObject<T>(str);
        //    }
        //    catch (Exception)
        //    {
        //        return default(T);
        //    }
        //}
        public static void Add(this List<KeyValuePair<string, string>> source, string key, string value)
        {
            source.Add(new KeyValuePair<string, string>(key, value));
        }

        public static string ConcatEx(this IEnumerable<string> source, string c)
        {

            var result = "";
            foreach (var item in source)
            {
                result += item + c;
            }
            result.TrimEnd(c.ToCharArray());
            return result;
        }

        public static string ReplaceEx(this string source, string oldValue, string newValue)
        {
            if (source.IsNullOrEmpty())
            {
                return "";
            }

            return source.Replace(oldValue, newValue);
        }

        public static string BuilderQueryCondition(this Queue<string> condition)
        {
            if (condition == null || condition.Count == 0)
            {
                return null;
            }

            if (condition.Count == 1)
            {
                return condition.Dequeue();
            }

            StringBuilder query = new StringBuilder();
            foreach (string s in condition)
            {
                query.Append(" AND ");
                query.Append(s);
            }
            query.Remove(0, 5);
            return query.ToString();
        }

        public static string ToString(this DBNull bNull, string resultStr)
        {
            if (DBNull.Value == null)
            {
                return resultStr;
            }

            return "";
        }

        public static string ToString(this object obj, string result)
        {
            if (obj == null)
            {
                return result;
            }
            if (obj.ToString() == "")
            {
                return result;
            }
            return obj.ToString();
        }
    }
}
