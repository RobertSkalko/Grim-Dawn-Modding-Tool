using System.Collections;
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

        protected override void Action()
        {
            float multi = float.Parse(Save.Instance.InputCommand);

            List<GrimObject> list = new List<GrimObject>(FileManager.GetObjectsFromAllFilesInPath(Path.Combine(Save.Instance.FilesToEditPath, "records", "game"), true).Where(x => x.Dict.ContainsKey("templateName") && x.Dict["templateName"].Equals("database\\Templates\\ItemCost.tpl")));

            var newlist = new List<GrimObject>();

            foreach (GrimObject obj in list) {
                foreach (var key in obj.Dict.Keys.ToList()) {
                    if (key.Contains("Equation")) {
                        obj.Dict[key] = multi + "*(" + obj.Dict[key] + ")";
                    }
                }

                newlist.Add(obj);
            }

            FileManager.WriteCopy(Save.Instance.OutputPath, newlist);
        }
    }
}