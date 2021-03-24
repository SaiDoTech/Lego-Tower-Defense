using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public float timeBetweenWaves = 7f;
    public float timeBetweenEnemy = 2f;

    private Transform spawnTransform;
    public float countDown = 2f;
    private int waveIndx = 1;

    // Update is called once per frame
    void Update()
    {
        if (spawnTransform == null)
            spawnTransform = MapCreator.PathPoints[0];

        if ((GameState.IsGameStarted) && (!GameState.IsGameOnPause))
        {
            if (countDown <= 0f)
            {
                StartCoroutine(SpawnWave());
                return;
            }

            countDown -= Time.deltaTime;
            countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        }
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveIndx; i++)
        {
            SpawnEnemy();
            countDown = timeBetweenWaves;
            yield return new WaitForSeconds(timeBetweenEnemy);
        }

        waveIndx++;
    }

    private void SpawnEnemy()
    {
        if (!GameState.IsGameOnPause)
        {
            System.Random rnd = new System.Random();

            Instantiate(Enemies[rnd.Next(0, Enemies.Length)], spawnTransform.position, new Quaternion());
        }
    }
}
