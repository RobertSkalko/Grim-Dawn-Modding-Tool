using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class ToolTQUniquesShowAffixes : ToolButton
    {
        public override string Name { get => "ToolTQUniquesShowAffixes"; }
        public override string Description { get; }

        public override Predicate<TQObject> GetObjectPredicate {
            get =>
            new Predicate<TQObject>(x => x.Dict["Class"].Contains("OneShot_Potion"));
        }

        public override Predicate<string> GetFilePathPredicate {
            get =>
            new Predicate<string>(x => x.Contains("item"));
        }

        protected override void Action()
        {
            ConcurrentBag<TQObject> list = GetAllObjects(Save.Instance.GetRecordsPath());

            var newlist = new List<TQObject>();

            foreach (TQObject obj in list) {
                if (obj.Dict["hidePrefixName"].Equals("1") || obj.Dict["hideSuffixName"].Equals("1")) {
                    obj.Dict["hidePrefixName"] = "0";
                    obj.Dict["hideSuffixName"] = "0";
                }

                newlist.Add(obj);
            }

            FileManager.WriteCopy(Save.Instance.GetOutputPath(), newlist);
        }
    }
}