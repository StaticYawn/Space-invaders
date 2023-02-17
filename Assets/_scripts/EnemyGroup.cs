using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] int rows;
    [SerializeField] int columns;

    [SerializeField] float moveTick;
    private float tickTimer;
    private int spawnRow;

    private GameObject[,] enemies;
    private int[] rowTypes;

    public BoolVariable leftOrRight;
    public BoolVariable down;
    public BoolVariable shot;

    private int columnBoundLeft;
    private int columBoundRight;


    private int maxBoundTop = 6;
    private int maxBoundBottom;

    static Random rnd = new Random();


    [SerializeField] private GameObject[] enemyTypes;

    void Start()
    {
        leftOrRight.SetFalse();
        down.SetFalse();
        shot.SetFalse();

        enemies = new GameObject[rows, columns];
        columBoundRight = (columns - 1) / 2;
        columnBoundLeft = columBoundRight * -1;

        spawnRow = maxBoundTop - rows + 1;

        spawnEnemies();
    }

    void Update()
    {
        if (tickTimer > moveTick)
        {
            string direction = down.Value ? "down" : leftOrRight.Value ? "left" : "right";
            MoveEnemies(direction);
            tickTimer = 0;
        }
        else
        {
            tickTimer += Time.deltaTime;
        }

        if (!shot.Value)
        {
            ShotControl();
            shot.SetTrue();
        }
    }

    int[] GenerateRowTypes()
    {
        int[] temp = new int[rows];
        for(int i = 0; i < rows; i++)
        {
            temp[i] = i % 3;
        }
        Array.Sort(temp);
        Array.Reverse(temp);
        return temp;
    }

    public void spawnEnemies()
    {
        rowTypes = GenerateRowTypes();
        for (int y = 0; y < rows; y++)
        {
            int colNum = 0;
            for (int x = columnBoundLeft; x < columBoundRight + 1; x++)
            {
                GameObject enemy = Instantiate(enemyTypes[rowTypes[y]], new Vector3(x, (spawnRow + y), 0), transform.rotation);
                enemies[y, colNum] = enemy;
                enemy.transform.parent = transform;
                colNum++;
            }
        }
    }

    private void MoveEnemies(string s)
    {
        for (int r = 0; r < enemies.GetLength(0); r++)
        {
            for (int c = 0; c < enemies.GetLength(1); c++)
            {
                if (enemies[r, c] == null) continue;
                GameObject enemy = enemies[r, c];
                EnemyBehavior behavior = enemy.GetComponent<EnemyBehavior>();

                behavior.Move(s);
            }
        }
    }

    private void ShotControl()
    {
        List<GameObject> validShooters = new();

        for (int r = 0; r < enemies.GetLength(0); r++)
        {
            for (int c = 0; c < enemies.GetLength(1); c++)
            {
                if (enemies[r, c] == null) continue;
                GameObject enemy = enemies[r, c];
                EnemyBehavior behavior = enemy.GetComponent<EnemyBehavior>();
                if (!behavior.EnemyInFront())
                {
                    validShooters.Add(enemy);
                }
            }
        }

        int index = rnd.Next(validShooters.Count);
        EnemyBehavior bhv = validShooters[index].GetComponent<EnemyBehavior>();
        bhv.Shoot();
    }
}
