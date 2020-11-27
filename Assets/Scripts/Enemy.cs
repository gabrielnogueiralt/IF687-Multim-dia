using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	private float totalHp;
	public float hp = 150; 
	public Slider hpSlider; 
	public float speed = 10; 
	public GameObject explosionEffectPrefab; 
	private Transform[] positions; 
	private int index = 0;
	void Start() 
	{
		totalHp = hp;
		positions = Waypoints.positions;
	}
	
	void Update() 
	{
		Move();
	}

	void Move() 
	{
		if (index > positions.Length - 1) return;
		transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
		if (Vector3.Distance(positions[index].position, transform.position) < 0.2) 
		{
			index++;
		}
		if (index > positions.Length - 1) 
		{
			ReachDestination();
		}
	}

    public void resetar(Vector3 position)
    {
        gameObject.transform.position = position;
        hp = totalHp;
        hpSlider.value = 1;
        index = 0;
       
    }

    void ReachDestination() 
	{
		GameObject.Destroy(gameObject);
        Debug.Log(this);
		GameManager.instance.Failed();
	}

	void OnDestroy()
	{
		EnemySpawner.CountEnemyAlive--;
	}

	public void TakeDamage(float damage)
	{
		if (hp <= 0)
		{
			return;
		}
		hp -= damage;
		hpSlider.value = hp / totalHp;
		if (hp <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
		Destroy(effect, 1.5f);
        OnDestroy();
        EnemySpawner.Reuse(gameObject);
	}

}
