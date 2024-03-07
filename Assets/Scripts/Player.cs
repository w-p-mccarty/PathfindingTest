/*
* Player.cs
* By: William McCarty
* Player Object that travels the A* path
* */
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// NavGrid to determine path
    /// </summary>
    [SerializeField]
    private NavGrid _grid;
    /// <summary>
    /// Speed the player will move along the path
    /// </summary>
    [SerializeField]
    private float _speed = 10.0f;
    /// <summary>
    /// Points to generate smooth path between NavGridNodes
    /// </summary>
    [SerializeField]
    private int _smoothSegments = 50;
    
    /// <summary>
    /// Turns on Player path in Editor
    /// </summary>
    [Space(10)]
    [Header("Debug")]
    [SerializeField]
    private bool _debug;
    
    /// <summary>
    /// Path for the Player to travel along for smooth movement
    /// </summary>
    private List<Vector3> _smoothPathToEnd;
    /// <summary>
    /// Generated NavGridPathNodes from the A* algorithm 
    /// </summary>
    private NavGridPathNode[] _generatedPath = Array.Empty<NavGridPathNode>();
    /// <summary>
    /// Current Vector3 the user is passing along the smooth path
    /// </summary>
    private int _currentPathIndex;
    
    /// <summary>
    /// Unity Update loop
    /// </summary>
    private void Update()
    {
        // Check Input
        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                _generatedPath = _grid.GetPath(transform.position, hitInfo.point);
                //We only want to do path finding if there is more than 1 node
                if (_generatedPath.Length > 1)
                {
                    _currentPathIndex = 0;
                    //Generates the smooth points from the generated path for the player to traverse
                    GenerateSmoothPath();
                }

            }
        }

        //if index is greater than the points in the list then the player has reached the end
        if (_smoothPathToEnd == null || _currentPathIndex >= _smoothPathToEnd.Count) return;
        
        // Calculate the target position based on current point index
        Vector3 targetPosition = _smoothPathToEnd[_currentPathIndex];

        // Move the object towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * _speed);

        // If the object reaches the target position, move to the next point
        if (transform.position == targetPosition)
        {
            _currentPathIndex++;
        }
    }
          
    /// <summary>
    /// Creates Smooth path of Vector3 points for the player to traverse on.
    /// </summary>
    private void GenerateSmoothPath()
    {
        _smoothPathToEnd = new List<Vector3>();
        //Go through every NavGridPathNode in our A* result
        for (var i = 0; i < _generatedPath.Length - 1; i++)
        {
            Vector3 p0 = i == 0 ? _generatedPath[i].Position : _generatedPath[i - 1].Position;
            Vector3 p1 = _generatedPath[i].Position;
            Vector3 p2 = _generatedPath[i + 1].Position;
            Vector3 p3 = i == _generatedPath.Length - 2 ? _generatedPath[i + 1].Position : _generatedPath[i + 2].Position;

            //Interpolate segment to generate curve between NavGridNodes 
            for (var j = 0; j < _smoothSegments; j++)
            {
                float t = (float)j / _smoothSegments;
                Vector3 point = Interpolate(p0, p1, p2, p3, t);
                //We only want to move along X and Z axis
                _smoothPathToEnd.Add(new Vector3(point.x, this.transform.position.y, point.z));
            }
        }
    }
    
    /// <summary>
    /// Interpolates points using Catmull Rom Equation
    /// </summary>
    /// <param name="p0">Starting point to interpolate from.</param>
    /// <param name="p1">Control point 1.</param>
    /// <param name="p2">Control point 2.</param>
    /// <param name="p3">End point to interpolate to.</param>
    /// <returns>Interpolated Vector3.</returns>
    private Vector3 Interpolate(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float t2 = t * t;
        float t3 = t * t2;

        Vector3 point =
            0.5f * ((2.0f * p1) +
                    (-p0 + p2) * t +
                    (2.0f * p0 - 5.0f * p1 + 4.0f * p2 - p3) * t2 +
                    (-p0 + 3.0f * p1 - 3.0f * p2 + p3) * t3);

        return point;
    }
#if UNITY_EDITOR
    /// <summary>
    /// Unity draw gizmos
    /// </summary>
    private void OnDrawGizmos()
    {
        if (!_debug || _smoothPathToEnd == null || _smoothPathToEnd.Count <= 0) return;
        
        //Draw all points on the path the player will go through
        Gizmos.color = Color.red;
        for (var i = 0; i < _smoothPathToEnd.Count - 1; i++)
        {
            Gizmos.DrawSphere(new Vector3(_smoothPathToEnd[i].x, 2,_smoothPathToEnd[i].z) , .1f);
        }
    }
    #endif
}

    

