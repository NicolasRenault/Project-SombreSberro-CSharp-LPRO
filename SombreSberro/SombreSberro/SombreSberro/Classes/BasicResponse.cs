using System;
using System.Collections.Generic;

namespace SombreSberro.View
{
    internal class BasicResponse<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public string Title { get; set; }
        public DateTime DateResponse { get; set; }
        public BasicResponse()
        {
            Items = new List<T>();
        }
    }
}
