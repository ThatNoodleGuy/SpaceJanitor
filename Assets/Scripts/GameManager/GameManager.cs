using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO;


public class GameManager : MonoBehaviour
{
	public TextMeshProUGUI continueTMP;
	public Button continueBtn;
	SaveLoad saveLoad;
	public Button saveGame;
	public string startScreen;
	public string gameLvlScene;
	public string endScreenScene;
	Scene scene;
	public RoomPC[] rooms;
	public MainPC mainPC;
	public PlayerOxygen playerOxygen = null;
	public PlayerHealth playerHealth = null;
	public WorkStation workStation = null;
	public Storage powerStorage = null;
	public Storage oxygenStorage = null;
	public Mask mask = null;
	public float winPoints;

	private void Start()
	{
		saveLoad = FindObjectOfType<SaveLoad>();
		rooms = FindObjectsOfType<RoomPC>();
		scene = SceneManager.GetActiveScene();
		mask = FindObjectOfType<Mask>();
		mainPC = FindObjectOfType<MainPC>();
		playerHealth = FindObjectOfType<PlayerHealth>();
		playerOxygen = FindObjectOfType<PlayerOxygen>();
	}

	private void Update()
	{
		GetGameData();
		if (SceneManager.GetActiveScene().buildIndex == 0)
		{
			ContinueGameActiv();
		}
		else if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			SaveBtnActive();
		}

		if (scene.name == gameLvlScene)
		{
			EndCondition();
		}

		foreach (RoomPC room in rooms)
		{
			if (room.isGameOverCon)
			{
				EndGameScene();
			}
		}

		if (scene.name != gameLvlScene)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	public void SaveGameBtn()
	{
		saveLoad.SaveGame();
	}
	public void SaveBtnActive()
	{
		foreach (var room in rooms)
		{
			if (room.isInRoom)
			{
				saveGame.interactable = false;
				return;
			}
		}
		saveGame.interactable = true;
	}

	public void StartScene()
	{
		SceneManager.LoadScene(startScreen);
	}

	public void PlayGame()
	{
		SceneManager.LoadScene(gameLvlScene);
	}

	public void ContinueGame()
	{
		string path = Application.persistentDataPath + "/savefile.json";
		if (File.Exists(path))
		{
			SceneManager.LoadScene(gameLvlScene);
			saveLoad.LoadGame();
		}
	}

	public void ContinueGameActiv()
	{
		string path = Application.persistentDataPath + "/savefile.json";
		if (File.Exists(path))
		{
			continueTMP.color = Color.white;
			print(path);
		}
		else
		{
			continueTMP.color = Color.gray;
			;
		}

	}
	public void EndGameScene()
	{
		SceneManager.LoadScene(endScreenScene);
	}

	public void ExitButton()
	{

		Application.Quit();
	}

	public void GetGameData()
	{
		PlayerPrefs.SetInt("powerStorageLvl", powerStorage.level);
		PlayerPrefs.SetInt("oxygenStorageLvl", oxygenStorage.level);
		PlayerPrefs.SetInt("workStationLvl", workStation.level);
		PlayerPrefs.SetInt("maskLvl", mask.level);
		PlayerPrefs.SetInt("oxygenBalloonLvl", playerOxygen.level);
		PlayerPrefs.SetInt("balanceText", Mathf.RoundToInt(mainPC.points));
	}

	public void EndCondition()
	{
		if (playerHealth.currentHealth <= 0 || mainPC.points >= winPoints)
		{
			EndGameScene();
		}
	}
}
