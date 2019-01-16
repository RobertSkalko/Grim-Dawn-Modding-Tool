using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public static class CommentRemover
    {
        public static string RemoveComments(string file)
        {
            Regex comments1 = new Regex(@"//.*?\n");
            Regex comments2 = new Regex(@"/\*(.|\n)*?\*/");

            string afterRemoval = comments1.Replace(file, "\n");
            afterRemoval = comments2.Replace(afterRemoval, "");

            return afterRemoval;
        }

        public static ConcurrentDictionary<string, string> RemoveComments(ConcurrentDictionary<string, string> files)
        {
            var newDict = new ConcurrentDictionary<string, string>();

            foreach (var item in files) {
                newDict.TryAdd(RemoveComments(item.Key), item.Value);
            }

            return newDict;
        }
    }
}