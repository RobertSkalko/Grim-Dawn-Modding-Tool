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

        public string recordValue = "";
        public string weightValue = "100";

        public bool IsPrefix()
        {
            return this.typeName.ToLower().Equals("prefix");
        }

        public TQAffixTable(int number, string type, int lvl)
        {
            this.typeName = type;
            this.number = number;
            this.level = lvl;
        }

        public void addTo(TQObject obj)
        {
            obj.Dict[this.getTableKey(number)] = recordValue;
            obj.Dict[this.getWeightKey(number)] = weightValue;
        }

        public void setData(TQObject obj)
        {
            this.recordValue = obj.Dict[getTableKey(number)];

            if (obj.Dict.ContainsKey(getWeightKey(number))) {
                this.weightValue = obj.Dict[getWeightKey(number)];
            }
        }

        public string getTableKey(int num)
        {
            return typeName + "RandomizerName" + num;
        }

        public string getWeightKey(int num)
        {
            return typeName + "RandomizerWeight" + num;
        }

        public bool exists(TQObject obj)
        {
            return obj.Dict.ContainsKey(this.getTableKey(number));
        }

        public override int GetHashCode()
        {
            var hashCode = -305050191;
            hashCode = hashCode * -1521134295 + number.GetHashCode();
            hashCode = hashCode * -1521134295 + level.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(typeName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(recordValue);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(weightValue);
            return hashCode;
        }

        public bool Equals(TQAffixTable table)
        {
            return table != null &&
                 number == table.number &&
                 level == table.level &&
                 typeName == table.typeName &&
                 this.recordValue == table.recordValue &&
                 weightValue == table.weightValue;
        }
    }
}