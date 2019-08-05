using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class TQObject : IEquatable<TQObject>, IComparable<TQObject>
    {
        public bool Equals(TQObject other)
        {
            return this.Dict == other.Dict;
        }

        public void ReplaceWithAllValuesOf(TQObject obj)
        {
            foreach (KeyValuePair<string, string> entry in obj.Dict) {
                this.Dict[entry.Key] = entry.Value;
            }
        }

        public int CompareTo(TQObject other)
        {
            return FilePath.CompareTo(other.FilePath) == 1 ? 1 : 0;
        }

        public bool HasTemplate()
        {
            return Dict.ContainsKey("templateName");
        }

        public string GetTemplate()
        {
            return Dict["templateName"];
        }

        public bool HasClass()
        {
            return Dict.ContainsKey("Class");
        }

        public string GetClass()
        {
            return Dict["Class"];
        }

        public override int GetHashCode()
        {
            return Dict.GetHashCode();
        }

        public bool anyKeyContains(string str)
        {
            foreach (var key in Dict.Keys.ToList()) {
                if (key.Contains(str)) {
                    return true;
                }
            }

            return false;
        }

        public string FilePath;
        public Dictionary<string, string> Dict = new Dictionary<string, string>();

        public TQObject(string path)
        {
            string text = CommentRemover.RemoveComments(File.ReadAllText(path));

            this.FilePath = path;

            SetupDBInfo(text);
        }

        public TQObject(string text, string filepath)
        {
            this.FilePath = filepath;

            SetupDBInfo(text);
        }

        public TQObject()
        {
        }

        private List<string> RemoveEmptyStrings(string[] strings)
        {
            return strings.ToList().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        }

        private string TrimWhiteSpaceAtBothEnds(string s)
        {
            return s.TrimEnd().TrimStart();
        }

        private void SetupDBInfo(string txt)
        {
            foreach (string s in RemoveEmptyStrings(txt.Split('\n'))) {
                if (string.IsNullOrWhiteSpace(s)) {
                    continue;
                }
                string trimmed = TrimWhiteSpaceAtBothEnds(s);

                if (trimmed.Contains(",")) {
                    var objs = trimmed.Split(',');
                    if (objs.Length >= 2) {
                        string left = objs[0];
                        string right = objs[1];

                        Dict.Add(left, right);
                    }
                }
            }
        }

        public string GetTextRepresentation()
        {
            string text = "";

            foreach (var item in Dict) {
                text += item.Key + "," + item.Value + "," + "\n";
            }

            return text;
        }
    }
}