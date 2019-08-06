using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;


public class UIManager : MonoBehaviour {

    public RectTransform mainPanel, carShop, gameOver, questionPopUp,loginPopUp,alertPopUp;
    public Image playBtn;
	public Button[]buttons;
    public QuestionsManager qManager;
    public GameManager gameManager;

    private void Start()
    {
        FadeEffect(playBtn, 0.5f, 1.5f, 1.5f);
        MoveUI(mainPanel, new Vector2(0, 0), 0.25f, 0f, Ease.OutFlash);
    }



    public void ButtonMethod(string value)
    {
        switch (value)
        {
		case "CarShopBtn":
			MoveUI (mainPanel, new Vector2 (-800, 0), 0.25f, 0f, Ease.OutFlash);
			MoveUI (carShop, new Vector2 (0, 0), 0.25f, 0.25f, Ease.OutFlash);
			EnableOrDisableBUttons (true);
            break;
            case "ShowAlert":
                MoveUI(mainPanel, new Vector2(-800, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(alertPopUp, new Vector2(0, 0), 0.25f, 0.25f, Ease.OutFlash);
                EnableOrDisableBUttons(true);
                break;

            case "ShowLogin":
                MoveUI(mainPanel, new Vector2(-800, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(loginPopUp, new Vector2(0, 0), 0.25f, 0.25f, Ease.OutFlash);
                EnableOrDisableBUttons(true);
                gameManager.PlaySound(2);
                break;

            case "ShowGameOver":
			MoveUI (mainPanel, new Vector2 (-800, 0), 0.25f, 0f, Ease.OutFlash);
			MoveUI (gameOver, new Vector2 (0, 0), 0.25f, 0.25f, Ease.OutFlash);
			EnableOrDisableBUttons (true);
                gameManager.PlaySound(2);
                break;

         case "ShowQuestion":
                MoveUI(mainPanel, new Vector2(-800, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(questionPopUp, new Vector2(0, 0), 0.25f, 0.25f, Ease.OutFlash);
                EnableOrDisableBUttons(true);
                gameManager.PlaySound(2);
                StartCoroutine(qManager.CountDownAnimation());
                break;

         case "CarShopBackBtn":
			MoveUI (carShop, new Vector2 (0, -1200), 0.25f, 0f, Ease.OutFlash);
			MoveUI (mainPanel, new Vector2 (0, 0), 0.25f, 0.25f, Ease.OutFlash);
			EnableOrDisableBUttons (false);
            break;

		case "HideGameOver":
			MoveUI (gameOver, new Vector2 (0, -1200), 0.25f, 0f, Ease.OutFlash);
			MoveUI (mainPanel, new Vector2 (0, 0), 0.25f, 0.25f, Ease.OutFlash);
			EnableOrDisableBUttons (false);
               // gameManager.PlaySound(2);
                break;
            case "HideLogin":
                MoveUI(loginPopUp, new Vector2(0, -1200), 0.25f, 0f, Ease.OutFlash);
                MoveUI(mainPanel, new Vector2(0, 0), 0.25f, 0.25f, Ease.OutFlash);
                EnableOrDisableBUttons(false);
               // gameManager.PlaySound(2);
                break;
            case "HideQuestion":
                MoveUI(questionPopUp, new Vector2(0, -1200), 0.25f, 0f, Ease.OutFlash);
                MoveUI(mainPanel, new Vector2(0, 0), 0.25f, 0.25f, Ease.OutFlash);
                EnableOrDisableBUttons(false);
                //gameManager.PlaySound(2);
                break;
            case "HideAlert":
                MoveUI(alertPopUp, new Vector2(0, -1200), 0.25f, 0f, Ease.OutFlash);
                MoveUI(mainPanel, new Vector2(0, 0), 0.25f, 0.25f, Ease.OutFlash);
                EnableOrDisableBUttons(false);
               // gameManager.PlaySound(2);
                break;
        }
    }

    void FadeEffect(Image _image, float fadeTo, float fadeDuration, float delay)
    {
        _image.DOFade(fadeTo, fadeDuration);
        _image.DOFade(1, fadeDuration).SetDelay(delay).OnComplete(() => FadeEffect(_image, fadeTo, fadeDuration, delay));
    }

    void MoveUI(RectTransform _traansform, Vector2 position, float moveTime, float delayTime, Ease ease)
    {
        _traansform.DOAnchorPos(position, moveTime).SetDelay(delayTime).SetEase(ease);
    }

	void EnableOrDisableBUttons(bool status)
	{
        if (buttons.Length > 0){
            for (int i = 0; i < buttons.Length; i++){
                buttons[i].interactable = status;
            }
        }
       
	}

    public IEnumerator ManageAlertPopUp(bool correct)
    {
        if (correct)
        {
            yield return new WaitForSeconds(0.5f);
            qManager.coinAnim.SetActive(true);
            qManager.coinAnim.GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(0.85f);
            qManager.score.text = (qManager.currentScore + 1).ToString();
            yield return new WaitForSeconds(0.1f);
        }
        else {
            yield return new WaitForSeconds(1.4f);
        }

        ButtonMethod("HideAlert");
       // ButtonMethod("ShowQuestion");
        yield return new WaitForSeconds(0.2f);

        qManager.coinAnim.SetActive(false); ;
        Debug.Log("A   counter   " + qManager.counter + "    questions.Length   " + qManager.questions.Length);
        if (qManager.counter != qManager.questions.Length )
        {
            ButtonMethod("ShowQuestion");
            qManager.scoreIncrementer.text = qManager.counter + "/" + (qManager.questions.Length - 1);

        }
        else
        {
            Debug.Log("B   counter   " + qManager.counter + "    questions.Length   " + qManager.questions.Length);
            StopAllCoroutines();
            StartCoroutine(LevelUp());
        }
       
       

    }

    public IEnumerator LevelUp()
    {
        ButtonMethod("HideQuestion");
        yield return new WaitForSeconds(0.5f);
        qManager.levelUpAnim.SetActive(true);
        qManager.levelUpAnim.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(2.3f);
        ButtonMethod("ShowQuestion");
        yield return new WaitForSeconds(0.2f);
        int currentLevel = int.Parse(qManager.level.text.ToString());
        qManager.level.text = (currentLevel + 1).ToString();
        qManager.levelUpAnim.SetActive(false);

    }
}
