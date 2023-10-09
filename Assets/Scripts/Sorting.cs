using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Rendering;

public class Sorting : MonoBehaviour
{
    public SortingGroup sorter;

    public void Start()
    {
        sorter = GetComponent<SortingGroup>();
    }
    void Update()
    {
        sorter.sortingOrder = Mathf.RoundToInt(transform.position.y * -25f);
    }
}
