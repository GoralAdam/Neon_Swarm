using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class WaveContent
{
    public string waveName;
    public GameObject enemyPrefab;
    public int count;
    public float spawnRate;
}

public class WaveManager : MonoBehaviour
{
    public List<WaveContent> waves; // Lista fal definiowana w Inspektorze
    public Transform[] spawnPoints; // Punkty, w których pojawią się wrogowie
    public float timeBetweenWaves = 3f;
    
    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(PlayGame());
    }

    IEnumerator PlayGame()
    {
        while (currentWaveIndex < waves.Count)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            currentWaveIndex++;
            
            // Opcjonalnie: czekaj aż wszyscy przeciwnicy zginą przed kolejną falą
            while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            {
                yield return new WaitForSeconds(1f);
            }
        }
        Debug.Log("Wszystkie fale pokonane! Czas na bossa?");
    }

    IEnumerator SpawnWave(WaveContent wave)
    {
        isSpawning = true;
        Debug.Log("Starting Wave: " + wave.waveName);

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(wave.spawnRate);
        }

        isSpawning = false;
    }

    void SpawnEnemy(GameObject prefab)
    {
        // Wybierz losowy punkt spawnu z tablicy
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(prefab, spawnPoints[spawnIndex].position, Quaternion.identity);
    }
}