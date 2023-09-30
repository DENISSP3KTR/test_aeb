using System.ComponentModel;

namespace test_aeb.Models
{
    public static class DescriptionAttributes<T>
    {
        public static Dictionary<string, string> RetrieveAttributesReverse()
        {
            Dictionary<string, string> Attributes = new Dictionary<string, string>();
            foreach (var memberInfo in typeof(T).GetMembers())
            {
                DescriptionAttribute[] list = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>().ToArray();
                if (list.Length > 0)
                {
                    Attributes[memberInfo.Name] = list[0].Description;
                }
            }
            return Attributes;
        }
        public static Dictionary<string, string> RetrieveAttributes()
        {
            Dictionary<string, string> Attributes = new Dictionary<string, string>();
            foreach (var memberInfo in typeof(T).GetMembers())
            {
                DescriptionAttribute[] list = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>().ToArray();
                if (list.Length > 0)
                {
                    Attributes[list[0].Description] = memberInfo.Name;
                }
            }
            return Attributes;
        }
    }
}
