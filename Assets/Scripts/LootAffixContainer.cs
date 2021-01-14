using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrimDawnModdingTool
{

    public class LootAffixTable
    {
        public void testPrint()
        {
            //    Debug.Log("prefixes: "+affixes.prefixes.GetTextRepresentation());
            //  Debug.Log("suffixes: "+affixes.suffixes.GetTextRepresentation());

        }

        private Dictionary<int, List<TQAffixTable>> affixesForLevelRanges = new Dictionary<int, List<TQAffixTable>>();



        public void AddAffixTables(TQAffixTable affixtable)
        {
            int lvlRange = affixtable.level;


            if (!affixesForLevelRanges.ContainsKey(lvlRange))
            {
                affixesForLevelRanges[lvlRange] = new List<TQAffixTable>();

            }

            affixesForLevelRanges[lvlRange].Add(affixtable);

        }


        static int AFFIX_MAX = 30;
        public void GetAffixesFor(TQObject loottable, TQObject item)
        {
            int lvl = item.GetItemLevel();

            List<TQAffixTable> list = new List<TQAffixTable>();



            List<int> keys = new List<int>(this.affixesForLevelRanges.Keys);

            keys.Sort((a, b) => Math.Abs(a - lvl).CompareTo(Math.Abs(b - lvl)));

           // Debug.Log("for lvl: " + lvl + " sorted by least diff " +string.Join(",", keys));


            Boolean adding = true;

            int prefixes = 0;
            int suffixes = 0;
            while (adding)
            {


                if (prefixes > AFFIX_MAX && suffixes > AFFIX_MAX)
                {
                    adding = false;
                    break;
                }

                if (keys.Count == 0)
                {
                    adding = false;
                    break;
                }

                List<TQAffixTable> affixes = this.affixesForLevelRanges[keys[0]];

                keys.RemoveAt(0);

                foreach (TQAffixTable table in affixes)
                {
                    if (table.IsPrefix())
                    {
                        if (prefixes < AFFIX_MAX)
                        {
                            prefixes++;
                            list.Add(table);
                        }
                    }
                    else
                    {
                        if (suffixes < AFFIX_MAX)
                        {
                            suffixes++;
                            list.Add(table);

                        }

                    }
                }
                
                

            }



            int prefixNum = 1;
            int suffixNum = 1;

            

            foreach (TQAffixTable matching in list)
            {
                int num = 0;


                if (matching.IsPrefix())
                {
                    num = prefixNum;
                    prefixNum++;
                }
                else
                {
                    num = suffixNum;
                    suffixNum++;
                }

                loottable.Dict[matching.getTableKey(num)] = matching.recordValue;
                loottable.Dict[matching.getWeightKey(num)] = matching.weightValue;

            }


        }
    }
}