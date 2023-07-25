using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager I;
    [SerializeField] Transform pfEmeny;

    [SerializeField] private float spawnRate;
    private bool isSaving;
    private bool isLoading;
    public List<Transform> enemies = new List<Transform>();
    private void Awake()
    {
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("enemiesListCount"))
        {
            int count = PlayerPrefs.GetInt("enemiesListCount");
            Vector3 pos = new Vector3();
            for (int i = 0; i < count; i++)
            {
                pos.x = PlayerPrefs.GetFloat("enemyX" + i);
                pos.y = PlayerPrefs.GetFloat("enemyY" + i);

                Transform e = Instantiate(enemies[i], pos, Quaternion.identity);
                e.position = pos;
            }




        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

            TimerAction.Create(.1f, () =>
            {
                EnemyController e = EnemyController.Create(pfEmeny, GetRandomPos());
                enemies.Add(e.transform);
            });


        }
    }



    private Vector3 GetRandomPos()
    {
        Vector3 r = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        return transform.position + r * Random.Range(2f, 2f);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public List<Transform> GetEnemies()
    {
        return enemies;
    }

    private void OnApplicationQuit()
    {

        PlayerPrefs.SetInt("enemiesListCount", enemies.Count);
        for (int i = 0; i < enemies.Count; i++)
        {
            PlayerPrefs.SetFloat("enemyX" + i, enemies[i].position.x);
            PlayerPrefs.SetFloat("enemyY" + i, enemies[i].position.y);
        }



    }
}
