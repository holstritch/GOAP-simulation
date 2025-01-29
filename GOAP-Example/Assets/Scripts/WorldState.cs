using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "GOAP/WorldStateKey")]
public class WorldState : ScriptableObject
{
    public string stateKeyName;
    public int value;
}
