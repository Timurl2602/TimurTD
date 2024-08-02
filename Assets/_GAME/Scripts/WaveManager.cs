using System.Collections;
using System.Collections.Generic;
using Supyrb;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<Wave> Waves;
    public Wave CurrentWave;
    
    public int TimerInBetweenWaves = 5;
    public int CurrentEnemiesAlive;

    private int currentWaveIndex = 0;
    private bool isWaveActive = false;

    [SerializeField] private PlayerStats _playerStats;
    

    private void Start()
    {
        if (Waves.Count > 0)
        {
            StartCoroutine(ManageWaves());
        }
    }

    private IEnumerator ManageWaves()
    {
        while (currentWaveIndex < Waves.Count)
        {
            CurrentWave = Waves[currentWaveIndex];
            StartWave();
            yield return new WaitUntil(() => CurrentEnemiesAlive == 0 || _playerStats.CurrentHealth <= 0 );
            EndWave();
            yield return new WaitForSeconds(TimerInBetweenWaves);
            currentWaveIndex++;
        }
    }

    private void StartWave()
    {
        CurrentEnemiesAlive = CurrentWave.Enemies.Count;
        isWaveActive = true;
        Signals.Get<WaveStartedSignal>().Dispatch();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        foreach (var enemy in CurrentWave.Enemies)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f / CurrentWave.SpawnRate);
        }
    }

    private void EndWave()
    {
        isWaveActive = false;
        Signals.Get<WaveEndedSignal>().Dispatch();
    }
}