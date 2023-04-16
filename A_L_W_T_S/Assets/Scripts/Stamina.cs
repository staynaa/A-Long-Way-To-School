using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider staminaBar;
   
    public void SetMaxStamina(int stamina)
    {
        staminaBar.maxValue = stamina;
        staminaBar.value = stamina;
    }
    public void SetStamina(int stamina)
    {
        staminaBar.value = stamina;
    }
}
