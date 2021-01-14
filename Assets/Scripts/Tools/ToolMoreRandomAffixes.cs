using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class ToolMoreRandomAffixes : ToolButton
    {
        public override string Name { get => "ToolMoreRandomAffixes"; }
        public override string Description { get; }

        public override Predicate<TQObject> GetObjectPredicate
        {
            get =>
            new Predicate<TQObject>(x => x.HasClass() && x.anyKeyContains("lootRandomizerJitter"));
        }

        public override Predicate<string> GetFilePathPredicate
        {
            get =>
            new Predicate<string>(x => x.Contains("affix") || x.Contains("Affix"));
        }

        protected override void Action()
        {
            ConcurrentBag<TQObject> all = GetAllObjects(Save.Instance.GetRecordsPath());
                              
         
            foreach (TQObject loottable in all)
            {
              float jitter= float.Parse(loottable.Dict["lootRandomizerJitter"]);

                if (jitter > 1 && jitter < 50)
                {
                    loottable.Dict["lootRandomizerJitter"] = "50";

                }

            }
                     
            FileManager.WriteCopy(Save.Instance.GetOutputPath(), all);
        }

       
    }
}