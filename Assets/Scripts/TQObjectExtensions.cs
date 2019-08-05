using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        public static bool isLegendary(this TQObject @this)
        {
            return @this.Dict.ContainsKey(ITEM_CLASSIFICATION) && @this.Dict[ITEM_CLASSIFICATION] == "Legendary";
        }

        public static bool isEpic(this TQObject @this)
        {
            return @this.Dict.ContainsKey(ITEM_CLASSIFICATION) && @this.Dict[ITEM_CLASSIFICATION] == "Epic";
        }

        public static bool isUniqueItemLootTable(this TQObject obj)
        {
            //List<string> mustbezero = new List<string>() { "suffixOnly", "prefixOnly", "rarePrefixOnly", "bothPrefixSuffix", "rareBothPrefixSuffix" };

            if (obj.Dict.ContainsKey("itemNames")) {
                string path = obj.Dict["itemNames"].getFirstRecord().GetPathOfRecord();

                if (File.Exists(path)) {
                    TQObject item = new TQObject(path);

                    if (item.isEpic() || item.isLegendary()) {
                        return true;
                    }
                }
                else {
                    Debug.Log(path + " doesn't exist");
                }
            }

            return false;
        }
    }
}