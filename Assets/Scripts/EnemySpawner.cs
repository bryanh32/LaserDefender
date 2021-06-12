using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = false;
    [SerializeField] int startingWave = 0;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } 
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnEnemyWaves(currentWave));

        }
    }

    private IEnumerator SpawnEnemyWaves(WaveConfig waveConfig)
    {
        var numberOfEnemies = waveConfig.GetNumberOfEnemies();
        for (int i = 0; i < numberOfEnemies; i++) {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
