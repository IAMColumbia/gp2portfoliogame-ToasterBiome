using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OverworldPlayerController : MonoBehaviour
{
    bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            if(!isMoving) StartCoroutine(MoveToTile((Vector2)transform.position + new Vector2(0, 1)));
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (!isMoving) StartCoroutine(MoveToTile((Vector2)transform.position + new Vector2(-1, 0)));
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (!isMoving) StartCoroutine(MoveToTile((Vector2)transform.position + new Vector2(0, -1)));
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (!isMoving) StartCoroutine(MoveToTile((Vector2)transform.position + new Vector2(1, 0)));
        }
    }

    private IEnumerator MoveToTile(Vector2 position)
    {
        //check if we can move there
        Tilemap map = OverworldManager.instance.tilemap;

        TileBase tile = map.GetTile(new Vector3Int((int)position.x - 1, (int)position.y - 1, 0));

        if(tile.GetType() == typeof(OverworldTile))
        {
            OverworldTile oTile = (OverworldTile)tile;
            if(!oTile.passable)
            {
                //maybe play that pokemon OOMPH sound when you walk into something
                yield break;
                
            }
        }
        //ChangeAnimationState("Move");
        isMoving = true;
        while (Vector2.Distance(transform.position, position) > 0.01f)
        {
            transform.position = (Vector2)transform.position + (position - (Vector2)transform.position).normalized * 4f * Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }


        transform.position = position;
        isMoving = false;

        //check if they are on a space with a battleStart
        OverworldBattleStart[] battleStartObjects = GameObject.FindObjectsOfType<OverworldBattleStart>();
        foreach(OverworldBattleStart bsObject in battleStartObjects)
        {
            if(transform.position == bsObject.transform.position)
            {
                OverworldManager.instance.StartBattle(bsObject.pool);
            }
        }
        //ChangeAnimationState("Idle");
        yield break;
    }
}
