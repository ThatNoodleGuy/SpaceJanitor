using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTool : MonoBehaviour
{
	public int selectedWeapon = 0;

	void Start()
	{
		SelectWeapon();
	}

	void Update()
	{
		int lastSelectedWeapon = selectedWeapon;

		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if (selectedWeapon >= transform.childCount - 1)
			{
				selectedWeapon = 0;
			}
			else
			{
				selectedWeapon++;
			}
		}

		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if (selectedWeapon <= 0)
			{
				selectedWeapon = transform.childCount - 1;
			}
			else
			{
				selectedWeapon--;
			}
		}

		if (lastSelectedWeapon != selectedWeapon)
		{
			SelectWeapon();
		}
	}

	void SelectWeapon()
	{
		int i = 0;
		foreach (Transform weapon in transform)
		{
			if (i == selectedWeapon)
			{
				weapon.gameObject.SetActive(true);
			}
			else
			{
				weapon.gameObject.SetActive(false);
			}
			i++;
		}
	}
}
