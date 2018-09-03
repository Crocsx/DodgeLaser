using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{

    Button button;

    public Sprite UnMuteSprite;
    public Sprite MuteSprite;
    // Use this for initialization
    void Start()
    {
        button = GetComponent<Button>();

        if (SoundManager.Instance.volume == 0)
            Mute();
        else
            Unmute();
    }

    void SetButtonSprite()
    {
        if (SoundManager.Instance.volume > 0)
            button.GetComponent<Image>().sprite = UnMuteSprite;
        else
            button.GetComponent<Image>().sprite = MuteSprite;
    }

    void Unmute()
    {
        SoundManager.Instance.Unmute();
        SetButtonSprite();
    }

    void Mute()
    {
        SoundManager.Instance.Mute();
        SetButtonSprite();
    }

    public void ToggleSound()
    {
        if (SoundManager.Instance.volume > 0)
            Mute();
        else
            Unmute();
    }
}