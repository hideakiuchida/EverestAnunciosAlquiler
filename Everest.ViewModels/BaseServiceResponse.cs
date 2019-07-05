using System;
using System.Collections.Generic;
using System.Text;

namespace Everest.ViewModels
{
    public class BaseServiceResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
