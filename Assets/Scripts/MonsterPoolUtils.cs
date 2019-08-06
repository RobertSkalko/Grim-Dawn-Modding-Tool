using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class MonsterPoolUtils
    {
        public static TQObject MultiplyHeroSpawnsByPercent(TQObject obj, int percent)
        {
            int avgNormalPercent = GetAvgNonHeroSpawnWeight(obj);

            foreach (string key in obj.Dict.Keys.ToList()) {
                string k = key.ToLower();

                if (k.Contains("weight")) {
                    int weight = 0;
                    bool parsed = int.TryParse(obj.Dict[key], out weight);

                    if (parsed) {
                        weight *= 100; // ensure that its big enough for a percent increase cus its int

                        if (k.Contains("champion")) {
                            weight *= 1 + (percent / 100);
                        }

                        obj.Dict[key] = weight + "";
                    }
                }
            }
            return obj;
        }

        public static int GetAvgNonHeroSpawnWeight(TQObject obj)
        {
            int avgNormalSpawnWeight = 1;

            foreach (string key in obj.Dict.Keys) {
                string k = key.ToLower();

                List<int> weights = new List<int>();

                if (k.Contains("weight") && k.Contains("champion") == false) {
                    int weight = 0;
                    bool parsed = int.TryParse(obj.Dict[key], out weight);

                    if (parsed) {
                        weights.Add(weight);
                    }
                }
                if (weights.Count > 0) {
                    avgNormalSpawnWeight = weights.Sum() / weights.Count;
                }
                else {
                    avgNormalSpawnWeight = 0;
                }
            }
            return avgNormalSpawnWeight;
        }
    }
}