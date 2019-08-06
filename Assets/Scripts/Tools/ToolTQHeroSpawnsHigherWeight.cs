using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class ToolTQHeroSpawnsHigherWeight : ToolButton
    {
        public override string Name { get => "More Hero spawns/weight"; }
        public override string Description { get; }
        public override Predicate<TQObject> GetObjectPredicate { get => new Predicate<TQObject>(x => x.IsMonsterPool()); }

        public override Predicate<string> GetFilePathPredicate { get => new Predicate<string>(x => x.Contains("pool") && x.Contains("boss") == false && x.Contains("quest") == false); }

        protected override void Action()
        {
            ConcurrentBag<TQObject> list = GetAllObjects(Save.Instance.GetRecordsPath());

            foreach (TQObject obj in list) {
                MonsterPoolUtils.MultiplyHeroSpawnsByPercent(obj, 500);

                if (obj.Dict.ContainsKey("championMax")) {
                    int max = int.Parse(obj.Dict["championMax"]);
                    obj.Dict["championMax"] = (max + 1).ToString();
                }
            }

            FileManager.WriteCopy(Save.Instance.GetOutputPath(), list);
        }
    }
}