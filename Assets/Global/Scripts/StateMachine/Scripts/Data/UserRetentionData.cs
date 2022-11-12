using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MonoState.Data
{
    public interface IRetentionData
    {
        string Path { get; }
        Object RetentionData();
    }

    public class UserRetentionData
    {
        Dictionary<string, IRetentionData> _dataDic = new Dictionary<string, IRetentionData>();

        public void SetData(IRetentionData data)
        {
            Object retentionData = data.RetentionData();
            string key = data.Path;
            Debug.Log(key);
            _dataDic.Add(key, data);
        }

        public Data GetData<Data>(string key) where Data : Object
        {
            IRetentionData retentionData = _dataDic.First(d => d.Key == key).Value;
            Object data = retentionData.RetentionData();

            return (Data)data;
        }
    }
}