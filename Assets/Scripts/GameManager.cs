using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject endUI;
	public Text endMessage;
	public static GameManager instance;
	private EnemySpawner enemySpawner;
    public BulletPrototype bulletPrototype;
    public GameObject missile, bullet;
	void Awake()
	{
		instance = this;
		enemySpawner = GetComponent<EnemySpawner>();
        bulletPrototype = new BulletPrototype(bullet,missile);
	}

	public void Win()
	{
		endUI.SetActive(true);
		endMessage.text = "Vitória";
	}

	public void Failed()
	{
		endUI.SetActive(true);
		endMessage.text = "Derrota";
		enemySpawner.Stop();
	}

	public void OnButtonRetryDown()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void OnButtonMenuDown()
	{
		SceneManager.LoadScene(0);
	}

}
