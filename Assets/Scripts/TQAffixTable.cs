using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class TQAffixTable
    {
        public int number = 0;

        public string typeName;
        public string tableName;
        public string weightName;

        public string table;
        public string weight = "100";

        public TQAffixTable(int number, string type)
        {
            this.typeName = type;
            this.number = number;

            this.setNameInfo();
        }

        public void addTo(TQObject obj)
        {
            obj.Dict[this.tableName] = table;
            obj.Dict[this.weightName] = weight;
        }

        public void setData(TQObject obj)
        {
            this.table = obj.Dict[tableName];

            if (obj.Dict.ContainsKey(weightName)) {
                this.weight = obj.Dict[weightName];
            }
        }

        public void setNameInfo()
        {
            this.tableName = typeName + "RandomizerName" + number;
            this.weightName = typeName + "RandomizerWeight" + number;
        }

        public bool exists(TQObject obj)
        {
            return obj.Dict.ContainsKey(this.tableName);
        }
    }
}