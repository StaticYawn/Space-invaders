using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class GameEventRaiseButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (GameEvent)target;

        if(GUILayout.Button("Raise", GUILayout.Height(20)))
        {
            script.Raise();
        }
    }
}
