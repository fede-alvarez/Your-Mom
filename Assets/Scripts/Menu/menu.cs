using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class menu : MonoBehaviour
{
    public bool spanish = false;
    public TMP_Text PlayText;
    public TMP_Text OptionsText;
    public TMP_Text QuitText;
    public TMP_Text Optionstitle;
    public TMP_Text MainMenuBTNtext;
    public TMP_Text MuteText;
    public AudioSource audioSource;
    public AudioSource FXaudioSource;
    public GameObject OptionsPanel;
    public GameObject MainPanel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Playfirstlevel()
    {
        FXaudioSource.Play();
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGamel()
    {

        FXaudioSource.Play();
        Application.Quit();
    }

    public void OpenOptions()
    {
        FXaudioSource.Play();
        MainPanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }
    public void OpenMenu()
    {
        FXaudioSource.Play();
        OptionsPanel.SetActive(false);
        MainPanel.SetActive(true);
    }
    public void MuteAudio()
    {
        if (spanish == false)
        {
            if (audioSource.mute == false)
            {
                audioSource.mute = true;
                MuteText.SetText("Deactivate Audio");

            }
            else
            {
                audioSource.mute = false;
                MuteText.SetText("Activate Audio");


            }
            if (FXaudioSource.mute == false)
            {
                FXaudioSource.mute = true;
                MuteText.SetText("Deactivate Audio");

            }
            else
            {
                FXaudioSource.mute = false;
                MuteText.SetText("Activate Audio");


            }
        }
        else
        {
            if (audioSource.mute == false)
            {

                audioSource.mute = true;
                MuteText.SetText("Desactivar Audio");
            }
            else
            {

                audioSource.mute = false;
                MuteText.SetText("Activar Audio");
            }
            if (FXaudioSource.mute == false)
            {
                MuteText.SetText("Desactivar Audio");
                FXaudioSource.mute = true;
            }
            else
            {

                FXaudioSource.mute = false;
                MuteText.SetText("Activar Audio");
            }
        }


    }
    public void ChangeLanguage()
    {
        if (spanish == false)
        {
            spanish = true;
            PlayText.SetText("Jugar");
            OptionsText.SetText("Opciones");
            QuitText.SetText("Salir");
            Optionstitle.SetText("Opciones");
            MainMenuBTNtext.SetText("Men� Principal");
            if (audioSource.mute == false)
            {
                MuteText.SetText("Desactivar Audio");
            }
            else
            {
                MuteText.SetText("Activar Audio");
            }

        }
        else
        {
            spanish = false;
            PlayText.SetText("Play");
            OptionsText.SetText("Options");
            QuitText.SetText("Quit");
            Optionstitle.SetText("Options");
            MainMenuBTNtext.SetText("Main Menu");
            if (audioSource.mute == false)
            {
                MuteText.SetText("Deactivate Audio");
            }
            else
            {
                MuteText.SetText("Activate Audio");
            }
        }
        FXaudioSource.Play();
    }

}
