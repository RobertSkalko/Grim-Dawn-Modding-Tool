using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class LootAffixTable
    {
        private Dictionary<int, TQObject> affixesForLevelRanges = new Dictionary<int, TQObject>();

        public void AddAffixTables(TQObject loottable, string gearType)
        {
        }

        public static List<int> LEVEL_RANGES = new List<int>() { 10, 20, 35, 50, 60, 80, 100 };

        public static int GetlvlRange(int lvl)
        {
            foreach (int num in LEVEL_RANGES) {
                if (lvl <= num) {
                    return num;
                }
            }
            return 0;
        }
    }
}