using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHealthBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bossName;
    Slider slider;

    private void Awake() 
    {
        slider = GetComponent<Slider>();
    }

    private void Start() 
    {
        SetUIHealthBarToInactive();
    }

    public void SetBossName(string name)
    {
        bossName.text = name;
    }

    public void SetUIHealthBarToActive()
    {
        slider.gameObject.SetActive(true);
    }

    public void SetUIHealthBarToInactive()
    {
        slider.gameObject.SetActive(false);
    }

    public void SetBossMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetBossCurrentHealth(int curretHealth)
    {
        slider.value= curretHealth;
    }
}
