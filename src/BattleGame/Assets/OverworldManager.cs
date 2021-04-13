using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class OverworldManager : MonoBehaviour
{
    public static OverworldManager instance;
    public Tilemap tilemap;
    public BattlePool nextBattlePool;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("A OverworldManager already exists");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.FindObjectOfType<Tilemap>();
        DontDestroyOnLoad(transform.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            StartBattle();
        }
    }

    public void StartBattle()
    {
        SceneManager.LoadScene("BattleScene");

    }

    public void StartBattle(BattlePool pool)
    {
        SceneManager.LoadScene("BattleScene");
        nextBattlePool = pool;
    }

    public void EndBattle()
    {
        SceneManager.LoadScene("OverworldScene");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BattleScene") BattleManager.instance.StartBattle(nextBattlePool);
        if (scene.name == "OverworldScene")
        {
            tilemap = GameObject.FindObjectOfType<Tilemap>();
        }

    }
}
