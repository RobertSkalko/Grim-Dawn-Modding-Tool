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

        private Dictionary<string, TQObject> dict = new Dictionary<string, TQObject>();

        public void TryGiveAffixesToLoottable(TQObject loottable, string gearType)
        {
            if (dict.ContainsKey(gearType)) {
                foreach (KeyValuePair<string, string> entry in dict[gearType].Dict) {
                    loottable.Dict[entry.Key] = entry.Value;
                }
            }
            else {
                Debug.Log("no key: " + gearType);
            }
        }
    }
}