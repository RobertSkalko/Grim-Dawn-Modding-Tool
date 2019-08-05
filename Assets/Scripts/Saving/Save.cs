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
                    TryLoadStateFromFile();
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

        public string InputCommand;

        private static JsonSerializerSettings serSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        };

        private static JsonSerializerSettings deSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
        };

        public static void TryLoadStateFromFile()
        {
            string savepath = SaveDataPath;

            if (File.Exists(savepath)) {
                instance = (JsonConvert.DeserializeObject<Save>(File.ReadAllText(savepath), deSettings) != null) ? JsonConvert.DeserializeObject<Save>(File.ReadAllText(savepath), deSettings) : new Save();
            }
            else {
                instance = new Save();
            }
        }

        public static void SaveStateToFile()
        {
            string json = JsonConvert.SerializeObject(Instance, Formatting.Indented, serSettings);

            if (!Directory.Exists(SaveDataPathWithoutFileName)) {
                Directory.CreateDirectory(SaveDataPathWithoutFileName);
            }

            if (!System.IO.File.Exists(SaveDataPath)) {
                System.IO.File.Create(SaveFileName);
            }

            System.IO.File.WriteAllText(SaveDataPath, json);
        }
    }
}