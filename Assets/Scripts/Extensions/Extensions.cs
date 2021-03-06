﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace GrimDawnModdingTool
{
    public static class Extensions
    {
        public static string getFirstRecord(this string @this)
        {
            if (@this.Contains(";")) {
                return @this.Split(';')[0];
            }
            else {
                return @this;
            }
        }


        public static string GetPathOfRecord(this string record)
        {         
            return Save.Instance.GetFolder() + record;
        }

        public static void AddRange<T>(this ConcurrentBag<T> @this, IEnumerable<T> toAdd)
        {
            Parallel.ForEach(toAdd, (elem) => {
                @this.Add(elem);
            });
        }

        public static List<string> ToStringList(this List<TQObject> objects)
        {
            var list = new List<string>();
            objects.ForEach(x => list.Add(x.GetTextRepresentation()));

            return list;
        }

        public static string JoinIntoString(this List<TQObject> objects)
        {
            return string.Join("\n", objects.ToStringList().ToArray());
        }

        public static int IndexOf<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            int i = 0;

            foreach (TSource element in source) {
                if (predicate(element))
                    return i;

                i++;
            }

            return -1;
        }
    }
}