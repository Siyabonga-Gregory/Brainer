using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public QuestionsManager questionMan;
    public AudioManager audioManager;
    public bool LevelComplete;
    public GameObject[] topMenu;

    public void ReloadGame(){

        /*questionMan.countDown.SetActive(true);
        questionMan.countDownSum = 4;
        questionMan.countDown.gameObject.GetComponent<Animator>().enabled = true;
        questionMan.countDown.gameObject.GetComponent<Animator>().SetBool("Play", true);
        questionMan.countDownText.text = questionMan.countDownSum.ToString();
        StartCoroutine(questionMan.CountDown());*/
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UnlockLevel()
    {
        this.LevelComplete = false;
    }
    public void PlaySound(int index)
    {
        audioManager.PlayAudioClip(index, this.transform);
    }

    public void Login(bool status){
        if (topMenu.Length > 0)
        {
            for (int i = 0; i < topMenu.Length; i++)
            {
                topMenu[i].SetActive(status);
            }
        }
    }


}
