using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]

public class ProgressBar : MonoBehaviour
{

    [Header("Bar Setting")]

    public float progress_max = 1;
    public float progress_current = 1;

    public Color BarColor;   
    public Color BarBackGroundColor;
    public Sprite BarBackGroundSprite;
    [Range(1f, 100f)]
    public float Alert = 20;
    public Color BarAlertColor;

    private Image bar, barBackground;
    private float nextPlay;
    private AudioSource audiosource;
    private Text txtTitle;
    private float barValue;
    public float BarValue
    {
        get { return barValue; }

        set
        {
            value = Mathf.Clamp(value, 0, progress_max);
            barValue = value;
            UpdateValue(barValue);

        }
    }

        

    private void Awake()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
        barBackground = GetComponent<Image>();
        barBackground = transform.Find("BarBackground").GetComponent<Image>();
    }

    private void Start()
    {
//       bar.color = BarColor;
//        barBackground.color = BarBackGroundColor; 
        barBackground.sprite = BarBackGroundSprite;
    }

    public void UpdateValue(float newHealth)
    {
        progress_current = newHealth;
        bar.fillAmount = progress_current / progress_max;
    }

}
