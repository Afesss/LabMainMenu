using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private Text[] allGameText;
    [SerializeField] private Slider fontSize;
    [SerializeField] private Slider menuValume;
    [SerializeField] private Slider backgroundColor;
    [SerializeField] private Dropdown colorText;
    [SerializeField] private AudioSource menuAudio;
    [SerializeField] private AudioSource menuSound;
    [SerializeField] private SpriteRenderer background;
    private Color blackBackground = new Color(0.16f, 0.15f, 0.15f);
    private Color textColorGray = new Color(0.67f, 0.67f, 0.67f);
    private Color textColorOrange = new Color(0.72f, 0.67f, 0.24f);
    private Color textColorRed = new Color(0.58f, 0.02f, 0);
    
    private void Start()
    {
        //gameObject.SetActive(false);
    }
    public void ChangeTextSize()
    {
        foreach(var text in allGameText)
        {
            text.fontSize = Mathf.FloorToInt(fontSize.value);
        }
    }
    public void ChangeValume()
    {
        menuAudio.volume = menuValume.value;
        menuSound.volume = menuValume.value;
    }
    public void ChangeBackgroundColor()
    {
        if (Mathf.FloorToInt(backgroundColor.value) == 0)
        {
            UIManager.Instance.CurrentBackColor = "Black";
            background.color = blackBackground;
        }
        else if(Mathf.FloorToInt(backgroundColor.value) == 1)
        {
            UIManager.Instance.CurrentBackColor = "Gray";
            background.color = Color.gray;
        }
    }
    public void ChangeColorText()
    {
        int index = colorText.value;
        switch (index)
        {
            case 0:
                foreach(var text in allGameText)
                {
                    text.color = textColorGray;
                }
                break;
            case 1:
                foreach(var text in allGameText)
                {
                    text.color = Color.black;
                }
                break;
            case 2:
                foreach(var text in allGameText)
                {
                    text.color = textColorOrange;
                }
                break;
            case 3:
                foreach (var text in allGameText)
                {
                    text.color = textColorRed;
                }
                break;
            default:
                break;
        }
    }
    
}
