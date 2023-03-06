using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] FloatVariable _rows;
    [SerializeField] FloatVariable _columns;
    [SerializeField] FloatVariable _moveSpeed;

    private float _tickTimer;
    private float _spawnRow;

    private GameObject[,] _enemies;
    private int[] _rowTypes;

    public BoolVariable LeftOrRight;
    public BoolVariable Down;
    public BoolVariable Shot;

    private int _columnBoundLeft;
    private int _columBoundRight;


    private float _maxBoundTop;
    private int _maxBoundBottom;


    [SerializeField] Camera _camera;
    [SerializeField] private GameObject[] _enemyTypes;

    [SerializeField] AnimationCurve _curve;

    void Start()
    {
        LeftOrRight.SetFalse();
        Down.SetFalse();
        Shot.SetFalse();
        _moveSpeed.SetValue(0.6f);

        _maxBoundTop = _camera.orthographicSize - 2;

        SetSpawnBounds();

        SpawnEnemies();

        StartCoroutine(ShootControlCrt());
        StartCoroutine(MoveEnemiesCrt());
    }

    int[] GenerateRowTypes()
    {
        int[] temp = new int[(int)_rows.Value];
        for (int i = 0; i < _rows.Value; i++)
        {
            temp[i] = i % 3;
        }
        Array.Sort(temp);
        Array.Reverse(temp);
        return temp;
    }

    private void SetSpawnBounds()
    {
        _columBoundRight = (int)(_columns.Value - 1) / 2;
        _columnBoundLeft = _columBoundRight * -1;

        _spawnRow = _maxBoundTop - _rows.Value + 1;
    }

    public void SpawnEnemies()
    {
        SetSpawnBounds();
        _enemies = new GameObject[(int)_rows.Value, (int)_columns.Value];
        _rowTypes = GenerateRowTypes();
        for (int y = 0; y < _rows.Value; y++)
        {
            int colNum = 0;
            for (int x = _columnBoundLeft; x < _columBoundRight + 1; x++)
            {
                GameObject enemy = Instantiate(_enemyTypes[_rowTypes[y]], new Vector3(x, (_spawnRow + y), 0), transform.rotation);
                _enemies[y, colNum] = enemy;
                enemy.transform.parent = transform;
                colNum++;
            }
        }
    }

    public void ResetEnemies()
    {
        StopAllCoroutines();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        SpawnEnemies();
        StartCoroutine(ShootControlCrt());
        StartCoroutine(MoveEnemiesCrt());

        LeftOrRight.SetFalse();
    }

    public void SetMoveSpeed()
    {
        float enemyCount = transform.childCount;
        float totalEnemies = _enemies.Length;
        float enemyPercentage = (enemyCount / totalEnemies);
        _moveSpeed.SetValue(_curve.Evaluate(enemyPercentage));
    }

    IEnumerator MoveEnemiesCrt()
    {
        while (true)
        {
            string direction = Down.Value ? "down" : LeftOrRight.Value ? "left" : "right";
            for (int r = 0; r < _enemies.GetLength(0); r++)
            {
                if (LeftOrRight.Value)
                {
                    for (int c = 0; c < _enemies.GetLength(1); c++)
                    {
                        if (_enemies[r, c] != null)
                        {
                            GameObject enemy = _enemies[r, c];
                            EnemyBehavior behavior = enemy.GetComponent<EnemyBehavior>();

                            behavior.Move(direction);
                            yield return new WaitForSeconds(_moveSpeed.Value / transform.childCount);
                        }
                    }
                }
                else
                {
                    for (int c = _enemies.GetLength(1) - 1; c >= 0; c--)
                    {
                        if (_enemies[r, c] != null)
                        {
                            GameObject enemy = _enemies[r, c];
                            EnemyBehavior behavior = enemy.GetComponent<EnemyBehavior>();

                            behavior.Move(direction);
                            yield return new WaitForSeconds(_moveSpeed.Value / transform.childCount);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(_moveSpeed.Value);
        }
    }

    IEnumerator ShootControlCrt()
    {
        while (true)
        {
            List<EnemyBehavior> validShooters = new();

            for (int r = 0; r < _enemies.GetLength(0); r++)
            {
                for (int c = 0; c < _enemies.GetLength(1); c++)
                {
                    if (_enemies[r, c] == null) continue;

                    GameObject enemy = _enemies[r, c];
                    EnemyBehavior behavior = enemy.GetComponent<EnemyBehavior>();

                    if (!behavior.EnemyInFront())
                    {
                        validShooters.Add(behavior);
                    }
                }
            }

            if (validShooters.Count > 0)
            {
                int index = Random.Range(0, validShooters.Count);
                EnemyBehavior bhv = validShooters[index];
                bhv.Shoot();
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
