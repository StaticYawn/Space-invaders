using UnityEditor;
using UnityEngine;

public class GameManagerWindow : EditorWindow
{
    [SerializeField] FloatVariable _playerMoveSpeed;
    [SerializeField] FloatVariable _playerShotSpeed;
    public FloatVariable EnemyRows;
    public FloatVariable EnemyColumns;
    public FloatVariable Score;

    GameManager _managerScript;

    bool _showEnemyStuff = false;
    bool _showPlayerStuff = false;
    bool _showGameStuff = false;

    private static GameManagerWindow _window;
    private static bool _isOpen;

    [MenuItem("Window/GameManager %F6")]
    private static void Init()
    {
        _window = (GameManagerWindow)GetWindow(typeof(GameManagerWindow), false, "GameManager");
        if (_isOpen)
        {
            _window.Close();
            _isOpen = false;
        } else
        {
            _window.Show();
            _isOpen = true;
        }
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
            PlayerShotAndMoveSpeed();
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

    private void PlayerShotAndMoveSpeed()
    {
        EditorGUILayout.BeginHorizontal();
        Label("Shot speed");
        int shotSpeed = EditorGUILayout.IntField((int)_playerShotSpeed.Value);
        _playerShotSpeed.SetValue(shotSpeed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        Label("Move speed");
        int moveSpeed = EditorGUILayout.IntField((int)_playerMoveSpeed.Value);
        _playerMoveSpeed.SetValue(moveSpeed);
        EditorGUILayout.EndHorizontal();
    }

    private void EnemyRowAndColCount()
    {
        EditorGUILayout.BeginHorizontal();
        Label("Rows");
        int rows = EditorGUILayout.IntField((int)EnemyRows.Value);
        EnemyRows.SetValue(rows);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        Label("Columns");
        int columns = EditorGUILayout.IntField((int)EnemyColumns.Value);
        EnemyColumns.SetValue(columns);
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
