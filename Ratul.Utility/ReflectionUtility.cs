﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ratul.Utility
{
    public class ReflectionUtility : Ratul.Utility.IReflectionUtility
    {
        public void AssignSameNamedPropertyValue(object source, object result)
        {
            foreach (PropertyInfo srcPropInfo in source.GetType().GetProperties())
            {
                foreach (PropertyInfo resPropInfo in result.GetType().GetProperties())
                {
                    if (srcPropInfo.Name == resPropInfo.Name && srcPropInfo.PropertyType == resPropInfo.PropertyType)
                    {
                        resPropInfo.SetValue(result, srcPropInfo.GetValue(source));
                    }
                }
            }
        }
    }
}
