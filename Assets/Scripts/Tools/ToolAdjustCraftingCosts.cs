using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class ToolAdjustCraftingCosts : ToolButton
    {
        public override string Name { get => "Adjust Crafting Costs (float in input)"; }
        public override string Description { get; }

        protected override void Action()
        {
            var list = new List<GrimObject>(FileManager.GetObjectsFromAllFilesInPath(Path.Combine(Save.Instance.FilesToEditPath, "records", "items", "crafting"), true));

            var newlist = new List<GrimObject>();

            Debug.Log(Save.Instance.InputCommand);

            float multi = float.Parse(Save.Instance.InputCommand);

            foreach (GrimObject obj in list) {
                if (obj.Dict.ContainsKey("artifactCreationCost") && float.TryParse(obj.Dict["artifactCreationCost"], out float se) == true) {
                    Debug.Log(obj.Dict["artifactCreationCost"]);

                    obj.Dict["artifactCreationCost"] = (float.Parse(obj.Dict["artifactCreationCost"]) * multi) + "";

                    newlist.Add(obj);
                }
            }

            FileManager.WriteCopy(Save.Instance.OutputPath, newlist);
        }
    }
}