using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class ToolGoldDropsAdjust : ToolButton
    {
        public override string Name { get => "Adjust Gold Drops (float in input)"; }
        public override string Description { get; }

        protected override void Action()
        {
            var list = new List<GrimObject>(FileManager.GetObjectsFromAllFilesInPath(Path.Combine(Save.Instance.FilesToEditPath, "records", "items", "lootchests"), true));

            var newlist = new List<GrimObject>();

            float multi = float.Parse(Save.Instance.InputCommand);

            foreach (GrimObject obj in list) {
                if (obj.Dict.ContainsKey("templateName") && obj.Dict["templateName"] == "database/templates/goldgenerator.tpl") {
                    obj.Dict["goldValueMin"] = (int.Parse(obj.Dict["goldValueMin"]) * multi) + "";
                    obj.Dict["goldValueMax"] = (int.Parse(obj.Dict["goldValueMax"]) * multi) + "";

                    newlist.Add(obj);
                }
            }

            FileManager.WriteCopy(Save.Instance.OutputPath, newlist);
        }
    }
}