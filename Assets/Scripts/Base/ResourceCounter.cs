using System;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    public event Action<int> ResourceCountChange;

    public int Count {  get; private set; }

    public void Add()
    {
        Count++;
        ResourceCountChange?.Invoke(Count);
    }
}