using System.IO;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree.Data.Json;

namespace BehaviourTree.IO
{
    /// <summary>
    /// Json‚ÆBehaviourTree‚Ì’‡‰îƒNƒ‰ƒX
    /// </summary>

    public class BehaviourTreeJsonMeditor
    {
        const string JsonPath = "Assets/Scripts/Utility/BehaviourTree/Json/UserPath.json";

        public IOPathModel Read()
        {
            StreamReader reader = new StreamReader(JsonPath);
            string read = reader.ReadToEnd();

            IOPathModel model;

            try
            {
                model = JsonUtility.FromJson<IOPathModel>(read);
            }
            catch (System.Exception)
            {
                model = null;
            }

            return model;
        }

        public void Write(List<string> list)
        {
            IOPathModel model = CreateModel(list);
            string json = JsonUtility.ToJson(model);

            StreamWriter writer = new StreamWriter(JsonPath, false);
            writer.WriteLine(json);
            writer.Flush();
            writer.Close();
        }

        IOPathModel CreateModel(List<string> pathList)
        {
            IOPathModel model = new IOPathModel();
            int length = pathList.Count;

            model.DataArray = new PathData[length];

            for (int index = 0; index < length; index++)
            {
                model.DataArray[index] = new PathData();
                model.DataArray[index].Path = pathList[index];
            }

            return model;
        }
    }
}