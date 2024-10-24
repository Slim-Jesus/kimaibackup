//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BehaviorTree : MonoBehaviour
//{
//    public Transform[] burgerLocations;
//    public Transform exitLocation;
//    public Transform[] zombieLocations;
//    public float detectionRange = 5f;

//    private Transform currentTarget;
//    private bool foundAllBurgers = false;
//    private Pathfinding pathfinding;
//    private List<Transform> collectBurgers = new List<Transform>();

//    void Start()
//    {
//        pathfinding = GetComponent<Pathfinding>();
//    }

//    void Update()
//    {
//        RunBehaviorTree();
//    }

//    void RunBehaviorTree()
//    {
//        if (!AvoidZombies())
//        {
//            if (!foundAllBurgers)
//            {
//                FindBurgers();
//            }
//            else
//            {
//                FindExit();
//            }
//        }
//    }

//    //Task Avoid Zambos
//    bool AvoidZombies()
//    {
//        foreach (Transform zombie in zombieLocations)
//        {
//            float distance = Vector3.Distance(transform.position, zombie.position);
//            if (distance < detectionRange)
//            {
//                //avoid zombie by moving away
//                Vector3 fleeDirection = (transform.position - zombie.position).normalized;
//                Vector3 safePosition = transform.position + fleeDirection * 2f; //move 2 units away
//                pathfinding.MoveTo(safePosition);
//            }
//        }
//    }

//}

//    Zombie closestZombie = GetClosest(GetContextByTag("Zombie"))?.GetComponent<Zombie>();

//    if (closestZombie != null)
//    {
//        float distanceToZombie = Vector3.Distance(transform.position, closestZombie.transform.position);

//        if (distanceToZombie > safeDistance)
//        {
//            currentPath.Clear();
//            return;
//        }
//    }
//}
//if (isFleeing)
//{
//    if (Vector3.Distance(transform.position, fleeTargetPosition) < 8f)
//    {
//        isFleeing = true;

//        SetWalkBuffer(pathfinding.GetTilePath(pathfinding.FindPath(Grid.Instance.GetClosest(transform.position), Grid.Instance.GetFinishTile())));
//    }
//    else 
//    {
//        SetWalkBuffer(currentPath);
//    }
//}
//else 
//{
//    Zombie closestZombie = GetClosest(GetContextByTag("Zombie"))?.GetComponent<Zombie>();
//    if (closestZombie != null)
//    {
//        //FleeFromZombie(closestZombie);

//    }
//}


//void FleeFromZombie(Zombie zombie)
//{
//    //isFleeing = true;
//    Vector3 fleeDirection = (transform.position - zombie.transform.position).normalized;
//    fleeTargetPosition = transform.position + fleeDirection * 2f; //find a safe spot 2 units away

//    Grid.Tile startTile = Grid.Instance.GetClosest(transform.position);
//    Grid.Tile endTile = Grid.Instance.GetClosest(fleeTargetPosition);

//    List<PathNode> path = pathfinding.FindPath(startTile, endTile);

//    if (path != null && path.Count > 0)
//    {
//        Debug.Log("Path found to get the hell away! " + string.Join(", ", path.Select(node => node.tile.ToString())));
//        currentPath = pathfinding.GetTilePath(path);
//        currentPathIndex = 0;

//    }
//    else
//    {
//        isFleeing = false;
//    }
//}

