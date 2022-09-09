using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fusion Data")]
public class FusionData : ScriptableObject
{
    [SerializeField] List<FisionDatabase> m_datas;
}

[System.Serializable]
public class FisionDatabase
{

}
