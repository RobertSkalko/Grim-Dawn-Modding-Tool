using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace GrimDawnModdingTool
{
    public static class GrimObjectGenerator
    {
        public static ConcurrentBag<GrimObject> GenerateSoldakObjects(ConcurrentDictionary<string, string> Files, bool AllowDuplicates = false)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var newList = new ConcurrentBag<GrimObject>();

            Debug.Log(Files.Values.Count);

            Parallel.ForEach(Files, (file) => {
                newList.Add(new GrimObject(file.Key, file.Value));
            });

            if (!AllowDuplicates) {
                var nonDuplicateList = new HashSet<GrimObject>(newList);

                Debug.Log(nonDuplicateList.Count());

                stopwatch.Stop();
                Debug.Log("Creating objects and Removing duplicate objects took " + stopwatch.ElapsedMilliseconds + " Miliseconds or " + stopwatch.ElapsedMilliseconds / 1000 + " Seconds");

                return new ConcurrentBag<GrimObject>(nonDuplicateList);
            }
            else {
                stopwatch.Stop();
                Debug.Log("Creating objects without removing any duplicates took " + stopwatch.ElapsedMilliseconds + " Miliseconds or " + stopwatch.ElapsedMilliseconds / 1000 + " Seconds");

                return new ConcurrentBag<GrimObject>(newList);
            }
        }
    }
}