using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class ToolTQAELegendaryAffixes : ToolButton
    {
        public override string Name { get => "ToolTQAELegendaryAffixes"; }
        public override string Description { get; }

        public Dictionary<string, GrimObject> makeAffixDict(List<GrimObject> list)
        {
            int i = 0;

            Dictionary<string, List<TQAffixTable>> dict = new Dictionary<string, List<TQAffixTable>>();

            foreach (GrimObject obj in list) {
                if (obj.Dict.ContainsKey("lootName1")) {
                    string path = Save.Instance.FilesToEditPath + "/" + obj.Dict["lootName1"];
                    if (File.Exists(path)) {
                        GrimObject item = new GrimObject(path);

                        string key = item.Dict["Class"];

                        if (dict.ContainsKey(key) == false) {
                            dict[key] = new List<TQAffixTable>();
                        }

                        var types = new List<string>() { "suffix", "prefix" };

                        for (int y = 0; y < 10; y++) {
                            foreach (string type in types) {
                                var table = new TQAffixTable(obj, y, type);
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
                            }
                        }
                    }
                    else {
                        Debug.Log(path + "Doesnt exist");
                    }
                }
            }

            var finaldict = new Dictionary<string, GrimObject>();

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
                            finaldict[entry.Key] = new GrimObject();
                        }
                    }
                }
            }

            return finaldict;
        }

        protected override void Action()
        {
            GrimObject chances = new GrimObject(Path.Combine(Save.Instance.DataPath, "chances.txt"));

            List<GrimObject> list = new List<GrimObject>(FileManager.GetObjectsFromAllFilesInPath(Path.Combine(Save.Instance.FilesToEditPath, "records"), true).Where(x => x.FilePath.Contains("loottables")));

            var uniqueitemlist = new List<GrimObject>();

            Dictionary<string, GrimObject> dict = makeAffixDict(list);

            foreach (GrimObject obj in list) {
                if (isUniqueItemLootTable(obj)) {
                    uniqueitemlist.Add(obj);
                }
            }
            Debug.Log("There are " + uniqueitemlist.Count + " unique items.");

            foreach (GrimObject obj in uniqueitemlist) {
                if (obj.Dict.ContainsKey("itemNames")) {
                    string path = Path.Combine(Save.Instance.FilesToEditPath, obj.Dict["itemNames"].getFirstRecord());

                    if (File.Exists(path)) {
                        GrimObject item = new GrimObject(path);

                        string key = item.Dict["Class"];

                        if (dict.ContainsKey(key)) {
                            Debug.Log("key exists: " + key);
                            addRandomAffixesToLootTable(obj, dict[key]);
                        }
                        else {
                            Debug.Log("no key: " + key);
                        }
                    }
                    else {
                        Debug.Log("file doesn't exist " + path);
                    }
                }
            }

            foreach (GrimObject obj in uniqueitemlist) {
                foreach (KeyValuePair<string, string> entry in chances.Dict) {
                    obj.Dict[entry.Key] = entry.Value;
                }
            }

            FileManager.WriteCopy(Save.Instance.OutputPath, uniqueitemlist);
        }

        public void addRandomAffixesToLootTable(GrimObject obj, GrimObject affixes)
        {
            Debug.Log(affixes.Dict.Values.Count);
            foreach (KeyValuePair<string, string> entry in affixes.Dict) {
                obj.Dict[entry.Key] = entry.Value;
            }
        }

        public bool isUniqueItemLootTable(GrimObject obj)
        {
            //List<string> mustbezero = new List<string>() { "suffixOnly", "prefixOnly", "rarePrefixOnly", "bothPrefixSuffix", "rareBothPrefixSuffix" };

            if (obj.Dict.ContainsKey("itemNames")) {
                string path = Save.Instance.FilesToEditPath + "/" + obj.Dict["itemNames"].getFirstRecord();

                if (File.Exists(path)) {
                    GrimObject item = new GrimObject(path);

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