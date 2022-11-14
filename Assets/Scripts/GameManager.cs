using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] EagleSpawner eagleSpawner;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausedPanel;
    [SerializeField] GameObject grass;
    [SerializeField] GameObject road;
    [SerializeField] int extent = 5;
    [SerializeField] int frontDistance = 10;
    [SerializeField] int backDistance = -5;
    [SerializeField] int maxSameTerrainRepeat = 3;
    [SerializeField] Animator gameOverAnimator;
    [SerializeField] Button resumeButton;
    [SerializeField] Animator fadeAnimator;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] AudioSettings audioSettings;
    private int playerLastMaxTravel;
    private int isDead = 0;
    public static bool isPaused = false;

    Dictionary<int, TerrainBlock> map = new Dictionary<int, TerrainBlock>(50);

    // Start is called before the first frame update
    void Start()
    {
        fadeAnimator.Play("FadeIn");

        //Set Up GameOver Panel
        gameOverPanel.SetActive(false);
        pausedPanel.SetActive(false);
        // gameOverText = gameOverPanel.GetComponentInChildren<TMP_Text>();
        
        //Grass belakang
        for (int z = backDistance; z <= 0; z++)
        {
            CreateTerrain(grass, z);
        }

        //Block depan
        for (int z = 1; z <= frontDistance; z++)
        {
            var prefab = GetNextRandomTerrain(z);

            //Institiate Block
            CreateTerrain(prefab, z);
        }

        player.SetUp(backDistance, extent);
    }

    // Update is called once per frame
    private void Update()
    {
        //Update Player's Life
        if(player.isDead && gameOverPanel.activeInHierarchy==false && isDead==0)
        {
            isDead=1;
            StartCoroutine(ShowingGameOverPanel());
        }

        //Pause
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Berhasil Esc");
            if (isPaused)
            {
                ResumeMenu(); 
                Debug.Log("Berhasil Esc 1");
            }
            else
            {
                PauseMenu();
                Debug.Log("Berhasil Esc 2");
            }
        }

        //Spawn Next Terrain
        if(player.MaxTravel==playerLastMaxTravel)
            return;

        playerLastMaxTravel = player.MaxTravel;
        
        var randomTB = GetNextRandomTerrain(player.MaxTravel+frontDistance);
        CreateTerrain(randomTB, player.MaxTravel + frontDistance);

        //Delete The Most Back Terrain
        var lastTB = map[player.MaxTravel-1 + backDistance];

        //Delete from Map, and delete from scene
        map.Remove(player.MaxTravel-1 + backDistance);
        Destroy(lastTB.gameObject);

        //Fix Player Movement Restriction After Updating Map
        player.SetUp(player.MaxTravel + backDistance, extent);
    }

    IEnumerator ShowingGameOverPanel()
    {
        yield return new WaitForSecondsRealtime(3);

        gameOverText.text = "" + player.MaxTravel;
        gameOverPanel.SetActive(true);
        gameOverAnimator.Play("Game Over");
    }

    private GameObject GetNextRandomTerrain(int nextPosition)
    {
        bool isRepeated = true;
        var tbRef = map[nextPosition - 1];
        for (int distance = 2; distance <= maxSameTerrainRepeat; distance++)
        {
            if (map[nextPosition - distance].GetType() != tbRef.GetType())
            {
                isRepeated = false;
                break;
            }
        }

        if(isRepeated)
        {
            if(tbRef is Grass)
                return road;
            else
                return grass;
        }
        
        //Probabilitas Prefab Grass/Road 1:1
        return Random.value > 0.5f ? road : grass;
    }

    private void CreateTerrain(GameObject prefab, int zPosition)
    {
        var go = Instantiate(prefab, new Vector3(0,0,zPosition),Quaternion.identity);
        var tb = go.GetComponent<TerrainBlock>();
        tb.Build(extent);

        map.Add(zPosition, tb);
    }

    private void PauseMenu()
    {
        pausedPanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
        Debug.Log("Got it");
        return;
    }

    public void ResumeMenu()
    {
        pausedPanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        Debug.Log("Got it 1");
        return;
    }

    public void Retry()
    {
        gameOverPanel.SetActive(false);
        fadeAnimator.Play("FadeEnd");
        var coroutine = ChangeScene(1,1.5f);
        StartCoroutine(coroutine);
        Debug.Log("Retry");
    }

    public void MainMenu()
    {
        gameOverPanel.SetActive(false);
        fadeAnimator.Play("FadeEnd");
        var coroutine = ChangeScene(0,1.5f);
        StartCoroutine(coroutine);
        Debug.Log("MainMenu");
    }

    IEnumerator ChangeScene(int sceneIndex, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
