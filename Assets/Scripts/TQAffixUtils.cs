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
        public static Dictionary<string, TQObject> makeAffixDict(ConcurrentBag<TQObject> list)
        {
            Dictionary<string, List<TQAffixTable>> dict = new Dictionary<string, List<TQAffixTable>>();

            foreach (TQObject obj in list) {
                if (obj.Dict.ContainsKey("lootName1")) {
                    string path = obj.Dict["lootName1"].GetPathOfRecord();
                    if (File.Exists(path)) {
                        TQObject item = new TQObject(path);

                        string key = item.Dict["Class"];

                        if (dict.ContainsKey(key) == false) {
                            dict[key] = new List<TQAffixTable>();
                        }

                        var types = new List<string>() { "suffix", "prefix" };

                        int fails = 0;

                        int y = 0;

                        foreach (string type in types) {
                            fails = 0;
                            y = 0;

                            while (fails < 2) {
                                var table = new TQAffixTable(y, type);
                                if (table.exists(obj)) {
                                    table.setData(obj);

                                    bool add = true;

                                    foreach (TQAffixTable checktable in dict[key]) {
                                        if (table.table == checktable.table) {
                                            add = false;
                                        }
                                    }
                                    if (add) {
                                        dict[key].Add(table);
                                    }
                                }
                                else {
                                    fails++;
                                }
                                y++;
                            }
                        }
                    }
                    else {
                        Debug.Log(path + "Doesnt exist");
                    }
                }
            }

            var finaldict = new Dictionary<string, TQObject>();

            foreach (KeyValuePair<string, List<TQAffixTable>> entry in dict) {
                var sorted = new Dictionary<string, List<TQAffixTable>>();

                var counts = new Dictionary<string, int>();

                foreach (TQAffixTable table in entry.Value) {
                    if (sorted.ContainsKey(table.typeName)) {
                        sorted[table.typeName].Add(table);
                    }
                    else {
                        sorted[table.typeName] = new List<TQAffixTable>();
                    }
                }
                foreach (KeyValuePair<string, List<TQAffixTable>> sort in sorted) {
                    foreach (TQAffixTable t in sort.Value) {
                        if (counts.ContainsKey(sort.Key)) {
                            counts[sort.Key]++;
                        }
                        else {
                            counts[sort.Key] = 1;
                        }
                        t.number = counts[sort.Key];
                        t.setNameInfo();

                        if (finaldict.ContainsKey(entry.Key)) {
                            t.addTo(finaldict[entry.Key]);
                        }
                        else {
                            finaldict[entry.Key] = new TQObject();
                        }
                    }
                }
            }

            string keys = "";
            new List<string>(dict.Keys).ForEach(x => keys += " " + x);
            Debug.Log(keys);

            return finaldict;
        }
    }
}