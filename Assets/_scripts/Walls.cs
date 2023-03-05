using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    [SerializeField] Camera cam;

    enum Direction {
        top,
        bottom,
        left,
        right
    }

    [SerializeField] Direction direction;
    
    float _height;
    float _width;

    BoxCollider2D _wall;

    private void Awake()
    {
        _wall = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        _height = cam.orthographicSize;
        _width = _height * cam.aspect;

        SetSize();
        SetPosition();
    }

    private void SetSize()
    {
        if(direction == Direction.top || direction == Direction.bottom)
        {
            _wall.size = new Vector2(_width * 2, 2);
        }
        else
        {
            _wall.size = new Vector2(2, _height * 2);
        }
    }

    private void SetPosition()
    {
        if(direction == Direction.top)
        {
            _wall.transform.position = new Vector2(0, _height - 0.5f);
        } 
        else if(direction == Direction.bottom)
        {
            _wall.transform.position = new Vector2(0, (_height * -1) + 0.5f);
        }
        else if(direction == Direction.right)
        {
            _wall.transform.position = new Vector2(_width, 0);
        }
        else
        {
            _wall.transform.position = new Vector2((_width * -1), 0);
        }
    }
}
