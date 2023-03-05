using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSetup : MonoBehaviour
{
    [SerializeField] Texture2D _barrierSprite;
    [SerializeField] GameObject _barrierPixel;

    int _width;
    int _height;

    float _pixelSize = 0.0625f;

    int _widthHalf;
    void Start()
    {
        _width = _barrierSprite.width;
        _widthHalf = _width / 2;
        _height = _barrierSprite.height;

        SetupBarrier();
    }

    private void SetupBarrier()
    {
        for(int x = 0; x < _width; x++)
        {
            for(int y = 0; y < _height; y++)
            {
                Color pixelColor = _barrierSprite.GetPixel((int)x, y);
                if (pixelColor.a < 1) continue;
                GameObject pixel = Instantiate(_barrierPixel, transform);

                Vector2 pos = new Vector2(transform.position.x + (x - _widthHalf) * _pixelSize, transform.position.y + y * _pixelSize);
                pixel.transform.position = pos;
            }
        }
    }

    void DestroyBarrier()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ResetBarrier()
    {
        DestroyBarrier();
        SetupBarrier();
    }
}
