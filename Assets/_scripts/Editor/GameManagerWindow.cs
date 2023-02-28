using UnityEditor;
using UnityEngine;

public class GameManagerWindow : EditorWindow
{
    public FloatVariable EnemyRows;
    public FloatVariable EnemyColumns;
    public FloatVariable Score;


    GameManager _managerScript;

    bool _showEnemyStuff = false;
    bool _showPlayerStuff = false;
    bool _showGameStuff = false;

    [MenuItem("Window/GameManager")]
    static void Init()
    {
        GameManagerWindow window = (GameManagerWindow)GetWindow(typeof(GameManagerWindow), false, "GameManager");
        window.Show();
    }

    private void OnEnable()
    {
        _managerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void OnGUI()
    {
        _showPlayerStuff = EditorGUILayout.Foldout(_showPlayerStuff, "Player Stuff");
        if (_showPlayerStuff)
        {

        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        _showEnemyStuff = EditorGUILayout.Foldout(_showEnemyStuff, "Enemy Stuff");
        if (_showEnemyStuff)
        {
            EnemyRowAndColCount();
            if (GUILayout.Button("Reset Enemies"))
            {
                _managerScript.ResetEnemies();
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        _showGameStuff = EditorGUILayout.Foldout(_showGameStuff, "Game Stuff");
        if (_showGameStuff)
        {
            ScoreField();
            PlaybackButtons();
        }
    }

    private void ScoreField()
    {
        EditorGUILayout.BeginHorizontal();
        Label("Score");
        int scoreField = EditorGUILayout.IntField((int)Score.Value);
        if (GUILayout.Button("Update Score"))
        {
            _managerScript.UpdateScoreUI();
        }
        Score.SetValue(scoreField);
        EditorGUILayout.EndHorizontal();
    }

    private void EnemyRowAndColCount()
    {
        EditorGUILayout.BeginHorizontal();
        Label("Rows");
        int rows = EditorGUILayout.IntField((int)EnemyRows.Value);
        Score.SetValue(rows);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        Label("Columns");
        int columns = EditorGUILayout.IntField((int)EnemyRows.Value);
        Score.SetValue(columns);
        EditorGUILayout.EndHorizontal();
    }

    private void PlaybackButtons()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Pause Game"))
        {
            _managerScript.Pause();
        }
        if (GUILayout.Button("Resume Game"))
        {
            _managerScript.Resume();
        }
        GUILayout.EndHorizontal();
    }

    private void Label(string str)
    {
        EditorGUILayout.LabelField(str, GUILayout.Width(80));
    }
}
