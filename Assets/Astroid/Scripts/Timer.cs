using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] float timeRemaining = 10,totalTime;
    [SerializeField] bool timerIsRunning = false;
    [SerializeField] Image _timerImage;

    private void Awake()
    {
        
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                _timerImage.fillAmount = timeRemaining/ totalTime;
              
            }
            else
            {
                if (timerIsRunning) {
                    gameController.StopedTimer();
                    timeRemaining = 0;
                    timerIsRunning = false;
                    _timerImage.gameObject.SetActive(false);
                }
               

            }
        }
    }
  
    public void StartTimer(float time) {
        totalTime = time;
        timeRemaining = time;
        timerIsRunning = true;
        _timerImage.gameObject.SetActive(true);
    }
}