using System.Collections.Generic;
using UnityEngine;

public abstract class Group
{
    public List<Transform> Members { get; set; }

    public Vector3 GetCenter()
    {
        Vector3 center = Vector3.zero;
        foreach (var member in Members)
        {
            center += member.position;
        }
        return center / Members.Count;
    }
}
