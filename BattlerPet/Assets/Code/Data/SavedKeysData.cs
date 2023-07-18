using System.Collections.Generic;

namespace Code.Code.Data
{
    public static class SavedKeysData
    {
        public static List<string> AllKeys = new() { PlayerProgressKey };
        
        public const string PlayerProgressKey = "Progress";
    }
}