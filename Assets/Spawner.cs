using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    private int waveNumber = 0;
    public int enemySpawnAmount = 0;
    public int enemiesKilled = 0;
    public Text Wavetxt;

    public GameObject[] spawners;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        spawners = new GameObject[5];

        for(int i = 0; i < spawners.Length; i++)
        {
            //if (this.gameObject.transform.childCount > i)
                spawners[i] = transform.GetChild(i).gameObject;
        }

        StartWave();
        setWaveText(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            SpawnEnemy();
        }

        if(enemiesKilled >= enemySpawnAmount)
        {
            NextWave();
        }

        setWaveText(waveNumber);
    }

    private void SpawnEnemy()
    {
        int spawnerID = Random.Range(0, spawners.Length);
        Instantiate(enemy, spawners[spawnerID].transform.position, spawners[spawnerID].transform.rotation);
    }

    private void StartWave()
    {
        waveNumber = 1;
        enemySpawnAmount = 4;
        enemiesKilled = 0;

        for(int i = 0; i < enemySpawnAmount; i++)
        {
            SpawnEnemy();
        }
    }

    public void setWaveText(int waveNumber)
    {
        Wavetxt.text = "Wave: " + waveNumber;
    }

    public void NextWave()
    {
        waveNumber++;
        enemySpawnAmount += 2;
        enemiesKilled = 0;

        for (int i = 0; i < enemySpawnAmount; i++)
        {
            SpawnEnemy();
        }
    }

}
