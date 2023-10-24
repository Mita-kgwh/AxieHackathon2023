using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComradeType
{
    LEADER = 0,
    RED = 1,
    BLUE = 2,
    BLACK = 3,
    WHITE = 4
}
public class ComradeManager : MonoSingleton<ComradeManager> {

    [System.Serializable]
    public class ComradeProperty
    {
        public ComradeType type;
        public GameObject comrade;
    }

    public List<ComradeProperty> comrades;
    Dictionary<ComradeType, GameObject> dict = new Dictionary<ComradeType, GameObject>();

    private void Awake()
    {
        MakeDictionary();
    }

    private void MakeDictionary()
    {
        foreach (ComradeProperty cp in comrades)
        {
            if (!dict.ContainsKey(cp.type))
            {
                dict.Add(cp.type, cp.comrade);
            }
        }
    }

    public GameObject GetObjectByType(ComradeType type)
    {
        if (dict.ContainsKey(type))
        {
            return dict[type];
        }
        else
        {
            return null;
        }
    }

    public int GetLength()
    {
        return comrades.Count;
    }
}
