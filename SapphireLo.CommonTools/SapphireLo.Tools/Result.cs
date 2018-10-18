using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapphireLo.Tools
{
    public class Result<T>
    {
        public Result()
        {
            IsSuccess = true;
            Value = default(T);
            Code = 0;
        }

        public Result(bool isSuccess, string msg, int code)
        {
            IsSuccess = isSuccess;
            Msg = msg;
            Code = code;
        }

        public Result(bool isSuccess, string msg, T value = default(T), int code = -1)
        {
            IsSuccess = isSuccess;
            Msg = msg;
            Code = code;
            Value = value;
        }

        public Result(T value, bool isSuccess = false, string msg = "", int code = -1)
        {
            Value = value;
            IsSuccess = isSuccess;
            Msg = msg;
            Code = code;
        }
        public int Code { get; set; }
        public string Msg { get; set; }
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
    }
}
