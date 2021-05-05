using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OverworldObject : MonoBehaviour
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

    public virtual IEnumerator MoveToTile(Vector2 position)
    {
        //check if we can move there
        Tilemap map = OverworldManager.instance.tilemap;

        TileBase tile = map.GetTile(new Vector3Int((int)position.x - 1, (int)position.y - 1, 0));

        if (tile.GetType() == typeof(OverworldTile))
        {
            OverworldTile oTile = (OverworldTile)tile;
            if (!oTile.passable)
            {
                //maybe play that pokemon OOMPH sound when you walk into something

                yield break;

            }
        }

        isMoving = true;
        Vector2 direction = (position - (Vector2)transform.position).normalized;
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
