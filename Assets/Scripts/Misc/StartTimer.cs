using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartTimer : MonoBehaviour
{
	public RoomPC myRoom;
	public UnityEvent myEvent;
	Mask mask;

	private void Start()
	{
		mask = FindObjectOfType<Mask>();
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (!myRoom.isInRoom)
			{
				myRoom.tenSecAlert = false;
				myRoom.fiveSecAlert = false;
				myRoom.threeSecAlert = false;
				myRoom.twoSecAlert = false;
				myRoom.oneSecAlert = false;
				myRoom.zeroSecAlert = false;

				myRoom.isGoingOut = true;
			}
			else
			{
				myRoom.timer = mask.roomTimer;
				myRoom.isGoingOut = false;
			}
			if (myRoom.isGoingOut && myRoom.alertMsg.text == myRoom.baseMsg)
			{
				myEvent.Invoke();
			}
		}
	}
}
