using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class AffixTable
    {
        public AffixTable(TQObject obj, int number, string type)
        {
            this.typeName = type;
            this.number = number;

            this.setNameInfo();
        }

        public void addTo(TQObject obj)
        {
            obj.Dict[this.tableName] = table;
            obj.Dict[this.minLevelName] = minLevel;
            obj.Dict[this.maxLevelName] = maxLevel;
            obj.Dict[this.weightName] = weight;
        }

        public void setData(TQObject obj)
        {
            this.table = obj.Dict[tableName];
            this.weight = obj.Dict[weightName];
            this.minLevel = obj.Dict[minLevelName];
            this.maxLevel = obj.Dict[maxLevelName];
        }

        public void setNameInfo()
        {
            this.tableName = typeName + "TableName" + number;
            this.weightName = typeName + "TableWeight" + number;
            this.minLevelName = typeName + "TableLevelMin" + number;
            this.maxLevelName = typeName + "TableLevelMax" + number;
        }

        public bool exists(TQObject obj)
        {
            return obj.Dict.ContainsKey(this.tableName);
        }

        public int number = 0;

        public string typeName;
        public string tableName;
        public string weightName;
        public string minLevelName;
        public string maxLevelName;

        public string table;
        public string weight;
        public string minLevel;
        public string maxLevel;
    }
}