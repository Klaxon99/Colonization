using System;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    [SerializeField] private int _unitCost;
    [SerializeField] private int _baseCost;

    public event Action<int> ResourceCountChange;
    public event Action AccumulatedUnitCost;
    public event Action AccumulatedBaseCost;

    private int _minCount = 0;
    private int _count = 0;

    public int UnitCost => _unitCost;
    public int BaseCost => _baseCost;

    public int Count
    {
        get
        {
            return _count;
        }

        private set
        {
            _count = Mathf.Max(_minCount, value);
            ResourceCountChange?.Invoke(_count);
        }
    }

    public void Add()
    {
        Count++;

        if (Count == _unitCost)
        {
            AccumulatedUnitCost?.Invoke();
        }

        if (Count == _baseCost)
        {
            AccumulatedBaseCost?.Invoke();
        }
    }

    public void BuyUnit()
    {
        TryBuy(_unitCost);
    }

    public void BuyBase()
    {
        TryBuy(_baseCost);
    }

    private bool TryBuy(int cost)
    {
        if (Count >= cost)
        {
            Count -= cost;

            return true;
        }

        return false;
    }
}