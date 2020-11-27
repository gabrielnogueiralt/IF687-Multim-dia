using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Wave[] waves;
    public GameObject prefab;
	public Transform START; 
	public float waveRate = 0.2f; 
	public static int CountEnemyAlive = 0; 
	private Coroutine coroutine;   
    public FlyweightEnemies pool;
        
	void Start()
	{
        pool = new FlyweightEnemies(prefab, START.position, Quaternion.identity);
        coroutine = StartCoroutine(SpawnEnemy());
       

	}

	IEnumerator SpawnEnemy() 
	{
		foreach (Wave wave in waves)
		{
			for (int i = 0; i < wave.count; i++) 
			{
                pool.OI();
                pool.getEnemy();
          
                CountEnemyAlive++;
				if (i != wave.count - 1) 
				{
					yield return new WaitForSeconds(wave.rate);
				}
			}
			while (CountEnemyAlive > 0)
			{
				yield return 0;
			}
			yield return new WaitForSeconds(waveRate);
		}

		while (CountEnemyAlive > 0)
		{
			yield return 0;
		}
		GameManager.instance.Win();
	}

	public void Stop()
	{
		StopCoroutine(coroutine);
	}

    public static void Reuse(GameObject o)
    {
        FlyweightEnemies.Reuse(o);
    }

    void Update()
    {
        Debug.Log(CountEnemyAlive);
    }
}
