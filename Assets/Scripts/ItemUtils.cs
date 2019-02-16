using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public static class ItemUtils
    {
        private static string ITEM_CLASSIFICATION = "itemClassification";

        public static string getFirstRecord(this string @this)
        {
            if (@this.Contains(";")) {
                return @this.Split(';')[0];
            }
            else {
                return @this;
            }
        }

        public static bool isLegendary(this GrimObject @this)
        {
            return @this.Dict.ContainsKey(ITEM_CLASSIFICATION) && @this.Dict[ITEM_CLASSIFICATION] == "Legendary";
        }

        public static bool isEpic(this GrimObject @this)
        {
            return @this.Dict.ContainsKey(ITEM_CLASSIFICATION) && @this.Dict[ITEM_CLASSIFICATION] == "Epic";
        }
    }
}