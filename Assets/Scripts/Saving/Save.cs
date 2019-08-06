using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class Save
    {
        private static Save instance = null;

        public static Save Instance {
            get {
                if (instance == null) {
                    instance = new Save();
                }

                return instance;
            }

            set => instance = value;
        }

        private static string SaveFileName = "Save.txt";
        private static string SaveDataPath = Application.persistentDataPath + "/SavedData/" + SaveFileName;
        private static string SaveDataPathWithoutFileName = Application.persistentDataPath + "/SavedData/";

        public void CreateFoldersIfEmpty()
        {
            List<string> dirs = new List<string>
            {
                GetOutputPath(),
                GetRecordsPath(),
                GetDataPath()
            };

            foreach (string dir in dirs) {
                if (Directory.Exists(dir) == false) {
                    Directory.CreateDirectory(dir);
                }
            }
        }

        public string GetFolder()
        {
            return SaveDataPathWithoutFileName;
        }

        public string GetOutputPath()
        {
            return SaveDataPathWithoutFileName + "output/";
        }

        public string GetRecordsPath()
        {
            return SaveDataPathWithoutFileName + "records/";
        }

        public string GetDataPath()
        {
            return SaveDataPathWithoutFileName + "data/";
        }

        public string InputCommand = "";
    }
}