using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class ToolAdjustEnchantCost : ToolButton
    {
        public override string Name { get => "Adjust Enchant Costs (float in input)"; }
        public override string Description { get; }

        protected override void Action()
        {
            var list = new List<GrimObject>(FileManager.GetObjectsFromAllFilesInPath(Path.Combine(Save.Instance.FilesToEditPath, "records", "items", "enchants"), true));

            var newlist = new List<GrimObject>();

            float multi = float.Parse(Save.Instance.InputCommand);

            foreach (GrimObject obj in list) {
                if (obj.Dict.ContainsKey("Class") && obj.Dict["Class"] == "ItemEnchantment") {
                    obj.Dict["itemCost"] = (int.Parse(obj.Dict["itemCost"]) * multi) + "";

                    newlist.Add(obj);
                }
            }

            FileManager.WriteCopy(Save.Instance.OutputPath, newlist);
        }
    }
}