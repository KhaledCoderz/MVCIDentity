namespace MVCIDentity.Core.Extensions
{
    public static class ListExtension
    {
        public static bool IsNotNullOrEmpty<T>(this ICollection<T> lst)
        {
            if (lst.Any())
            {
                var obj = lst.First();

                return obj.IsNotNullOrEmpty();
            }
            return false;
        }
    }
}
