using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{
    private const int Move_Straight_Cost = 10;
    private const int Move_Diagonal_Cost = 14;
    private Grid grid;
    private Dictionary<Grid.Tile, PathNode> pathNodeMap;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    private void Start()
    {
        grid = Grid.Instance;
        if (grid == null)
        {
            Debug.LogError("Grid instance is null in pathfinding");
            return;
        }
        InitializePathNodeMap();

    }
    private void InitializePathNodeMap()
    {
        pathNodeMap = new Dictionary<Grid.Tile, PathNode>();

        foreach (Grid.Tile tile in grid.GetTiles())
        {
            pathNodeMap[tile] = new PathNode(tile);
        }
    }
    public Pathfinding(int width, int height)
    {
        grid = Grid.Instance;

        pathNodeMap = new Dictionary<Grid.Tile, PathNode>();

        foreach (Grid.Tile tile in grid.GetTiles())
        {
            pathNodeMap[tile] = new PathNode(tile);
        }
    }

    public List<PathNode> FindPath(Grid.Tile startTile, Grid.Tile endTile)
    {
        PathNode startNode = new PathNode(startTile);
        PathNode endNode = new PathNode(endTile);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode.tile == endNode.tile)
            {
                return CalculatePath(currentNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            List<Grid.Tile> Neighbors = new List<Grid.Tile>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;
                    Grid.Tile tile = new Grid.Tile();
                    tile.x = currentNode.tile.x - x;
                    tile.y = currentNode.tile.y - y;
                    Neighbors.Add(Grid.Instance.TryGetTile(new Vector2Int(tile.x, tile.y)));
                    
                }
            }
                
            foreach (Grid.Tile neighborNode in Neighbors)
            {
                PathNode node = new PathNode(neighborNode);
                node.cameFromNode = currentNode;
                foreach (var silly in closedList.Where(n => n.tile == node.tile)) goto Return;

                if (node.tile.occupied)
                {
                    node.gCost = 9999; // real g
                }
                else 
                {
                    node.gCost = currentNode.gCost + 1;
                }

                 node.hCost = Mathf.Pow(node.tile.x - endTile.x, 2) + Mathf.Pow(node.tile.y - endTile.y, 2);
                
                foreach (PathNode n in openList)
                {
                    if (Grid.Instance.IsSameTile(node.tile, n.tile))
                    {
                        if (node.gCost > n.gCost)
                        {
                            goto Return;
                        }

                    }

                }

                openList.Add(node);
                

            Return: continue;
            }
        
        }

        //out of nodes on openlist
        return null;
    }

    public PathNode GetNode(int x, int y)
    {
        Grid.Tile tile = grid.TryGetTile(new Vector2Int(x, y));
        if (tile != null && pathNodeMap.ContainsKey(tile))
        {
            return pathNodeMap[tile];
        }
        return null;
    }

    public List<Grid.Tile>GetTilePath(List<PathNode>L) 
    {
        
        List<PathNode> List = new List<PathNode>();
        List<Grid.Tile> tiles = new List<Grid.Tile>();
        List = L;
        foreach (var Node in List) 
        {
            if (Node == null) continue;
            Grid.Tile tile = new Grid.Tile();
            tile.x = Node.tile.x;
            tile.y = Node.tile.y;
            tiles.Add(tile);

        }
        return tiles;
    }
    public List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        while (endNode.cameFromNode != null)
        { 
            path.Add(endNode);
            endNode = endNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.tile.x - b.tile.x);
        int yDistance = Mathf.Abs(a.tile.y - b.tile.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return Move_Diagonal_Cost * Mathf.Min(xDistance, yDistance) + Move_Straight_Cost * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].f() < lowestFCostNode.f())
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

    internal List<PathNode> FindPath(Grid.Tile tile, Vector3 awayFromZombie)
    {
        throw new NotImplementedException();
    }
}
