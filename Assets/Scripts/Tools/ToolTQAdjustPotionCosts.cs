using System.Collections;

using System.Collections;

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

        protected override void Action()
        {
            float multi = float.Parse(Save.Instance.InputCommand);

            List<GrimObject> list = new List<GrimObject>(FileManager.GetObjectsFromAllFilesInPath(Path.Combine(Save.Instance.FilesToEditPath, "records"), true).Where(x => x.FilePath.Contains("oneshot") && x.Dict.ContainsKey("Class") && x.Dict["Class"].Contains("OneShot_Potion")));

            var newlist = new List<GrimObject>();

            foreach (GrimObject obj in list) {
                foreach (var key in obj.Dict.Keys.ToList()) {
                    if (key.Contains("itemCost")) {
                        obj.Dict["itemCost"] = (multi * float.Parse(obj.Dict["itemCost"])) + "";
                    }
                }

                newlist.Add(obj);
            }

            FileManager.WriteCopy(Save.Instance.OutputPath, newlist);
        }
    }
}