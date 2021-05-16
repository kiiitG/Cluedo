using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class Kek
{
    Type[] types = new Type[] { typeof(UIManager) };

    public void kek()
    {
        using (StreamWriter sw = new StreamWriter("classInfo.csv", false, Encoding.UTF8))
        {
            Type cur; 
            for (int i = 0; i < types.Length; i++)
            {
                cur = types[i];
                string clazz = cur.Name + "\n";
                foreach (var property in cur.GetProperties())
                {
                    clazz += property.Name + ";";
                    clazz += "public;";
                    clazz += property.PropertyType.Name + ";";
                    clazz += "-;\n";
                }
                foreach (var method in cur.GetMethods())
                {
                    clazz += method.Name + ";";
                    clazz += "public;";
                    clazz += method.ReturnType.Name + ";";
                    if (method.GetParameters().Length == 0)
                    {
                        clazz += "-;";
                    }
                    else
                    {
                        foreach (var arrgument in method.GetParameters())
                        {
                            clazz += arrgument.ParameterType.Name + " " + arrgument.Name + ", ";
                        }
                    }
                    
                    clazz += "\n";
                }
                sw.WriteLine(clazz);
                clazz = "";
            }
        }
    }
}
