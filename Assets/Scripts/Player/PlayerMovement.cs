using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	public AudioSource musicBG;
	AudioSource audioManager;
	//UI
	public Slider playerSpeed;
	public Slider mouseSensitivity;
	public Slider gameVolume;
	public Slider musicBGVolume;
	public GameObject pauseMenu;

	// look around
	float mouseX;
	public float cameraSpeed;
	float mouseY;
	float cameraX;
	Transform cameraTrn;

	//movement
	float horizontal;
	float vertical;
	public float movementSpeed;
	Vector3 v;
	CharacterController characterController;

	// jump
	Transform groundCheckTrn;
	public LayerMask groundLayer;
	public bool isGround;
	float gravity = -9.81f;
	Vector3 velocity;
	[SerializeField] float jumpHeight;

	void Start()
	{
		audioManager = GetComponent<AudioSource>();
		// movement init
		cameraX = 0;
		cameraTrn = GameObject.FindGameObjectWithTag("PlayerCamera").transform;
		characterController = GetComponent<CharacterController>();

		// jump init
		groundCheckTrn = GameObject.Find("GroundCheck").transform;

		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			movementSpeed = playerSpeed.value * 2;
		}
		else
		{
			movementSpeed = playerSpeed.value;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PauseGame();
		}

		if (pauseMenu.activeInHierarchy)
		{
			return;
		}

		// rotate left & right
		mouseX = Input.GetAxis("Mouse X") * cameraSpeed * Time.deltaTime;
		transform.Rotate(0, mouseX, 0);

		// rotate up & down
		mouseY = Input.GetAxis("Mouse Y") * cameraSpeed * Time.deltaTime;
		cameraX -= mouseY;
		cameraX = Mathf.Clamp(cameraX, -65, 48);
		cameraTrn.localRotation = Quaternion.Euler(cameraX, 0, 0);

		// movement
		horizontal = Input.GetAxis("Horizontal") * movementSpeed * 100 * Time.deltaTime;
		vertical = Input.GetAxis("Vertical") * movementSpeed * 100 * Time.deltaTime;

		// new vector3(0,0,1) * vertical + new vector3(1,0,0) * horizontal
		v = transform.forward * vertical + transform.right * horizontal;
		characterController.Move(v * Time.deltaTime);

		// jump
		//isGround = Physics.CheckSphere(groundCheckTrn.position, 0.2f, groundLayer);

		if (transform.position.y > 1.4f)
		{
			transform.position += new Vector3(0, gravity, 0) * Time.deltaTime;
		}
		/*		else
				{
					velocity.y += gravity * Time.deltaTime;
				}
				characterController.Move(velocity * Time.deltaTime);
		*/
		/*		if (Input.GetButtonDown("Jump") == true)
				{
					velocity.y += jumpHeight;
				}*/
	}

	private void PauseGame()
	{
		pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
		if (pauseMenu.activeInHierarchy)
		{
			//Time.timeScale = 0;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			//Time.timeScale = 1;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}


	}

	public void SetPlayerSpeed()
	{
		movementSpeed = playerSpeed.value;
	}

	public void SetMouseSensitivity()
	{
		cameraSpeed = mouseSensitivity.value;
	}

	public void SetGameVoluve()
	{
		AudioListener.volume = gameVolume.value;
	}

	public void SetMusicVolume()
	{
		musicBG.volume = musicBGVolume.value;
	}

	public void SetDefaults()
	{
		musicBGVolume.value = 0.5f;
		gameVolume.value = 1;
		mouseSensitivity.value = 225;
		playerSpeed.value = 4;

	}
}
