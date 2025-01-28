using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;

public class UpdateWorld : MonoBehaviour
{
    public Text statesText;

    private void LateUpdate()
    {
        Dictionary<string, int> worldStates = GameWorld.Instance.GetWorld().GetStates();
        statesText.text = "";

        foreach (KeyValuePair<string, int> state in worldStates)
        {
            statesText.text += state.Key + ": " + state.Value + "\n";
        }
    }
}
