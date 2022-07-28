using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiLoGame.Shared.DTO.Http
{
    public class ResponseModel<T> 
    {
        public int StatusCode { get; set; } = 200;
        public T Data { get; set; }
        public IReadOnlyCollection<string> Errors { get; set; }

        public void SetData(T data) =>
            Data = data;

    }
}
