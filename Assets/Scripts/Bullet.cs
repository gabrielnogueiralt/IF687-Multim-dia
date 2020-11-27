using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public int damage = 50; 
	public float speed = 40; 
	public GameObject explosionEffectPrefab; 
	private Transform target; 
	
	public void SetTarget(Transform target)
	{
		this.target = target;
	}

	void Update() 
	{
        if ((target == null) || (target.gameObject.activeInHierarchy == false))
        {
            Die();
            return;
        }
        else
        {

            transform.LookAt(target.position);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			other.GetComponent<Enemy>().TakeDamage(damage);
			Die();
		}
	}

	void Die()
	{
		GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
		Destroy(effect, 1);
		Destroy(gameObject);
	}

}
