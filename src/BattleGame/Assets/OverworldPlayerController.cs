using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OverworldPlayerController : MonoBehaviour
{
    bool isMoving = false;

    public Animator animator;

    public AudioSource hit;

    public string currentState;


    public enum PlayerFacing
    {
        Up,
        Down,
        Left,
        Right
    }

    public PlayerFacing direction;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        hit = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            direction = PlayerFacing.Up;
            if (!isMoving)
            {
                direction = PlayerFacing.Up;
                StartCoroutine(MoveToTile((Vector2)transform.position + new Vector2(0, 1)));
            }
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (!isMoving)
            {
                direction = PlayerFacing.Left;
                StartCoroutine(MoveToTile((Vector2)transform.position + new Vector2(-1, 0)));
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (!isMoving)
            {
                direction = PlayerFacing.Down;
                StartCoroutine(MoveToTile((Vector2)transform.position + new Vector2(0, -1)));
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (!isMoving)
            {
                direction = PlayerFacing.Right;
                StartCoroutine(MoveToTile((Vector2)transform.position + new Vector2(1, 0)));
            }
        }
        if(isMoving)
        {
            ChangeAnimationState("walk_" + direction.ToString().ToLower());
        } else
        {
            ChangeAnimationState("stand_" + direction.ToString().ToLower());
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
                hit.Play();
                yield break;
                
            }
        }
        
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

        //check if they are on a space with a mapObject
        MapTransition[] mapTransitionObjects = GameObject.FindObjectsOfType<MapTransition>();
        foreach (MapTransition mtObject in mapTransitionObjects)
        {
            if (transform.position == mtObject.transform.position)
            {
                OverworldManager.instance.ChangeMap(mtObject.MapName, mtObject.moveLocation);
            }
        }

        yield break;
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
