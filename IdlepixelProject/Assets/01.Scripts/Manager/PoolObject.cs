using System;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public string poolTag;
    public virtual void init()
    {
        gameObject.SetActive(true);
    }
}
