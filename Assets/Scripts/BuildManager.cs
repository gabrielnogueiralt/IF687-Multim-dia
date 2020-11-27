using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

	public TurretData laserTurretData; 
	public TurretData missileTurretData; 
	public TurretData standardTurretData; 
	private TurretData selectedTurretData; 
	private MapCube selectedMapCube; 
	public Text moneyText;
	public Animator moneyAnimator; 
	private int money = 1000; 
	public GameObject upgradeCanvas; 
	public Button upgradeButton; 
	public Animator upgradeCanvasAnimator; 
	
	void ChangeMoney(int change)
	{
		money += change;
		moneyText.text = "R$ " + money;
	}
	
	void Update() 
	{

		if (Input.GetMouseButtonDown(0))
		{

			if (EventSystem.current.IsPointerOverGameObject() == false)
			{
				
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				
				bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
				if (isCollider)
				{
					
					MapCube mapCube = hit.collider.gameObject.GetComponent<MapCube>();
					
					if (selectedTurretData != null && mapCube.turretGo == null)
					{
						
						if (money >= selectedTurretData.cost)
						{
							
							ChangeMoney(-selectedTurretData.cost);
							
							mapCube.BuildTurret(selectedTurretData);
						}
						else 
						{
							
							moneyAnimator.SetTrigger("Flicker");
						}
					}
					else if (mapCube.turretGo != null)
					{
						if (mapCube == selectedMapCube && upgradeCanvas.activeInHierarchy)
						{
							
							StartCoroutine(HideUpgradeUI());
						}
						else
						{
							
							ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
						}
						
						selectedMapCube = mapCube;
					}
				}
			}
		}
	}

	
	public void OnLaserSelected(bool isOn) 
	{
		if (isOn)
		{
			selectedTurretData = laserTurretData;
		}
	}

	public void OnMissileSelected(bool isOn) 
	{
		if (isOn)
		{
			selectedTurretData = missileTurretData;
		}
	}

	public void OnStandardSelected(bool isOn)
	{
		if (isOn)
		{
			selectedTurretData = standardTurretData;
		}
	}

	void ShowUpgradeUI(Vector3 position, bool isDisableUpgrade)
	{
		StopCoroutine(HideUpgradeUI());
		upgradeCanvas.SetActive(false);

		upgradeCanvas.SetActive(true);
		upgradeCanvas.transform.position = position;
		upgradeButton.interactable = !isDisableUpgrade;
	}

	IEnumerator HideUpgradeUI()
	{
		upgradeCanvasAnimator.SetTrigger("Hide");
		yield return new WaitForSeconds(0.5f);
		upgradeCanvas.SetActive(false);
	}

	public void OnUpgradeButtonDown()
	{
		if (money >= selectedMapCube.turretData.costUpgraded)
		{
			ChangeMoney(-selectedTurretData.costUpgraded);
			selectedMapCube.UpgradeTurret();
			StartCoroutine(HideUpgradeUI());
		}
		else 
		{
			moneyAnimator.SetTrigger("Flicker");
		}
	}
	
	public void OnDestroyButtonDown()
	{
		selectedMapCube.DestroyTurret();
		StartCoroutine(HideUpgradeUI());
	}

}
