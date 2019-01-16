using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class ToolLegendaryAffixes : ToolButton
    {
        public override string Name { get => "Legendary Affixes"; }
        public override string Description { get; }

        public Dictionary<string, GrimObject> makeAffixDict(List<GrimObject> list)
        {
            int i = 0;

            Dictionary<string, List<AffixTable>> dict = new Dictionary<string, List<AffixTable>>();

            foreach (GrimObject obj in list) {
                if (obj.FilePath.Contains("loottables") && obj.Dict.ContainsKey("lootName1")) {
                    string path = Save.Instance.FilesToEditPath + "/" + obj.Dict["lootName1"];
                    GrimObject item = new GrimObject(path);

                    string key = item.Dict["Class"];

                    if (dict.ContainsKey(key) == false) {
                        dict[key] = new List<AffixTable>();
                    }

                    var types = new List<string>() { "suffix", "prefix", "rareSuffix", "rarePrefix" };

                    for (int y = 0; y < 10; y++) {
                        foreach (string type in types) {
                            var table = new AffixTable(obj, y, type);
                            if (table.exists(obj)) {
                                table.setData(obj);

                                bool add = true;

                                foreach (AffixTable checktable in dict[key]) {
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
            }

            var finaldict = new Dictionary<string, GrimObject>();

            foreach (KeyValuePair<string, List<AffixTable>> entry in dict) {
                var sorted = new Dictionary<string, List<AffixTable>>();

                var counts = new Dictionary<string, int>();

                foreach (AffixTable table in entry.Value) {
                    if (sorted.ContainsKey(table.typeName)) {
                        sorted[table.typeName].Add(table);
                    }
                    else {
                        sorted[table.typeName] = new List<AffixTable>();
                    }
                }
                foreach (KeyValuePair<string, List<AffixTable>> sort in sorted) {
                    foreach (AffixTable t in sort.Value) {
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

            var list = new List<GrimObject>(FileManager.GetObjectsFromAllFilesInPath(Path.Combine(Save.Instance.FilesToEditPath, "records", "items", "loottables"), true));

            var newlist = new List<GrimObject>();

            Dictionary<string, GrimObject> dict = makeAffixDict(list);

            foreach (GrimObject obj in list) {
                if (isUniqueItemLootTable(obj)) {
                    newlist.Add(obj);
                }
            }

            foreach (GrimObject obj in list) {
                if (obj.FilePath.Contains("loottables") && obj.Dict.ContainsKey("lootName1")) {
                    string path = Save.Instance.FilesToEditPath + "/" + obj.Dict["lootName1"];
                    GrimObject item = new GrimObject(path);

                    string key = item.Dict["Class"];

                    if (dict.ContainsKey(key)) {
                        addRandomAffixesToLootTable(obj, dict[key]);

                        Debug.Log("key exists: " + key);
                    }
                    else {
                        Debug.Log("no key: " + key);
                    }
                }
            }

            foreach (GrimObject obj in newlist) {
                foreach (KeyValuePair<string, string> entry in chances.Dict) {
                    obj.Dict[entry.Key] = entry.Value;
                }
            }

            FileManager.WriteCopy(Save.Instance.OutputPath, newlist);
        }

        public void addRandomAffixesToLootTable(GrimObject obj, GrimObject affixes)
        {
            foreach (KeyValuePair<string, string> entry in affixes.Dict) {
                obj.Dict[entry.Key] = entry.Value;
            }
        }

        public bool isUniqueItemLootTable(GrimObject obj)
        {
            //List<string> mustbezero = new List<string>() { "suffixOnly", "prefixOnly", "rarePrefixOnly", "bothPrefixSuffix", "rareBothPrefixSuffix" };

            if (obj.FilePath.Contains("loottables") && obj.Dict.ContainsKey("lootName1")) {
                string path = Save.Instance.FilesToEditPath + "/" + obj.Dict["lootName1"];
                GrimObject item = new GrimObject(path);

                if (item.isEpic() || item.isLegendary()) {
                    return true;
                }
            }

            return false;
        }
    }
}