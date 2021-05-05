﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OverworldPlayerController : OverworldObject
{

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

        //check for interactions
        if(Input.GetKeyDown(KeyCode.Z) && !isMoving)
        {
            Debug.Log("Checking");
            Vector2 location = transform.position;

            switch (direction)
            {
                case PlayerFacing.Up:
                    location += Vector2.up;
                    break;
                case PlayerFacing.Down:
                    location += Vector2.down;
                    break;
                case PlayerFacing.Left:
                    location += Vector2.left;
                    break;
                case PlayerFacing.Right:
                    location += Vector2.right;
                    break;
            }

            OverworldSimpleDialogue[] dialogueObjects = GameObject.FindObjectsOfType<OverworldSimpleDialogue>();
            foreach (OverworldSimpleDialogue dObject in dialogueObjects)
            {
                if (location == (Vector2)dObject.transform.position)
                {
                    if(!dObject.currentlyTalking)
                    {
                        dObject.currentlyTalking = true;
                        TextboxController.instance.SendTexts(dObject);
                    }
                }
            }
        }
    }

    public override IEnumerator MoveToTile(Vector2 position)
    {
        yield return base.MoveToTile(position);
        //check if they are on a space with a battleStart
        OverworldBattleStart[] battleStartObjects = GameObject.FindObjectsOfType<OverworldBattleStart>();
        foreach (OverworldBattleStart bsObject in battleStartObjects)
        {
            if (transform.position == bsObject.transform.position)
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
