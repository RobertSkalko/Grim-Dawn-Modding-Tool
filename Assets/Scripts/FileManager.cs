using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Profiling;
using Debug = UnityEngine.Debug;

namespace GrimDawnModdingTool
{
    public static class FileManager
    {
        public static void WriteCopy(string path, IEnumerable<TQObject> files)
        {
            foreach (TQObject obj in files) {
                string newpath = Save.Instance.GetOutputPath() + "/" + obj.FilePath.Replace(Save.Instance.GetRecordsPath(), "");

                string dirpath = Path.GetDirectoryName(newpath);

                if (Directory.Exists(dirpath) == false) {
                    Directory.CreateDirectory(dirpath);
                }

                File.WriteAllText(newpath, obj.GetTextRepresentation());
            }
        }

        public static ConcurrentBag<TQObject> GetAllObjects(string path, Predicate<string> pathpred, Predicate<TQObject> objpred, string ending = ".dbr")
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var list = new ConcurrentBag<TQObject>();

            Parallel.ForEach(Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories), (file) => {
                if (file.EndsWith(ending) && pathpred(file)) {
                    TQObject obj = new TQObject(file);
                    if (objpred(obj)) {
                        list.Add(obj);
                    }
                }
            });

            stopwatch.Stop();
            Debug.Log("Getting Objects from files took: " + stopwatch.ElapsedMilliseconds + " Miliseconds or " + stopwatch.ElapsedMilliseconds / 1000 + " Seconds");

            return list;
        }
    }
}