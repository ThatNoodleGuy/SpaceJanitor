using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOxygen : MonoBehaviour
{
	public Slider oxygenBar;
	public float oxygenAmount = 100f;
	public float currentOxygen;
	public float decreaseOxygenBy = 1f;
	public float amountRatio;
	public float upgradePerc = 1.1f;
	public int level = 1;
	public int upgradeCost;

	MainPC mainPC;

	void Start()
	{
		currentOxygen = oxygenAmount;
		float health = GetComponent<PlayerHealth>().currentHealth;
		mainPC = FindObjectOfType<MainPC>();
	}

	public void UpgardeMaxCapacity()
	{
		oxygenAmount++;
		level++;
	}

	void Update()
	{
		upgradeCost = level + 10 - 1;
		amountRatio = currentOxygen / oxygenAmount * 100;
		oxygenBar.value = amountRatio;
		currentOxygen = Mathf.Clamp(currentOxygen, 0, oxygenAmount);

		if (mainPC.oxygenStorage.amount < 0.001f)
		{
			currentOxygen -= decreaseOxygenBy * Time.deltaTime;
		}
		else
		{
			currentOxygen += decreaseOxygenBy * Time.deltaTime;
		}
	}
}
