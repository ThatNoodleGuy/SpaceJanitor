using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpace : MonoBehaviour
{
	public RoomPC myRoom;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
			myRoom.isInRoom = true;
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
			myRoom.isInRoom = false;
	}
}
