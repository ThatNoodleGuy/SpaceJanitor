using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPC : MonoBehaviour
{
	public AudioSource workstationaAudioSource;
	public AudioSource playerAudioSource;
	public AudioClip workstationOff;
	public AudioClip clickUI;
	public AudioClip upgradeBuilding;
	public AudioClip upgradeGear;
	public AudioClip upgradeFail;


	bool hasPlayedWorkstationOff;

	PlayerOxygen playerOxygen;
	PlayerHealth playerHealth;

	[Header("Station members")]
	public Storage powerStorage;
	public RoomPC powerRoomPC;
	public Storage oxygenStorage;
	public RoomPC oxygenRoomPC;
	public RoomPC[] rooms;
	public WorkStation workStation;

	[Header("Vars")]
	public bool isWorking;
	public float points;
	public bool isHomeScreen = true;

	Mask mask;

	[Header("MonitorUI")]
	public Button viewBtn;
	public GameObject homeUI;
	public GameObject storeUI;
	public Button StartBtnUI;
	public GameObject workingIcon;
	public Text scoreUI;
	public Text powerTextUI;
	public Text oxygenTextUI;
	public Text workstationLvlMain;
	public Text workstationCurrproductionMain;

	[Header("StoreUI: SpaceShip")]
	public Text powerStorageLvl;
	public Text powerCurrAmount;
	public Text powerNextLvlAmount;
	public Text powerUpgradeCost;
	public Text oxygenStorageLvl;
	public Text oxygenCurrAmount;
	public Text oxygenNextLvlAmount;
	public Text oxygenUpgradeCost;
	public Text workstationLvl;
	public Text workstationCurrproduction;
	public Text workstationNextLvlproduction;
	public Text workStationUpgradeCost;

	[Header("StoreUI: Player Gear")]

	public Text healCostText;
	public Text maskLvl;
	public Text maskCostText;
	public Text timeInRooms;
	public Text oxygenBaloonCost;
	public Text oxygenLvl;



	public void SwitchViews()
	{
		isHomeScreen = !isHomeScreen;
		playerAudioSource.PlayOneShot(clickUI);
	}

	public void UpgradePowerStorage()
	{
		if (points >= powerStorage.upgradeCost)
		{
			powerStorage.UpgradeAndFillStorage();
			points -= powerStorage.upgradeCost;
			playerAudioSource.PlayOneShot(upgradeBuilding);
		}
		else
		{
			playerAudioSource.PlayOneShot(upgradeFail);
		}
	}

	public void UpgradeOxygenStorage()
	{
		if (points >= oxygenStorage.upgradeCost)
		{
			oxygenStorage.UpgradeAndFillStorage();
			points -= oxygenStorage.upgradeCost;
			playerAudioSource.PlayOneShot(upgradeBuilding);
		}
		else
		{
			playerAudioSource.PlayOneShot(upgradeFail);
		}
	}

	public void UpgradeWorkStation()
	{
		if (points >= workStation.upgradeCost)
		{
			workStation.UpgradeWorkStation();
			points -= workStation.upgradeCost;
			playerAudioSource.PlayOneShot(upgradeBuilding);
		}
		else
		{
			playerAudioSource.PlayOneShot(upgradeFail);
		}
	}

	private void Start()
	{
		playerOxygen = FindObjectOfType<PlayerOxygen>();
		playerHealth = FindObjectOfType<PlayerHealth>();
		mask = FindObjectOfType<Mask>();
		rooms = FindObjectsOfType<RoomPC>();
	}

	public void PowerBtn()
	{
		if (CanWork())
		{
			workstationaAudioSource.enabled = true;
			hasPlayedWorkstationOff = false;
			InvokeRepeating(nameof(StartMining), 1, 1);
			StartBtnUI.interactable = false;
			workingIcon.SetActive(true);
		}
		playerAudioSource.PlayOneShot(clickUI);
	}


	private void Update()
	{
		//update UI
		scoreUI.text = "Balance: " + points.ToString("#,###") + "$";
		if (isHomeScreen)
		{
			HomeUI();
		}
		else
		{
			StoreUI();
		}

		//Stop Machines
		if (!CanWork())
		{
			CancelInvoke(nameof(StartMining));
			StartBtnUI.interactable = true;
			workingIcon.SetActive(false);

			if (!hasPlayedWorkstationOff)
				playerAudioSource.PlayOneShot(workstationOff);
			hasPlayedWorkstationOff = true;
			workstationaAudioSource.enabled = false;
		}
	}

	public void HomeUI()
	{
		homeUI.SetActive(true);
		storeUI.SetActive(false);
		powerTextUI.text = (powerStorage.amountPerc * 100).ToString("0") + "%";
		oxygenTextUI.text = (oxygenStorage.amountPerc * 100).ToString("0") + "%";
		workstationLvlMain.text = "Work Station Lvl: " + workStation.level; ;
		workstationCurrproductionMain.text = "Current Production Rate: " + workStation.addPoints.ToString("0") + "$/sec"; ;

	}

	public void StoreUI()
	{
		homeUI.SetActive(false);
		storeUI.SetActive(true);

		powerStorageLvl.text = "Power\nLvl: " + powerStorage.level;
		powerUpgradeCost.text = "Cost: " + powerStorage.upgradeCost.ToString("0") + "$";
		powerCurrAmount.text = "Storage capacity: " + powerStorage.maxAmount.ToString("0");
		powerNextLvlAmount.text = "Upgrade capacity: " + (powerStorage.maxAmount * powerStorage.upgradePerc * powerStorage.level).ToString("0");

		oxygenStorageLvl.text = "O2\nLvl: " + oxygenStorage.level;
		oxygenUpgradeCost.text = "Cost: " + oxygenStorage.upgradeCost.ToString("0") + "$";
		oxygenCurrAmount.text = "Storage capacity: " + oxygenStorage.maxAmount.ToString("0");
		oxygenNextLvlAmount.text = "Upgrade capacity: " + (oxygenStorage.maxAmount * oxygenStorage.upgradePerc * oxygenStorage.level).ToString("0");

		workstationLvl.text = "Work Station\nLvl: " + workStation.level;
		workStationUpgradeCost.text = "Cost: " + workStation.upgradeCost.ToString("0") + "$";
		workstationCurrproduction.text = "Production: " + workStation.addPoints.ToString("0") + "$/sec";
		workstationNextLvlproduction.text = "Upgrade Production: " + (workStation.addPoints * workStation.upgradePerc).ToString("0") + "$/sec";

		//healCostText
		healCostText.text = "Cost: " + playerHealth.missingHealth.ToString("0") + "$";
		//mask
		maskCostText.text = "Cost: " + mask.upgradeCost.ToString("0");
		timeInRooms.text = "Room time: " + mask.roomTimer.ToString() + "s";
		maskLvl.text = "Gas Mask\nLvl" + mask.level;
		//baloon
		oxygenBaloonCost.text = "Cost: " + playerOxygen.upgradeCost + "$";
		oxygenLvl.text = "Oxygen Baloon\nLvl" + playerOxygen.level;
	}

	public void Heal()
	{
		if (points >= playerHealth.missingHealth)
		{
			playerHealth.currentHealth = playerHealth.healthAmount;
			points -= playerHealth.missingHealth;
			playerAudioSource.PlayOneShot(upgradeGear);
		}
		else
		{
			playerAudioSource.PlayOneShot(upgradeFail);
		}
	}

	public void UpgradeBaloon()
	{
		if (points >= playerOxygen.upgradeCost)
		{
			playerOxygen.UpgardeMaxCapacity();
			points -= playerOxygen.upgradeCost;
			playerAudioSource.PlayOneShot(upgradeGear);
		}
		else
		{
			playerAudioSource.PlayOneShot(upgradeFail);
		}
	}

	public void UpgradeMask()
	{
		if (points >= mask.upgradeCost)
		{
			mask.UpgradeMask();
			points -= mask.upgradeCost;
			playerAudioSource.PlayOneShot(upgradeGear);
		}
		else
		{
			playerAudioSource.PlayOneShot(upgradeFail);
		}
	}

	public void StartMining()
	{
		workStation.Work();
	}

	public bool CanWork()
	{
		foreach (var item in rooms)
		{
			if (!item.HasRecource())
			{
				return false;
			}
		}
		return true;
	}
}
