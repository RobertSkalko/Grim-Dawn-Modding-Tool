using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class GrimObject : IEquatable<GrimObject>, IComparable<GrimObject>
    {
        public bool Equals(GrimObject other)
        {
            return this.Dict == other.Dict;
        }

        public int CompareTo(GrimObject other)
        {
            return FilePath.CompareTo(other.FilePath) == 1 ? 1 : 0;
        }

        public override int GetHashCode()
        {
            return Dict.GetHashCode();
        }

        public string FilePath;
        public Dictionary<string, string> Dict = new Dictionary<string, string>();

        public GrimObject(string path)
        {
            string text = File.ReadAllText(path);

            this.FilePath = path;

            SetupDBInfo(text);
        }

        public GrimObject(string text, string filepath)
        {
            this.FilePath = filepath;

            SetupDBInfo(text);
        }

        public GrimObject()
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