using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class TQAffixTable : IEquatable<TQAffixTable>
    {
        public int number = 0;

        public int level = 0;
        public string typeName = "";
        public string tableName = "";
        public string weightName = "";

        public string table = "";
        public string weight = "100";

        public TQAffixTable(int number, string type, int lvl)
        {
            this.typeName = type;
            this.number = number;
            this.level = lvl;

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

        public override int GetHashCode()
        {
            var hashCode = -305050191;
            hashCode = hashCode * -1521134295 + number.GetHashCode();
            hashCode = hashCode * -1521134295 + level.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(typeName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(tableName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(weightName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(table);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(weight);
            return hashCode;
        }

        public bool Equals(TQAffixTable table)
        {
            return table != null &&
                 number == table.number &&
                 level == table.level &&
                 typeName == table.typeName &&
                 tableName == table.tableName &&
                 weightName == table.weightName &&
                 this.table == table.table &&
                 weight == table.weight;
        }
    }
}