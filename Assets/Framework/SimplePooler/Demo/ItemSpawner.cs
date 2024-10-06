using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : PoolerBase<Item>
{
    [SerializeField] private Item _itemPrefab;

    private void Start()
    {
        InitPool(_itemPrefab); // Initialize the pool

        var shape = Get(); // Pull from the pool
        Release(shape); // Release back to the pool
    }

    // Optionally override the setup components
    protected override void GetSetup(Item shape)
    {
        base.GetSetup(shape);
        shape.transform.position = Vector3.zero;
    }
}