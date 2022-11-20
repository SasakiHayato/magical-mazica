using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController
{
    List<TeleportPoint> _teleportList = new List<TeleportPoint>();
    int _id;
    public class TeleportPoint
    {
        public readonly int ID;
        public readonly Transform ObjTransform;
        public TeleportPoint(int id, Transform objTransform)
        {
            ID = id;
            ObjTransform = objTransform;
        }
    }
    public void CreateData(Transform transform, System.Action<int> action)
    {
        TeleportPoint teleportPoint = new TeleportPoint(_id, transform);
        Teleporter teleporter = transform.gameObject.AddComponent<Teleporter>();
        teleporter.SetData(_id, action);

        _id++;
        _teleportList.Add(teleportPoint);
    }
    public Transform GetData(int id)
    {
        return _teleportList.Find(x => x.ID == id).ObjTransform;
    }
}
