using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayCounter : MonoBehaviour, TimeSubject, PlayerObserver
{
    //public Text dayCounterText;
    //public Text timerInDayText;

    public TMPro.TextMeshProUGUI dayCounterText;
    public TMPro.TextMeshProUGUI timerInDayText;

    private float startTime;
    private int dayCounter;

    GameObject player;
    PlayerStatus playerStats;
    
    // Start is called before the first frame update
    void Start()
    {
        this.resetInitTime();
        this.dayCounter = 0;
        
        this.player = GameObject.Find("Player");
        this.playerStats = this.player.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        this.updateTime();
        if(this.timerInDayText.text == "0:10"){
            /*this.notifyObservers();
            this.updateDateCounter(1);
            this.resetInitTime();*/
            this.eventHandler(1, true);
        }
    }

    void resetInitTime(){
        this.startTime = Time.time;
    }

    void updateTime(){
        float currTime = Time.time - this.startTime;
        string minutes = ((int) currTime/60).ToString();
        string seconds = (currTime % 60).ToString("f0");
        if(seconds.Length == 1) seconds = "0" + seconds;

        //string timeString= "Time: " + minutes + ":" + seconds;
        string timeString = minutes + ":" + seconds;

        this.timerInDayText.text = timeString;
    }

    void updateDateCounter(int inc){
        this.dayCounter+=inc;
        this.dayCounterText.text = "Day " + this.dayCounter;
    }

    public void notifyObservers(){
        this.playerStats.updateDays();
    }

    public void eventHandler(int inc, bool updateStats){
        //this.dayCounter = this.dayCounter + 10;
        //this.dayCounterText.text = "Day " + this.dayCounter;
        if(updateStats){
            this.notifyObservers();
        }
        this.updateDateCounter(inc);
        this.resetInitTime();
    }

    public void updateFromPlayer(string tag){
        switch(tag){
            case "HOSPITAL CHECK IN" : 
                this.eventHandler(10, false);
                break;
            case "HOME SLEEP" : 
                this.eventHandler(1, true);    
                break;
        }
    }

    //-----------------getters-------------------
    public float getTime(){
        return Time.time - this.startTime;
    }

    public int getDay(){
        return this.dayCounter;
    }
    
}
