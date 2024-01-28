using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesScript : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;

    private GameObject enemy;
    private float nextWaveTimer;
    private float timer = 0f;
    private float nextTime = 2f;
    private int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        nextWaveTimer = Random.Range(60, 120);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= nextWaveTimer && waveNumber < enemiesToSpawn.Length)
        {
            ++waveNumber;
            timer = 0;
            nextTime = 2;

            nextWaveTimer = Random.Range(60f, 120f);
        }


        if (timer >= nextTime)
        {
            SpawnEnemy();
            nextTime = timer + Random.Range(1f, 10f);
        }
    }

    public void SpawnEnemy()
    {
        enemy = enemiesToSpawn[Random.Range(0, waveNumber)];

        if (waveNumber == 1)
        {
            for (int i = 0; i < Random.Range(0, 2); i++)
            {
                Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
            }
        }

        if (waveNumber == 2)
        {
            for (int i = 0; i < Random.Range(1, 3); i++)
            {
                Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
            }
        }

        if (waveNumber == 3)
        {
            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
            }
        }



    }
}
