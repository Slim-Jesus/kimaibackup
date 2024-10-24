using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Kim : CharacterController
{
    [SerializeField] float ContextRadius = 10f;
    private Pathfinding pathfinding;
    public override void StartCharacter()
    {
        base.StartCharacter();
        pathfinding = GetComponent<Pathfinding>();  // reminder: attach pathfinding to Kim prefab not just in editor
    }

    public override void UpdateCharacter()
    {
        base.UpdateCharacter();

        SetWalkBuffer(pathfinding.GetTilePath(pathfinding.FindPath(Grid.Instance.GetClosest(transform.position), Grid.Instance.GetFinishTile())));
    }

    Vector3 GetEndPoint()
    {
        GameObject finishTile = GameObject.FindWithTag("Finish");
        if (finishTile != null)
        {
            return finishTile.transform.position;
        }
        else 
        {
            Debug.LogError("FinishTile is not assigned");
            return Vector3.zero;
        }
    }

    GameObject[] GetContextByTag(string aTag)
    {
        Collider[] context = Physics.OverlapSphere(transform.position, ContextRadius);
        List<GameObject> returnContext = new List<GameObject>();
        foreach (Collider c in context)
        {
            if (c.transform.CompareTag(aTag))
            {
                returnContext.Add(c.gameObject);
            }
        }
        return returnContext.ToArray();
    }

    GameObject GetClosest(GameObject[] aContext)
    {
        float dist = float.MaxValue;
        GameObject Closest = null;
        foreach (GameObject z in aContext)
        {
            float curDist = Vector3.Distance(transform.position, z.transform.position);
            if (curDist < dist)
            {
                dist = curDist;
                Closest = z;
            }
        }
        return Closest;
    }
}
