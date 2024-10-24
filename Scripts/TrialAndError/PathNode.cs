using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathNode
{
    public Grid.Tile tile;
    public float gCost;
    public float hCost;


    public PathNode cameFromNode;
    public PathNode(Grid.Tile t) 
    {
        gCost = 0;
        hCost = 0;
        tile = t;

    }

   public float f() => gCost + hCost;
    

    public override string ToString()
    {
        return tile.x + ":" + tile.y;
    }

    public int GetX() { return tile.x; }
    public int GetY() { return tile.y; }
}
