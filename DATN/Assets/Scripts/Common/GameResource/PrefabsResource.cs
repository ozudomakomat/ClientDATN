using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Prefabs", fileName = "prefabs")]
public class PrefabsResource : ScriptableObject
{
    public List<DefinePrefab> m_ALLPrefabs;

    public GameObject LoadPrefab(TypeEffect type)
    {
        foreach (var i in m_ALLPrefabs)
        {
            if (i.m_PrefabType == type && type != TypeEffect.null_type)
            {
                return i.m_Prefab;
            }
        }
        return null;
    }
}

[System.Serializable]
public class DefinePrefab
{
    public TypeEffect m_PrefabType;
    public GameObject m_Prefab;
}
