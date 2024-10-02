using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public Vector3Int Position;
    public bool IsOccupied;
    public GameObject Building;

    public GridCell(Vector3Int position)
    {
        Position = position;
        IsOccupied = false;
        Building = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
