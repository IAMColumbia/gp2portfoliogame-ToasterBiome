using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OverworldObject : MonoBehaviour, IOverworldObject
{
    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnInteract()
    {
        //does nothing in here, teehee!
    }

    public virtual IEnumerator MoveToTile(Vector2 position)
    {
        //check if we can move there
        Tilemap map = OverworldManager.instance.tilemap;

        TileBase tile = map.GetTile(new Vector3Int((int)position.x - 1, (int)position.y - 1, 0));

        if (tile.GetType() == typeof(OverworldTile) || tile.GetType().BaseType == typeof(OverworldTile))
        {
            OverworldTile oTile = (OverworldTile)tile;
            if (!oTile.getPassable())
            {
                //maybe play that pokemon OOMPH sound when you walk into something

                yield break;

            }
        }
        Vector2 direction = (position - (Vector2)transform.position).normalized;
        //check if they are on a space with a mapObject
        OverworldObject[] overworldObjects = GameObject.FindObjectsOfType<OverworldObject>();
        foreach (OverworldObject oObject in overworldObjects)
        {
            if ((Vector2)transform.position + direction == (Vector2)oObject.transform.position)
            {
                //cant walk here

                yield break;
            }
        }

        isMoving = true;
        
        while (Vector2.Distance(transform.position, position) > 0.1f)
        {
            transform.position = (Vector2)transform.position + direction * 4f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }


        transform.position = position;
        isMoving = false;

        yield break;
    }

    
}
