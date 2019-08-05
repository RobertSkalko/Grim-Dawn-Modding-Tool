using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class LootAffixes
    {
        public LootAffixes(ConcurrentBag<TQObject> allLootTables)
        {
            this.dict = TQAffixUtils.makeAffixDict(allLootTables);
        }

        private Dictionary<string, LootAffixTable> dict = new Dictionary<string, LootAffixTable>();

        public void TryGiveAffixesToLoottable(TQObject loottable, string gearType)
        {
            if (dict.ContainsKey(gearType)) {
                LootAffixContainer affixes = dict[gearType].GetAffixesFor(loottable.getFirstObjectOfLootTable());

                loottable.ReplaceWithAllValuesOf(affixes.prefixes);
                loottable.ReplaceWithAllValuesOf(affixes.suffixes);
            }
            else {
                Debug.Log("no key: " + gearType);
            }
        }
    }
}