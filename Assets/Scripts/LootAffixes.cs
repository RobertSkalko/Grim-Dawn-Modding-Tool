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

        public void testPrint()
        {
            foreach (KeyValuePair<string,LootAffixTable> entry in dict)
            {
                Debug.Log("Gear type: " + entry.Key);

                entry.Value.testPrint();
            }
        }

        public void TryGiveAffixesToLoottable(TQObject loottable, string gearType)
        {
            if (dict.ContainsKey(gearType)) {
              dict[gearType].GetAffixesFor(loottable,loottable.getFirstObjectOfLootTable());
                               
            }
            else {
                Debug.Log("no key: " + gearType);
            }
        }
    }
}