﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextboxHandler : MonoBehaviour
{
    public AudioClip textSound;
    public Image screenOverlay, textboxBackground;
    public Text textToPrint;
    public bool startOpen;

    private AudioSource source;
    private float inputDelay, soundDelayTimer;
    private string textQueue;
    private bool isTextboxOpen;

    void Start()
    {
        textToPrint = GetComponent<Text>();
        textQueue = textToPrint.text;
        textToPrint.text = "";

        source = GetComponent<AudioSource>();
        source.clip = textSound;
        source.loop = false;

        if (!startOpen)
        {
            textboxBackground.enabled = false;
        }

        isTextboxOpen = startOpen;
    }

    void Update()
    {
        if (textQueue != "")
        {
            GameManager.SetGameActive(false);
            Time.timeScale = 0.0f;
            textboxBackground.enabled = true;
            isTextboxOpen = true;
            textToPrint.text += textQueue.Substring(0, 1);
            textQueue = textQueue.Substring(1);

            soundDelayTimer -= Time.unscaledDeltaTime;
            if(soundDelayTimer <= 0.0f)
            {
                source.Play();
                soundDelayTimer = 0.05f;
            }
        }

        if (Input.GetButtonDown("Fire1") && inputDelay < 0)
        {
            if (textQueue == "")
            {
                
                GameManager.SetGameActive(true);
                Time.timeScale = 1.0f;
                textboxBackground.enabled = false;
                isTextboxOpen = false;
                textToPrint.text = "";
            }
            else
            {
                textToPrint.text += textQueue;
                textQueue = "";
                source.Play();
            }
        }
        else
        {
            inputDelay -= Time.unscaledDeltaTime;
        }
    }

    public void SetTextToPrint(string textToPrint)
    {
        this.textToPrint.text = "";
        textQueue = textToPrint;
        inputDelay = 0.2f;
    }

    public bool IsTextboxOpen()
    {
        return isTextboxOpen;
    }
}
