using UnityEngine;

[CreateAssetMenu]
public class BoolVariable : ScriptableObject
{
    public bool Value;

    public void SetFalse()
    {
        Value = false;
    }

    public void SetTrue()
    {
        Value = true;
    }
}
