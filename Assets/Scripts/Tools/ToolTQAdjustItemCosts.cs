using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class ToolTQAdjustItemCosts : ToolButton
    {
        public override string Name { get => "ToolTQAdjustItemCosts"; }
        public override string Description { get; }

        public override Predicate<TQObject> GetObjectPredicate {
            get =>
            new Predicate<TQObject>(x => x.Dict.ContainsKey("templateName") && x.Dict["templateName"].Contains("ItemCost") && x.anyKeyContains("Equation"));
        }

        public override Predicate<string> GetFilePathPredicate {
            get =>
            new Predicate<string>(x => true);
        }

        protected override void Action()
        {
            float multi = float.Parse(Save.Instance.InputCommand);

            ConcurrentBag<TQObject> list = GetAllObjects(Save.Instance.GetRecordsPath());

            var newlist = new List<TQObject>();

            foreach (TQObject obj in list) {
                foreach (var key in obj.Dict.Keys.ToList()) {
                    obj.Dict[key] = multi + "*(" + obj.Dict[key] + ")";
                }

                newlist.Add(obj);
            }

            FileManager.WriteCopy(Save.Instance.GetOutputPath(), newlist);
        }
    }
}