using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity.ResponseModel
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }

        public string? Message { get; set; }

        public bool Result { get; set; }     

        public string? StatusCode { get; set; }

    }
}
