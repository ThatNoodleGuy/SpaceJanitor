using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageStatusBar : MonoBehaviour
{
    RoomPC roomPC;
    MainPC mainPC;

    public float powerAmount = 100f;
    public float currentPower;
    public Slider powerSlider;
    public Image powerSliderImage;

    void Start()
    {
        mainPC = FindObjectOfType<MainPC>();

        //powerAmount = mainPC.powerStorage.maxAmount;
        //currentPower = mainPC.powerStorage.amount;

        currentPower = powerAmount;
        powerSlider.value = mainPC.powerStorage.amountPerc * 100;
    }

    void Update()
    {
        powerSlider.value = mainPC.powerStorage.amountPerc * 100;
        currentPower = Mathf.Clamp(currentPower, 0, powerAmount);

        powerSliderImage.color = Color.Lerp(Color.red, Color.green, powerSlider.value / 100);
    }
}
