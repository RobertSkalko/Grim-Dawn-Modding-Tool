using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace GrimDawnModdingTool
{
    public abstract class ToolButton
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract Predicate<TQObject> GetObjectPredicate { get; }
        public abstract Predicate<string> GetFilePathPredicate { get; }

        private bool StoppedBecauseError = false;

        public void DoAction()
        {
            //          Stopwatch stop = new Stopwatch();
            //            stop.Start();

            StoppedBecauseError = false;

            //  stop.Stop();
            // Debug.Log("Checking for Action errors took: " + stop.ElapsedMilliseconds + " Miliseconds or " + stop.ElapsedMilliseconds / 1000 + " Seconds");

            if (!StoppedBecauseError) {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Action();
                stopwatch.Stop();

                AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("TaskComplete"), new Vector3());

                Debug.Log("Everything Took: " + stopwatch.ElapsedMilliseconds + " Miliseconds or " + stopwatch.ElapsedMilliseconds / 1000 + " Seconds");
            }
        }

        private string ending = ".dbr";

        public ConcurrentBag<TQObject> GetAllObjects(string path)
        {
            var list = new ConcurrentBag<TQObject>();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Parallel.ForEach(Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories), (file) => {
                if (file.EndsWith(ending) && this.GetFilePathPredicate(file)) {
                    TQObject obj = new TQObject(file);
                    if (this.GetObjectPredicate(obj)) {
                        list.Add(obj);
                    }
                }
            });

            stopwatch.Stop();
            Debug.Log("Getting Objects from files took: " + stopwatch.ElapsedMilliseconds + " Miliseconds or " + stopwatch.ElapsedMilliseconds / 1000 + " Seconds");

            return list;
        }

        protected abstract void Action();
    }
}