using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Toggle MusicOnOff;
    public Text OnOffText;

    //public GameObject OptionMenu;

    // Start is called before the first frame update
    void Start()
    {
        //OptionMenu.SetActive(false);
        //script = GameObject.Find("MenuOptions").GetComponent<MenuButtons>();
        //string mute = PlayerPrefs.GetString("Mute", "false");
        //if (mute == "true")
        //{
        //    AudioListener.pause = true;
        //    OnOffText.text = "On";
        //    MusicOnOff.isOn = true;
        //}

        //else
        //{
        //    AudioListener.pause = false;
        //    OnOffText.text = "Off";
        //    MusicOnOff.isOn = false;
        //}
    }

    public void OnClickMuteUnmute()
    {
        if (!AudioListener.pause && MusicOnOff.isOn == false)
        {
            AudioListener.pause = true;
            OnOffText.text = "Off";
            PlayerPrefs.SetString("Mute", "true");
        }

        else
        {
            AudioListener.pause = false;
            OnOffText.text = "On";
            PlayerPrefs.SetString("Mute", "false");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
