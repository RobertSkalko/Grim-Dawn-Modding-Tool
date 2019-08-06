using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class ToolTQMoreHeroSpawns : ToolButton
    {
        public override string Name { get => "More Min/Max Hero spawns"; }
        public override string Description { get; }
        public override Predicate<TQObject> GetObjectPredicate { get => new Predicate<TQObject>(x => x.IsProxyPoolEquation()); }

        public override Predicate<string> GetFilePathPredicate { get => new Predicate<string>(x => x.Contains("prox") && x.Contains("boss") == false && x.Contains("quest") == false); }

        protected override void Action()
        {
            ConcurrentBag<TQObject> list = GetAllObjects(Save.Instance.GetRecordsPath());

            foreach (TQObject obj in list) {
                obj.Dict["championMinEquation"] = 1 + "+" + obj.Dict["championMinEquation"];
                obj.Dict["championMaxEquation"] = 3 + "+" + obj.Dict["championMaxEquation"];
            }

            FileManager.WriteCopy(Save.Instance.GetOutputPath(), list);
        }
    }
}