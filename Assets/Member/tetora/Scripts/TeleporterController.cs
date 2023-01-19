using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController
{
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

    int _id;
    
    List<TeleportPoint> _teleportList = new List<TeleportPoint>();

    public void CreateData(Transform transform, System.Action<int> action, int telepoatObjLength)
    {
        TeleportPoint teleportPoint = new TeleportPoint(_id, transform);
        Teleporter teleporter = transform.gameObject.GetComponent<Teleporter>();
        teleporter.SetData(_id, action, GetData, telepoatObjLength);

        _id++;
        _teleportList.Add(teleportPoint);
    }
    public Transform GetData(int id)
    {
        return _teleportList.Find(x => x.ID == id).ObjTransform;
    }
}

