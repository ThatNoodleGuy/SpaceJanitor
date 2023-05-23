using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	AudioSource audioSource;
	public AudioClip openDoor;
	//public AudioClip closeDoor;

	bool hasPlayedOpenDoor;

	public bool isLocked;
	Transform Player;
	public float checkDistance = 4;
	float startY;
	float y;
	public float doorSpeed = 1.3f;

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, checkDistance);
	}
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		Player = GameObject.Find("Player").gameObject.transform;
		startY = transform.position.y;
	}

	void Update()
	{
		if (isLocked)
		{
			CloseDoor();
			return;
		}

		if (ChackPlayer())
		{
			OpenDoor();
		}
		else
		{
			CloseDoor();
		}
	}


	public bool ChackPlayer()
	{
		float distance = Vector3.Distance(transform.position, Player.position);

		if (distance < checkDistance)
		{
			return true;
		}
		return false;
	}

	public void OpenDoor()
	{

		if (transform.localPosition.y <= startY + GetComponent<BoxCollider>().size.y + 0.5f)
		{
			y = 0;
			y += Time.deltaTime * doorSpeed;
			transform.position += new Vector3(0, y, 0);
		}

		if (!hasPlayedOpenDoor)
		{
			audioSource.PlayOneShot(openDoor);
		}
		hasPlayedOpenDoor = true;
	}

	public void CloseDoor()

	{

		if (transform.position.y > startY)
		{
			y = 0;
			y -= Time.deltaTime * doorSpeed;
			transform.position += new Vector3(0, y, 0);

		}
		else
		{
			hasPlayedOpenDoor = false;
		}
	}
}
