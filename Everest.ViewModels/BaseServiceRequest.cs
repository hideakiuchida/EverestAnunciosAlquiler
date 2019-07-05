using System;
using System.Collections.Generic;
using System.Text;

namespace Everest.ViewModels
{
    public class BaseServiceRequest<T>
    {
        public int IdUsuario { get; set; }
        public T Data { get; set; }
    }
}
