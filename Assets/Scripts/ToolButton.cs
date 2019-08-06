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

        public void DoAction()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Action();
            stopwatch.Stop();

            AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("TaskComplete"), new Vector3());

            Debug.Log("Everything Took: " + stopwatch.ElapsedMilliseconds + " Miliseconds or " + stopwatch.ElapsedMilliseconds / 1000 + " Seconds");
        }

        public ConcurrentBag<TQObject> GetAllObjects(string path)
        {
            return FileManager.GetAllObjects(path, this.GetFilePathPredicate, this.GetObjectPredicate);
        }

        protected abstract void Action();
    }
}