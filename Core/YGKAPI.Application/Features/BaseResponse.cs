using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGKAPI.Application.Features
{
    public class BaseResponse<T>
    {
        public string Error { get; set; }
        public bool Succeeded { get; set; } = true;
        public Int16 Code { get; set; } = 200;
        public T Data { get; set; }
    }
}