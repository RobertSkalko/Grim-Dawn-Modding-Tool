using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class ToolTQAdjustPotionCosts : ToolButton
    {
        public override string Name { get => "ToolTQAdjustPotionCosts"; }
        public override string Description { get; }

        public override Predicate<TQObject> GetObjectPredicate {
            get =>
            new Predicate<TQObject>(x => x.HasClass() && x.GetClass().Contains("OneShot_Potion") && x.hasCost());
        }

        public override Predicate<string> GetFilePathPredicate {
            get =>
            new Predicate<string>(x => x.Contains("oneshot"));
        }

        protected override void Action()
        {
            float multi = float.Parse(Save.Instance.InputCommand);

            ConcurrentBag<TQObject> list = this.GetAllObjectsInRecordsPath();

            foreach (TQObject obj in list) {
                obj.Dict["itemCost"] = (multi * float.Parse(obj.getCost())) + "";
            }

            this.WriteToOutput(list);
        }
    }
}