using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CFG/Path", fileName = "New CFG path")]
public class PathScriptable : ScriptableObject
{
    public List<PathScriptable> Childrens;
}