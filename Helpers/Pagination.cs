﻿namespace ECommerce_App.Helpers
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int pageSize, int count, List<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; } 

        public List<T> Data { get; set; }
    }
}
