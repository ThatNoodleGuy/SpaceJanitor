using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenStorageBar : MonoBehaviour
{
    public float oxygenAmount;
    public float currentOxygen;
    public Slider oxygenSlider;
    public Image OxygenSliderImage;

    MainPC mainPC;

    void Start()
    {
        mainPC = FindObjectOfType<MainPC>();

        //oxygenAmount = mainPC.oxygenStorage.maxAmount;
        //currentOxygen = mainPC.oxygenStorage.amount;

        currentOxygen = oxygenAmount;
        oxygenSlider.value = mainPC.oxygenStorage.amountPerc * 100;
    }

    void Update()
    {
        oxygenSlider.value = mainPC.oxygenStorage.amountPerc * 100;
        currentOxygen = Mathf.Clamp(currentOxygen, 0, oxygenAmount);

        OxygenSliderImage.color = Color.Lerp(Color.grey, Color.blue, oxygenSlider.value / 100);
    }
}
