using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPC : MonoBehaviour
{
	public AudioSource playerAudioSource;
	public AudioClip lowResouces;
	public AudioClip fillStorageSFX;

	public bool tenSecAlert;
	public bool fiveSecAlert;
	public bool threeSecAlert;
	public bool twoSecAlert;
	public bool oneSecAlert;
	public bool zeroSecAlert;

	public AudioClip a10;
	public AudioClip a5;
	public AudioClip a3;
	public AudioClip a2;
	public AudioClip a1;
	public AudioClip a0;


	public void VoiceAlert()
	{
		if (Mathf.Floor(timer) == 16)
		{
			if (!tenSecAlert)
			{
				playerAudioSource.PlayOneShot(a10);
				tenSecAlert = true;
			}
		}
		if (Mathf.Floor(timer) == 5)
		{
			if (!fiveSecAlert)
			{
				playerAudioSource.PlayOneShot(a5);
				fiveSecAlert = true;
			}
		}
		if (Mathf.Floor(timer) == 3)
		{
			if (!threeSecAlert)
			{
				playerAudioSource.PlayOneShot(a3);
				threeSecAlert = true;
			}
		}
		if (Mathf.Floor(timer) == 2)
		{
			if (!twoSecAlert)
			{
				playerAudioSource.PlayOneShot(a2);
				twoSecAlert = true;
			}
		}
		if (Mathf.Floor(timer) == 1)
		{
			if (!oneSecAlert)
			{
				playerAudioSource.PlayOneShot(a1);
				oneSecAlert = true;
			}
		}
		if (Mathf.Floor(timer) == 0)
		{
			if (!zeroSecAlert && oneSecAlert)
			{
				playerAudioSource.PlayOneShot(a0);
				zeroSecAlert = true;
				StartCoroutine(WaitMomentForZero(a0.length + 0.5f));
			}
		}
	}

	public bool hasPlayedVoiceAlert;

	public Door OuterDoor;
	public Door myDoor;

	public float timer;
	public Text timerText;
	public Storage myTank;
	public Text ammountPerc;
	public Text storageLvlText;
	public Button fillStorage;
	public Text alertMsg;
	public string baseMsg = "No Errors, can fill storage.";
	public bool isGoingOut;
	public float dir;

	public Light myAlertLight;
	float spintLight = 180f;
	public float spinningSpeed;
	public bool isInRoom;
	public bool isGameOverCon = false;

	WorkStation workStation;
	MainPC mainPC;

	public void startTimer()
	{
		timer -= Time.deltaTime;
		timer = timer < 0 ? 0 : timer;
	}

	private void Start()
	{
		mainPC = FindObjectOfType<MainPC>();
		workStation = FindObjectOfType<WorkStation>();
	}



	public void SetLock()
	{
		if (isInRoom)
		{
			if (timer > 0)
			{
				UnlockDoors();
			}
			else
			{
				LockDoors();
			}
		}
		else
		{
			if (HasRecource())
			{
				LockDoors();
			}
			else
			{
				UnlockDoors();
			}
		}
	}

	public void LockDoors()
	{
		myDoor.isLocked = true;
		OuterDoor.isLocked = true;
	}
	public void UnlockDoors()
	{
		myDoor.isLocked = false;
		OuterDoor.isLocked = false;
	}


	private void Update()
	{

		VoiceAlert();
		SetLock();
		//alert lights
		if (HasRecource())
		{
			AlertLighsOn();
		}
		else
		{
			AlertLighsOff();
		}
		//update UI
		//Stats
		ammountPerc.text = (myTank.amountPerc * 100).ToString("0") + "%";
		storageLvlText.text = "(lvl" + myTank.level + ")";
		//Errors
		if (alertMsg.text == baseMsg)
		{
			fillStorage.interactable = true;
		}
		else
		{
			fillStorage.interactable = false;
		}

		//
		if (isInRoom)
		{
			startTimer();
		}
		else
		{
			timer = 0;
		}
		timerText.text = "Time:" + Mathf.Floor(timer);
	}

	public void Full()
	{
		myTank.amount = myTank.maxAmount;
		playerAudioSource.PlayOneShot(fillStorageSFX);
	}

	public void AlertMassage(List<string> errors)
	{
		if (errors.Count > 1)
		{
			alertMsg.text = errors[0];

			for (int i = 1; i < errors.Count; i++)
			{
				alertMsg.text += errors[i] + "; ";
			}
		}
		else
		{
			alertMsg.text = baseMsg;
		}
	}

	public bool HasRecource()
	{
		if (myTank.amount >= myTank.reqAmount * workStation.level)
		{

			hasPlayedVoiceAlert = false;

			return true;
		}
		else
		{
			if (!hasPlayedVoiceAlert)
			{
				StartCoroutine(WaitMoment(2.5f));
			}
			hasPlayedVoiceAlert = true;
		}
		return false;
	}

	public IEnumerator WaitMoment(float moment)
	{
		yield return new WaitForSeconds(moment);
		playerAudioSource.PlayOneShot(lowResouces);
	}
	public IEnumerator WaitMomentForZero(float moment)
	{
		yield return new WaitForSeconds(moment);
		if (isInRoom && zeroSecAlert)
		{
			isGameOverCon = true;
		}
	}



	public void AlertLighsOn()
	{
		AlertLight(myAlertLight, true);
		SpinAlertLight(myAlertLight, false);
	}

	public void AlertLighsOff()
	{
		AlertLight(myAlertLight, false);
		SpinAlertLight(myAlertLight, true);
	}

	public void AlertLight(Light myLight, bool state)
	{
		myLight.gameObject.SetActive(state);
	}

	public void SpinAlertLight(Light myLight, bool state)
	{
		myLight.gameObject.SetActive(state);
		if (!state)
			spintLight = 0;
		else
		{
			spintLight += spinningSpeed * Time.deltaTime;
		}
		myLight.transform.localRotation = Quaternion.Euler(spintLight, dir, 0);
	}


}
