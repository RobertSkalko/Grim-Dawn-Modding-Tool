﻿using System;
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
            new Predicate<TQObject>(x => x.HasClass() && x.GetClass().Contains("LootItemTable") && x.getFirstObjectOfLootTable() != null);
        }

        public override Predicate<string> GetFilePathPredicate {
            get =>
            new Predicate<string>(x => x.Contains("loottables") || x.Contains("lootables"));
        }

        protected override void Action()
        {
            TQObject chances = new TQObject(Path.Combine(Save.Instance.GetDataPath(), "chances.txt"));
            ConcurrentBag<TQObject> allLootTables = GetAllObjects(Save.Instance.GetRecordsPath());
            
            Debug.Log("There are " + allLootTables.Count + " item loot tables.");

            LootAffixes affixes = new LootAffixes(allLootTables);
            var uniqueitemLoottableList = allLootTables.Where(x => x.isUniqueItemLootTable()).ToList();

          
            Debug.Log("There are " + uniqueitemLoottableList.Count + " unique item loot tables.");

            affixes.testPrint();

            
            foreach (TQObject loottable in uniqueitemLoottableList) {
                TQObject gear = loottable.getFirstObjectOfLootTable();

                if (gear != null) {
                    string type = gear.GetClass();

                    affixes.TryGiveAffixesToLoottable(loottable, type);
                }
                else
                {
                    Debug.Log("gear is null!!!");
                }
            }

            uniqueitemLoottableList.ForEach(x => x.ReplaceWithAllValuesOf(chances));

            FileManager.WriteCopy(Save.Instance.GetOutputPath(), uniqueitemLoottableList);
        }

        public void addRandomAffixesToLootTable(TQObject obj, TQObject affixes)
        {
            foreach (KeyValuePair<string, string> entry in affixes.Dict) {
                obj.Dict[entry.Key] = entry.Value;
            }
        }
    }
}