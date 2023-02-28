using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] int _rows;
    [SerializeField] int _columns;

    [SerializeField] float _moveTick;
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

    private Random _rnd = new Random();

    [SerializeField] Camera _camera;
    [SerializeField] private GameObject[] _enemyTypes;

    void Start()
    {
        LeftOrRight.SetFalse();
        Down.SetFalse();
        Shot.SetFalse();

        _maxBoundTop = _camera.orthographicSize - 1;
        _enemies = new GameObject[_rows, _columns];

        _columBoundRight = (_columns - 1) / 2;
        _columnBoundLeft = _columBoundRight * -1;

        _spawnRow = _maxBoundTop - _rows + 1;

        spawnEnemies();
    }

    void Update()
    {
        if (_tickTimer > _moveTick)
        {
            string direction = Down.Value ? "down" : LeftOrRight.Value ? "left" : "right";
            MoveEnemies(direction);
            _tickTimer = 0;
        }
        else
        {
            _tickTimer += Time.deltaTime;
        }

        if (!Shot.Value)
        {
            ShotControl();
            Shot.SetTrue();
        }
    }

    int[] GenerateRowTypes()
    {
        int[] temp = new int[_rows];
        for(int i = 0; i < _rows; i++)
        {
            temp[i] = i % 3;
        }
        Array.Sort(temp);
        Array.Reverse(temp);
        return temp;
    }

    public void spawnEnemies()
    {
        _rowTypes = GenerateRowTypes();
        for (int y = 0; y < _rows; y++)
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

    private void MoveEnemies(string s)
    {
        for (int r = 0; r < _enemies.GetLength(0); r++)
        {
            for (int c = 0; c < _enemies.GetLength(1); c++)
            {
                if (_enemies[r, c] == null) continue;
                GameObject enemy = _enemies[r, c];
                EnemyBehavior behavior = enemy.GetComponent<EnemyBehavior>();

                behavior.Move(s);
            }
        }
    }

    private void ShotControl()
    {
        List<GameObject> validShooters = new();

        for (int r = 0; r < _enemies.GetLength(0); r++)
        {
            for (int c = 0; c < _enemies.GetLength(1); c++)
            {
                if (_enemies[r, c] == null) continue;

                GameObject enemy = _enemies[r, c];
                EnemyBehavior behavior = enemy.GetComponent<EnemyBehavior>();

                if (!behavior.EnemyInFront())
                {
                    validShooters.Add(enemy);
                }
            }
        }

        if (validShooters.Count <= 0) return;

        int index = _rnd.Next(validShooters.Count);
        EnemyBehavior bhv = validShooters[index].GetComponent<EnemyBehavior>();
        bhv.Shoot();
    }
}
