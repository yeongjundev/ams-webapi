using System.Collections.Generic;

namespace WebAPI.Helpers
{
    public class OrderingOption
    {
        public string[] OrderBy { get; set; }

        public List<(string, bool)> GetOrderByInfos()
        {
            List<(string, bool)> orderByInfos = new List<(string, bool)>();

            if (OrderBy == null)
            {
                return orderByInfos;
            }

            foreach (var orderBy in OrderBy)
            {
                if (string.IsNullOrWhiteSpace(orderBy))
                {
                    continue;
                }

                if (string.Equals(orderBy.Substring(0, 1), "-"))
                {
                    orderByInfos.Add((orderBy.Substring(1), true));
                }
                else
                {
                    orderByInfos.Add((orderBy, false));
                }
            }

            return orderByInfos;
        }
    }
}