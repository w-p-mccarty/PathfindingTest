/*
* NavGridPathNode.cs
* By: William McCarty
* Node of Nav Grid
* */
using System;
using UnityEngine;

public struct NavGridPathNode
{
    /// <summary>
    /// Enum of the different of nodes possible
    /// </summary>
    public enum NodeType
    {
        Start,
        End,
        Blocked,
        Walkable
    };
    /// <summary>
    /// World position of the node
    /// </summary>
    public Vector3 Position;
    /// <summary>
    /// The type of node this is
    /// </summary>
    public NodeType Type;
    /// <summary>
    /// Where on the entire NavGrid this Node is
    /// </summary>
    public Vector2 Coord;
    /// <summary>
    /// What node coord is its parent
    /// </summary>
    public Vector2 ParentCoord;
    /// <summary>
    /// Cost of node to reach end point using A*
    /// </summary>
    public int TotalCost;
    public int DistToStart;
    public int Heuristic;
    
    
    /// <summary>
    /// Constructs node with the world position and NavGrid position
    /// </summary>
    /// <param name="position">World position of where the center of this node is.</param>
    /// <param name="coord">The coordinate of where this node is in the NavGrid.</param>
    public NavGridPathNode(Vector3 position, Vector2 coord)
    {
        this.Position = position;
        this.Coord = coord;
        this.Type = NodeType.Walkable;
        this.ParentCoord = new Vector2(int.MaxValue, int.MaxValue);
        this.TotalCost = 0;
        this.DistToStart = 0;
        this.Heuristic = 0;
    }
    
    /// <summary>
    /// Resets data associated with pathfinding
    /// </summary>
    public void Reset()
    {
        //We assume everything is walkable until processed by pathfinding location
        this.Type = NodeType.Walkable;
        this.ParentCoord = new Vector2(int.MaxValue, int.MaxValue);
        this.TotalCost = 0;
        this.DistToStart = 0;
        this.Heuristic = 0;
    }

    /// <summary>
    /// Helper functions to convert the Vector2 X coordinate to int
    /// </summary>
    public int GetXCoord()
    {
        return Convert.ToInt32(Coord.x);
    }
    
    /// <summary>
    /// Helper functions to convert the Vector2 Y coordinate to int
    /// </summary>
    public int GetYCoord()
    {
        return Convert.ToInt32(Coord.y);
    }
}