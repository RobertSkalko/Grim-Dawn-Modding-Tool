using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using GrimDawnModdingTool;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class TQAffixUtils : MonoBehaviour
    {
        public static Dictionary<string, LootAffixTable> makeAffixDict(ConcurrentBag<TQObject> loottables)
        {
            Dictionary<string, HashSet<TQAffixTable>> dict = new Dictionary<string, HashSet<TQAffixTable>>(); // here all the affixes stored

            foreach (TQObject loottable in loottables) {
                TQObject item = loottable.getFirstObjectOfLootTable();

                if (item != null) {
                    string key = item.GetClass();

                    if (dict.ContainsKey(key) == false) {
                        dict[key] = new HashSet<TQAffixTable>();
                    }

                    var suffixAndPrefix = new List<string>() { "suffix", "prefix" };

                    int fails = 0;

                    int y = 0;

                    foreach (string affixType in suffixAndPrefix) {
                        fails = 0;
                        y = 0;

                        while (fails < 2) {
                            var table = new TQAffixTable(y, affixType, item.GetItemLevel());
                            if (table.exists(loottable)) {
                                table.setData(loottable);

                                dict[key].Add(table);
                            }
                            else {
                                fails++;
                            }
                            y++;
                        }
                    }
                }
            }

            var finaldict = new Dictionary<string, LootAffixTable>();

            foreach (KeyValuePair<string, HashSet<TQAffixTable>> entry in dict) {
                var sorted = new Dictionary<string, List<TQAffixTable>>();

                foreach (TQAffixTable table in entry.Value) {
                    if (sorted.ContainsKey(table.typeName)) {
                        sorted[table.typeName].Add(table);
                    }
                    else {
                        sorted[table.typeName] = new List<TQAffixTable>();
                    }
                }
                var counts = new Dictionary<string, int>();

                foreach (KeyValuePair<string, List<TQAffixTable>> sort in sorted) {
                    foreach (TQAffixTable t in sort.Value) {
                        if (counts.ContainsKey(sort.Key)) {
                            counts[sort.Key]++;
                        }
                        else {
                            counts[sort.Key] = 1;
                        }
                        t.number = counts[sort.Key];

                        if (finaldict.ContainsKey(entry.Key)) {
                            finaldict[entry.Key].AddAffixTables(t);
                        }
                        else {
                            finaldict[entry.Key] = new LootAffixTable();
                        }
                    }
                }
            }

            // im kinda lost on what the old code up does..

            string keys = "";
            new List<string>(dict.Keys).ForEach(x => keys += " " + x);
            Debug.Log(keys);

            return finaldict;
        }
    }
}