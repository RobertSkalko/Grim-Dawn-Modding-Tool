using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public static class Main
    {
        public static List<ToolButton> Buttons => GetAllButtons();

        private static List<ToolButton> GetAllButtons()
        {
            List<Type> derivedTypes = VType.GetDerivedTypes(typeof(ToolButton), Assembly.GetExecutingAssembly());
            List<ToolButton> buttons = new List<ToolButton>();
            derivedTypes.ForEach(x => buttons.Add((ToolButton)Activator.CreateInstance(x)));
            return buttons;
        }

        public static string RemoveComments(string file)
        {
            Regex comments1 = new Regex(@"//.*?\n");
            Regex comments2 = new Regex(@"/\*(.|\n)*?\*/");

            string afterRemoval = comments1.Replace(file, "");
            afterRemoval = comments2.Replace(afterRemoval, "");

            return afterRemoval;
        }
    }

    public enum Modifiers
    {
        none,
        overrides,
        addsTo,
    }
}