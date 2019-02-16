using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class TQAffixTable
    {
        public TQAffixTable(GrimObject obj, int number, string type)
        {
            this.typeName = type;
            this.number = number;

            this.setNameInfo();
        }

        public void addTo(GrimObject obj)
        {
            obj.Dict[this.tableName] = table;
            obj.Dict[this.weightName] = weight;
        }

        public void setData(GrimObject obj)
        {
            this.table = obj.Dict[tableName];
            this.weight = obj.Dict[weightName];
        }

        public void setNameInfo()
        {
            this.tableName = typeName + "RandomizerName" + number;
            this.weightName = typeName + "RandomizerWeight" + number;
        }

        public bool exists(GrimObject obj)
        {
            return obj.Dict.ContainsKey(this.tableName);
        }

        public int number = 0;

        public string typeName;
        public string tableName;
        public string weightName;

        public string table;
        public string weight;
    }
}