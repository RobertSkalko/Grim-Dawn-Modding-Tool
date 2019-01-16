using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GrimDawnModdingTool
{
    public class ConsoleOutput : MonoBehaviour
    {
        public GameObject TextPrefab;
        public Scrollbar VerticalScrollbar;
        public GameObject Content;

        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            GameObject obj = Instantiate(TextPrefab, Content.transform);
            obj.GetComponent<Text>().text = logString;
            VerticalScrollbar.value = 0; // keeps it at the bottom!
        }
    }
}