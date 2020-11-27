using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {

	[HideInInspector]
	public GameObject turretGo;
	[HideInInspector]
	public bool isUpgraded = false;
	public GameObject buildEffect;
	private Renderer cubeRenderer;
	public TurretData turretData; 

	void Start()
	{
		cubeRenderer = GetComponent<Renderer>();
	}
	
	public void BuildTurret(TurretData turretData) 
	{
		this.turretData = turretData;
		isUpgraded = false;
		turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
		GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
		Destroy(effect, 1.5f);
	}
	
	void OnMouseEnter()
	{
		if (turretGo == null && !EventSystem.current.IsPointerOverGameObject())
		{
			cubeRenderer.material.color = Color.red;
		}
	}

	void OnMouseExit()
	{
		cubeRenderer.material.color = Color.white;
	}

	public void UpgradeTurret()
	{
		if (isUpgraded)
		{
			return;
		}

		Destroy(turretGo);
		isUpgraded = true;
		turretGo = GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
		GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
		Destroy(effect, 1.5f);
	}

	public void DestroyTurret()
	{
		Destroy(turretGo);
		isUpgraded = false;
		turretGo = null;
		turretData = null;
	}

}
