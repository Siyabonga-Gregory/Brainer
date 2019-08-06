using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class QuestionsManager : MonoBehaviour
{
    public string[] questions;
    public Sprite [] questionImages;
    public bool  [] questionAnswer;
    public bool  [] questionStatus;

    public Text questionText;
    public Image questionImage;
    public Sprite[] alertImages;
    public Text[] alertText;
    public Image alertImage;
    public Text score;
    public Text level;
    public int currentScore;
    public Text scoreIncrementer;
    public int counter=1;

    public Text countDownText;

    int currentQuestionIndex;
    public UIManager uiMan;

    public GameObject countDown;
    public int countDownSum = 15;
    public bool CountDownAnimationCancel = false;
    public GameObject coinAnim;
    public GameObject levelUpAnim;


    void Start()
    {
        Random.Range(0, questions.Length);
       // RandomizeQuestions();
        //LoadNextQuestion();
        uiMan.ButtonMethod("ShowLogin");
        scoreIncrementer.text = "1/" + (questions.Length - 1);
  
    }

    public void LoadNextQuestion()// Loading next question
    {
        questionText.text = questions[currentQuestionIndex];
        questionImage.sprite = questionImages[currentQuestionIndex];
    }


    int GetQuestionIndexByQuestionName(string question){for(int i =0;i<questions.Length;i++){if (question.ToString().Equals(questions[i].ToString())){return i;}}return -1;}

    bool Status(int questionIndex){return questionStatus[questionIndex];}

   public void RandomizeQuestions()
    {
        if (CanRandomize())
        {
            bool done = false;
            while (!done){
                currentQuestionIndex = Random.Range(0, questions.Length);

                if (!Status(currentQuestionIndex)){
                    done = true;
                }
                else {
                    //  Debug.Log("Question Index Has Been Asked  " + currentQuestionIndex);
                }
            }

            countDownSum = 15;
        }
        else
        {
            Debug.Log("Can't randomize now");
            uiMan.gameManager.LevelComplete = true;
            CountDownAnimationCancel = true;
            StopAllCoroutines();
            StartCoroutine(uiMan.LevelUp());
        }
    }

    bool CanRandomize(){
        for(int i=0;i<questionStatus.Length-1;i++)
        {
            if (!questionStatus[i])
            {
                i = questionStatus.Length;
                return true;
            }
        }
        return false;
    }


    public void SubmitAndMarkQuestion(bool answer)
    {
        CountDownAnimationCancel = true;
        questionStatus[currentQuestionIndex] = true;
        currentScore = int.Parse(score.text.ToString());
          StopCoroutine(CountDownAnimation());
        if (CanRandomize())
        {
           // alertText[1].enabled = false;
           // alertText[0].enabled = true;
           // alertText[0].text = "CORRECT";
            alertImage.sprite = alertImages[0];
            if (questionAnswer[currentQuestionIndex] && answer)// correct answer
            {
                
                StopCoroutine(CountDownAnimation());
                uiMan.gameManager.PlaySound(0);
                counter += 1;
                //score.text = (currentScore + 1).ToString();
                uiMan.ButtonMethod("HideQuestion");
                countDown.SetActive(false);
                countDown.gameObject.GetComponent<Animator>().enabled = false;
                countDown.gameObject.GetComponent<Animator>().SetBool("Play", false);
                uiMan.ButtonMethod("ShowAlert");
                StartCoroutine(uiMan.ManageAlertPopUp(true));
            }
            else if (!questionAnswer[currentQuestionIndex] && !answer)// correct answer
            {
                StopCoroutine(CountDownAnimation());
                uiMan.gameManager.PlaySound(0);
                counter += 1;
                //score.text = (currentScore + 1).ToString();
                uiMan.ButtonMethod("HideQuestion");
                countDown.SetActive(false);
                countDown.gameObject.GetComponent<Animator>().enabled = false;
                countDown.gameObject.GetComponent<Animator>().SetBool("Play", false);
                uiMan.ButtonMethod("ShowAlert");
                StartCoroutine(uiMan.ManageAlertPopUp(true));
            }
            else
            {
                StopCoroutine(CountDownAnimation());
                uiMan.gameManager.PlaySound(1);
              //  alertText[0].enabled = false;
              //  alertText[1].enabled = true;
              //  alertText[1].text = "INCORRECT";
                alertImage.sprite = alertImages[Random.Range(1, 2)];
                counter += 1;
                //StopAllCoroutines();
                uiMan.ButtonMethod("HideQuestion");
                countDown.SetActive(false);
                countDown.gameObject.GetComponent<Animator>().enabled = false;
                countDown.gameObject.GetComponent<Animator>().SetBool("Play", false);
                uiMan.ButtonMethod("ShowAlert");
                StartCoroutine(uiMan.ManageAlertPopUp(false));
            }
            
           

        }
        else
        {
            uiMan.ButtonMethod("HideQuestion");
            countDown.SetActive(false);
            countDown.gameObject.GetComponent<Animator>().enabled = false;
            countDown.gameObject.GetComponent<Animator>().SetBool("Play", false);
            uiMan.ButtonMethod("ShowLevelUp");
            uiMan.gameManager.PlaySound(2);
        }
    }

    public void ShowPopUp(bool status)
    {
       
    }

    IEnumerator TypeQuestion()
    {
        bool done = false;
        StreamReader reader = new StreamReader(questions[0].ToString());    
        while (!done){ yield return new WaitForSeconds(0.5f);}
    }

    public IEnumerator CountDownAnimation()
    {
        countDown.SetActive(false);
        countDown.gameObject.GetComponent<Animator>().enabled = false;
        countDown.gameObject.GetComponent<Animator>().SetBool("Play", false);
       
        
        yield return new WaitForSeconds(5f);
        Debug.Log("Starting New Instance   CountDownAnimationCancel   " + CountDownAnimationCancel);

        if (!CountDownAnimationCancel)
        {
            countDown.SetActive(true);
            countDown.gameObject.GetComponent<Animator>().enabled = true;
            countDown.gameObject.GetComponent<Animator>().SetBool("Play", true);
            countDownText.text = countDownSum.ToString();

            StartCoroutine(CountDown());
        }
        else
        {
            StopCoroutine(CountDownAnimation());
            StopCoroutine(CountDown());
            Debug.Log("Restarting");
            CountDownAnimationCancel = false;
            //StartCoroutine(CountDownAnimation());
        }

       
    }

    public IEnumerator CountDown()
    {
        if (countDown.gameObject.activeSelf)
        {
            yield return new WaitForSeconds(1.9f);

            if (!CountDownAnimationCancel)
            {
                if (countDownSum > 0 && countDown.gameObject.activeSelf)
                {
                    countDownSum -= 1;
                    countDownText.text = countDownSum.ToString();
                    if (!CountDownAnimationCancel)
                    {
                        StartCoroutine(CountDown());
                    }
                    else
                    {
                        StopCoroutine(CountDown());
                    }
                }
                else if (countDownSum > 0 && !countDown.gameObject.activeSelf)
                {
                    Debug.Log("Stoped B");

                }
                else if (countDownSum == 0)
                {
                    StopAllCoroutines();
                    uiMan.ButtonMethod("HideQuestion");
                    countDown.SetActive(false);
                    countDown.gameObject.GetComponent<Animator>().enabled = false;
                    countDown.gameObject.GetComponent<Animator>().SetBool("Play", false);
                    uiMan.ButtonMethod("ShowGameOver");
                }
            }
            else
            {
                StopCoroutine(CountDownAnimation());
                StopCoroutine(CountDown());
            } 
        }
        else
        {
            StopCoroutine(CountDownAnimation());
            StopCoroutine(CountDown());
        }
    }



}
