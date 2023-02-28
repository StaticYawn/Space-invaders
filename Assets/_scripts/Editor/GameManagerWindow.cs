using UnityEditor;
using UnityEngine;

public class GameManagerWindow : EditorWindow
{
    public FloatVariable variable;
    private GameManager _managerScript;

    [MenuItem("Window/GameManager")]
    static void Init()
    {
        GameManagerWindow window = (GameManagerWindow)GetWindow(typeof(GameManagerWindow), false, "GameManager");
        window.Show();
    }

    private void OnGUI()
    {
        _managerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        ScoreField();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Pause Game"))
        {
            _managerScript.Pause();
        }
        if(GUILayout.Button("Resume Game"))
        {
            _managerScript.Resume();
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Reset Enemies"))
        {
            _managerScript.ResetEnemies();
        }
    }

    private void ScoreField()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Score");
        int scoreField = EditorGUILayout.IntField((int)variable.Value);
        if (GUILayout.Button("Update Score"))
        {
            _managerScript.UpdateScoreUI();
        }
        variable.SetValue(scoreField);
        GUILayout.EndHorizontal();
    }

}
