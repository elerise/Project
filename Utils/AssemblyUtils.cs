using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Auxiliary
{
    public static class AssemblyUtils
    {
        public static T CreateInstance<T>(string typeName, string asmName) where T : class
        {
            Assembly asm = string.IsNullOrEmpty(asmName) ? Assembly.GetCallingAssembly() : Assembly.LoadFrom(asmName);
            Type type = asm.GetType(typeName);
            if (type != null)
            {
                object obj = Activator.CreateInstance(type);
                return (obj as T);
            }
            return null;
        }        

    }
}
