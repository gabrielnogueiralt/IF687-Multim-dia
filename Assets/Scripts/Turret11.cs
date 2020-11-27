using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	private List<GameObject> enemies = new List<GameObject>();
	public float attackRateTime = 1.0f;
	private float timer = 0;
	public GameObject bulletPrefab;
	public Transform firePosition;
	public Transform head;
	public bool isUseLaser = false;
	public float damageRate = 70;
	public LineRenderer laserRenderer;
	public GameObject laserEffect;
	
	void Start()
	{
		timer = attackRateTime;
	}

	void Update()
	{
		if (enemies.Count > 0 && enemies[0] != null)
		{
			Vector3 targetPosition = enemies[0].transform.position;
			targetPosition.y = head.position.y;
			head.LookAt(targetPosition);
		}

		if (isUseLaser)
		{
			Debug.Log ("piu piu  " + gameObject.name + "\n");
			if (enemies.Count > 0)
			{
				if (enemies[0] == null)
				{
					UpdateEnemys();
				}
				if (enemies.Count > 0)
				{
					if (laserRenderer.enabled == false)
					{
						laserRenderer.enabled = true;
						laserEffect.SetActive(true);
					}
					laserRenderer.SetPositions(new Vector3[]{firePosition.position, enemies[0].transform.position});
					laserEffect.transform.position = enemies[0].transform.position;
					laserEffect.transform.LookAt(new Vector3(transform.position.x, enemies[0].transform.position.y, transform.position.z));
					enemies[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime);
				}
			}
			else
			{
				laserRenderer.enabled = false;
				laserEffect.SetActive(false);
			}
			
		}


		else 
		{
			
			timer += Time.deltaTime;
			if (enemies.Count > 0 && timer >= attackRateTime)
			{
				// 定时器清空
				Debug.Log ("pou pou  " + gameObject.name + "\n");
				timer = 0;
				Attack();
			}
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			enemies.Add(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Enemy")
		{
			enemies.Remove(other.gameObject);
		}
	}

	void Attack()
	{
		if (enemies[0] == null)
		{
			UpdateEnemys();
		}
		if (enemies.Count > 0)
		{
			GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
			bullet.GetComponent<Bullet>().SetTarget(enemies[0].transform);
		}
		else
		{
			timer = attackRateTime;
		}
		
	}

	void UpdateEnemys()
	{
		List<int> emptyIndexList = new List<int>();
		for (int i = 0; i < enemies.Count; i++)
		{
			if (enemies[i] == null)
			{
				emptyIndexList.Add(i);
			}
		}
		for (int i = 0; i < emptyIndexList.Count; i++)
		{
			enemies.RemoveAt(emptyIndexList[i] - i);
		}
	}

}
