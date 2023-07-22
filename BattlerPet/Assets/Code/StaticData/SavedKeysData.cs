using System.Collections.Generic;

namespace Code.Data
{
    public static class SavedKeysData
    {
        public static readonly List<string> AllKeys = new() { PlayerProgressKey };
        
        public const string PlayerProgressKey = "Progress";
    }
}