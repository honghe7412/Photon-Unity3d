using System.Collections.Generic;

namespace Common.Tools
{
    public class DicTool
    {
        public static T2 GetValue<T1, T2>(Dictionary<T1,T2> dict,T1 key)
        {
            bool isSuccess = dict.TryGetValue(key, out T2 value);

            if (isSuccess)
                return value;
            else
                return default(T2);
        }
    }
}
