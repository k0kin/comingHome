using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class AStarUpdate : MonoBehaviour
{
    [SerializeField] private GameObject walls;
    [SerializeField] private GridGraph graph;
    
    void Start()
    {
        graph = AstarPath.active.data.gridGraph;

        graph.center = walls.transform.position;
        
        AstarPath.active.Scan();
    }

    // Update is called once per frame
    void Update()
    {
        graph = AstarPath.active.data.gridGraph;

        graph.center = walls.transform.position;
        
        AstarPath.active.Scan();
    }
}
