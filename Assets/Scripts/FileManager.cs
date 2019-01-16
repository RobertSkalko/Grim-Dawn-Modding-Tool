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
        public static void WriteCopy(string path, List<GrimObject> files)
        {
            foreach (GrimObject obj in files) {
                string newpath = Save.Instance.OutputPath + "/" + obj.FilePath.Replace(Save.Instance.FilesToEditPath, "");

                string dirpath = Path.GetDirectoryName(newpath);

                if (Directory.Exists(dirpath) == false) {
                    Directory.CreateDirectory(dirpath);
                }

                File.WriteAllText(newpath, obj.GetTextRepresentation());
            }
        }

        public static void SaveOutputToFile(List<GrimObject> objects, string ending = ".dbr")
        {
            foreach (GrimObject obj in objects) {
                string path = obj.FilePath;

                if (!File.Exists(path)) {
                    Debug.Log("OutputPath Doesn't exist or not Inputed: " + path);

                    return;
                }

                File.WriteAllText(path, obj.GetTextRepresentation());

                Debug.Log("File saved to \"" + path + "\"");
            }
        }

        public static void SaveOutputToFile(string file, string ending = ".dbr")
        {
            if (!Directory.Exists(Save.Instance.OutputPath)) {
                Debug.Log("OutputPath Doesn't exist or not Inputed!");
                return;
            }
            string filename = Save.Instance.OutputPath + "/" + DateTime.Now.ToFileTime() + ending;
            File.WriteAllText(filename, file);

            Debug.Log("File saved to \"" + Save.Instance.OutputPath + "\"");
        }

        public static void WriteChangedFiles(string path, ConcurrentBag<string> files)
        {
            foreach (string file in files) {
                File.WriteAllText(path, file);
            }
        }

        public static ConcurrentBag<GrimObject> GetObjectsFromAllFilesInPath(string path, bool AllowDuplicates = false)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var files = GetAllGDBFilesInFolder(path);

            var fileswithoutcomments = CommentRemover.RemoveComments(files);

            var soldakobjects = GrimObjectGenerator.GenerateSoldakObjects(fileswithoutcomments, AllowDuplicates); // also highly intensive

            stopwatch.Stop();
            Debug.Log("Getting files and generating objects took: " + stopwatch.ElapsedMilliseconds + " Miliseconds or " + stopwatch.ElapsedMilliseconds / 1000 + " Seconds");

            return soldakobjects;
        }

        private static ConcurrentDictionary<string, string> GetAllGDBFilesInFolder(string path, string ending = ".dbr")
        {
            var dict = new ConcurrentDictionary<string, string>();

            Parallel.ForEach(Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories), (file) => {
                if (file.EndsWith(ending)) {
                    dict.TryAdd(File.ReadAllText(file), file);
                }
            });

            return dict;
        }
    }
}