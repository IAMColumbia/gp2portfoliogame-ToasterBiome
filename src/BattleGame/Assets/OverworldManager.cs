using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class OverworldManager : MonoBehaviour
{
    public static OverworldManager instance;
    public Tilemap tilemap;
    public BattlePool nextBattlePool;

    public GameObject currentMapObject;

    public GameObject player;

    public Image fadeImage;
    public float transitionSpeed = 2f;

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

    public void SwitchMap(string mapName, Vector2Int playerLocation)
    {
        GameObject newMap = Resources.Load<GameObject>("Maps/" + mapName);
        if(newMap)
        {
            Destroy(currentMapObject);
            currentMapObject = Instantiate(newMap);
            Debug.Log("Changed Map");
            //move the player
            player.transform.position = (Vector2)playerLocation;
            tilemap = currentMapObject.GetComponentInChildren<Tilemap>();
        }
    }

    public void ChangeMap(string mapName, Vector2Int playerLocation)
    {
        StartCoroutine(StartSwitchMap(mapName, playerLocation));
    }

    public IEnumerator StartSwitchMap(string mapName, Vector2Int playerLocation)
    {
        while(fadeImage.color.a < 1)
        {
            fadeImage.color = new Color(0, 0, 0, fadeImage.color.a + Time.deltaTime * transitionSpeed);
            yield return new WaitForEndOfFrame();
        }
        SwitchMap(mapName, playerLocation);
        while (fadeImage.color.a > 0)
        {
            fadeImage.color = new Color(0, 0, 0, fadeImage.color.a - Time.deltaTime * transitionSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
}
