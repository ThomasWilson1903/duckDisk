using System;
using System.Collections.Generic;

namespace duckDisk.data.api
{
    internal class Paging<T>
    {
        public int TotalPages { get; set; }
        public int TotalElements { get; set; }
        public int Number { get; set; }
        public int Size { get; set; }
        public int NumberOfElements { get; set; }
        public Boolean First { get; set; }
        public Boolean Last { get; set; }
        public Boolean Empty { get; set; }
        public List<T> Content { get; set; } = new();
    }
}
