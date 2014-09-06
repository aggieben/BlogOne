
namespace BlogOne.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static bool HasValue(this object obj)
        {
            return obj != null;
        }
    }
}