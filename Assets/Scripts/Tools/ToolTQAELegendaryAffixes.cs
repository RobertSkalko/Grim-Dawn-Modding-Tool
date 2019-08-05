using System;
using System.Collections;
using System.Collections.Concurrent;
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

        public override Predicate<TQObject> GetObjectPredicate {
            get =>
            new Predicate<TQObject>(x => true);
        }

        public override Predicate<string> GetFilePathPredicate {
            get =>
            new Predicate<string>(x => x.Contains("loottables"));
        }

        protected override void Action()
        {
            TQObject chances = new TQObject(Path.Combine(Save.Instance.GetDataPath(), "chances.txt"));

            ConcurrentBag<TQObject> list = GetAllObjects(Save.Instance.GetRecordsPath());

            var uniqueitemlist = new List<TQObject>();

            Dictionary<string, TQObject> dict = TQAffixUtils.makeAffixDict(list);

            foreach (TQObject obj in list) {
                if (obj.isUniqueItemLootTable()) {
                    uniqueitemlist.Add(obj);
                }
            }
            Debug.Log("There are " + uniqueitemlist.Count + " unique items.");

            foreach (TQObject obj in uniqueitemlist) {
                if (obj.Dict.ContainsKey("itemNames")) {
                    string path = obj.Dict["itemNames"].getFirstRecord().GetPathOfRecord();

                    if (File.Exists(path)) {
                        TQObject item = new TQObject(path);

                        string key = item.Dict["Class"];

                        if (dict.ContainsKey(key)) {
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

            uniqueitemlist.ForEach(x => x.ReplaceWithAllValuesOf(chances));

            FileManager.WriteCopy(Save.Instance.GetOutputPath(), uniqueitemlist);
        }

        public void addRandomAffixesToLootTable(TQObject obj, TQObject affixes)
        {
            foreach (KeyValuePair<string, string> entry in affixes.Dict) {
                obj.Dict[entry.Key] = entry.Value;
            }
        }
    }
}