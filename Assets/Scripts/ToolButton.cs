using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace GrimDawnModdingTool
{
    public abstract class ToolButton
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        private bool StoppedBecauseError = false;

        public void DoAction()
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();

            StoppedBecauseError = false;

            if (!Directory.Exists(Save.Instance.FilesToEditPath)) {
                Debug.Log("Please Enter FilesToEditPath");
                StoppedBecauseError = true;
            }
            if (!Directory.Exists(Save.Instance.OutputPath)) {
                Debug.Log("Please Enter OutputPath");
                StoppedBecauseError = true;
            }
            if (string.IsNullOrEmpty(Save.Instance.DataPath)) {
                Debug.Log("Please Enter Data path");
                StoppedBecauseError = true;
            }

            stop.Stop();
            Debug.Log("Checking for Action errors took: " + stop.ElapsedMilliseconds + " Miliseconds or " + stop.ElapsedMilliseconds / 1000 + " Seconds");

            if (!StoppedBecauseError) {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Action();
                stopwatch.Stop();

                AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("TaskComplete"), new Vector3());

                Debug.Log("Everything Took: " + stopwatch.ElapsedMilliseconds + " Miliseconds or " + stopwatch.ElapsedMilliseconds / 1000 + " Seconds");
            }
        }

        protected abstract void Action();
    }
}