using System.Collections;
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

        protected override void Action()
        {
            List<GrimObject> list = new List<GrimObject>(FileManager.GetObjectsFromAllFilesInPath(Path.Combine(Save.Instance.FilesToEditPath, "records"), true).Where(x => x.FilePath.Contains("item") && x.Dict.ContainsKey("itemClassification")));

            var newlist = new List<GrimObject>();

            foreach (GrimObject obj in list) {
                if (obj.Dict["hidePrefixName"].Equals("1") || obj.Dict["hideSuffixName"].Equals("1")) {
                    obj.Dict["hidePrefixName"] = "0";
                    obj.Dict["hideSuffixName"] = "0";
                }

                newlist.Add(obj);
            }

            FileManager.WriteCopy(Save.Instance.OutputPath, newlist);
        }
    }
}