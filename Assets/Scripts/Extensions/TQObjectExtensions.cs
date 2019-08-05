using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public static class TQOjbectExtensions
    {
        private static string ITEM_CLASSIFICATION = "itemClassification";

        public static TQObject getFirstObjectOfLootTable(this TQObject loottable)
        {
            if (loottable.Dict.ContainsKey("itemNames")) {
                string path = loottable.Dict["itemNames"].getFirstRecord().GetPathOfRecord();

                if (File.Exists(path)) {
                    return new TQObject(path);
                }
                else {
                    Debug.Log("file doesn't exist " + path);
                }
            }
            return null;
        }

        public static bool isLegendary(this TQObject @this)
        {
            return @this.Dict.ContainsKey(ITEM_CLASSIFICATION) && @this.Dict[ITEM_CLASSIFICATION] == "Legendary";
        }

        public static bool isEpic(this TQObject @this)
        {
            return @this.Dict.ContainsKey(ITEM_CLASSIFICATION) && @this.Dict[ITEM_CLASSIFICATION] == "Epic";
        }

        public static bool isUnique(this TQObject @this)
        {
            return @this.isEpic() || @this.isLegendary();
        }

        public static bool isUniqueItemLootTable(this TQObject obj)
        {
            //List<string> mustbezero = new List<string>() { "suffixOnly", "prefixOnly", "rarePrefixOnly", "bothPrefixSuffix", "rareBothPrefixSuffix" };

            if (obj.Dict.ContainsKey("itemNames")) {
                string path = obj.Dict["itemNames"].getFirstRecord().GetPathOfRecord();

                if (File.Exists(path)) {
                    TQObject item = new TQObject(path);

                    if (item.isUnique()) {
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