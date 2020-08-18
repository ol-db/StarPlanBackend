using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StarPlan.StarPlanConfig
{
    /// <todo>
    /// fix dynamic late binding
    /// </todo>

    /// <todo>
    /// fix file handling
    /// prevent hard-coding directory
    /// </todo>
    public class StarPlanConfig
    {
        /// <summary>
        /// returns config file
        /// as an object
        /// </summary>
        /// <returns></returns>
        private static dynamic GetConfig()
        {
            //gets root project dir where appsettings.json is stored
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;

            //reads config.json and returns the dynamic object representing the conf file
            using (StreamReader r = new StreamReader(filePath + "/StarPlan/config.json"))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);

                return array;
            }
        }

        /// <summary>
        /// returns db from config file
        /// based on the specific name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetDB(string name)
        {
            return GetConfig()["ConnectionStrings"][name];
        }
    }
}
