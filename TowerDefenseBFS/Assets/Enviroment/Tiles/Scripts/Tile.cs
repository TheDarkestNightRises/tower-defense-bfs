using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }
    [SerializeField] Tower towerPrefab;

    GridManager gridManager;
    Path_Finder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {   
        pathfinder = FindObjectOfType<Path_Finder>(); 
        gridManager = FindObjectOfType<GridManager>();
    }

    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if(!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {   
            bool isSuccesfull = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (isSuccesfull)
            {
                pathfinder.NotifyReceivers();
                gridManager.BlockNode(coordinates);
            }
        }
    }
}
