using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class LootAffixContainer

    {
        public static LootAffixContainer empty = new LootAffixContainer();

        public TQObject prefixes = new TQObject();
        public TQObject suffixes = new TQObject();

        private List<string> alreadyAdded = new List<string>();

        private int suffixNum = 0;
        private int prefixNum = 0;

        public void Add(TQAffixTable table)
        {
            if (alreadyAdded.Contains(table.recordValue) == false) {
                if (table.IsPrefix()) {
                    table.number = prefixNum;
                    table.addTo(prefixes);
                    prefixNum++;
                }
                else {
                    table.number = suffixNum;
                    table.addTo(suffixes);
                    suffixNum++;
                }

                alreadyAdded.Add(table.recordValue);
            }
        }
    }

    public class LootAffixTable
    {
        private Dictionary<int, LootAffixContainer> affixesForLevelRanges = new Dictionary<int, LootAffixContainer>();

        public void AddAffixTables(TQAffixTable affixtable)
        {
            int lvlRange = GetlvlRange(affixtable.level);

            LootAffixContainer obj = null;

            if (affixesForLevelRanges.ContainsKey(lvlRange)) {
                obj = affixesForLevelRanges[lvlRange];
                obj.Add(affixtable);
            }
            else {
                obj = new LootAffixContainer();
            }

            affixesForLevelRanges[lvlRange] = obj;
        }

        public LootAffixContainer GetAffixesFor(TQObject item)
        {
            int lvlrange = GetlvlRange(item.GetItemLevel());

            if (affixesForLevelRanges.ContainsKey(lvlrange)) {
                return affixesForLevelRanges[lvlrange];
            }
            else {
                return LootAffixContainer.empty;
            }
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