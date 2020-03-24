using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.UI;
using TMPro;
public class PlayerStatus : MonoBehaviour, InventoryObserver, 
TimeObserver, PlayerSubject, LocationObserver{

    public GameObject FoodIcon;
    public GameObject HygieneIcon;
    public GameObject PreventiveIcon;
    private FoodProducts foodManager;
    private HygieneProducts hygieneManager;
    private PreventiveProducts preventiveManager;
    
    
    //health
    private double health;
    private double hunger;
    private double sleep;
    private double immuneSys;

    private double HUNGER_CONTRIBUTION = 0.33;
    private double SLEEP_CONTRIBUTION = 0.33;
    private double IMMUNE_SYS_CONTRIBUTION = 0.33;
 
    //money
    private double money;

    //text for test
    public Text healthText;
    public Text hungerText;
    public Text sleepText;
    public Text immuneSysText;

    DayCounter dayCounter;

    double getVirusChance;

    bool infected;

    public TMPro.TextMeshProUGUI moneyText;
    // Start is called before the first frame update
    void Start()
    { //logic to load game later
        this.infected = false;

        this.initialStates();

        //Debug.Log(background);
        this.dayCounter = GetComponent<DayCounter>();


        this.foodManager = this.FoodIcon.GetComponent<FoodProducts>();
        this.hygieneManager = this.HygieneIcon.GetComponent<HygieneProducts>();
        this.preventiveManager = this.PreventiveIcon.GetComponent<PreventiveProducts>();

    }

    // Update is called once per frame
    void Update()
    {
        this.updateTestText();
        this.watchHealth();
        this.updateMoneyText();
    }

    void updatePlayerHealth(double newHunger, double newSleep, double newImmuneSys){
        this.hunger = newHunger;
        this.sleep = newSleep;
        this.immuneSys = newImmuneSys;
        this.health = ((this.hunger * HUNGER_CONTRIBUTION)
                    + (this.sleep * SLEEP_CONTRIBUTION)
                    + (this.immuneSys * IMMUNE_SYS_CONTRIBUTION));
    }

    void initialStates(){ //startup Test
        this.updatePlayerHealth(1.0, 1.0, 1.0);
        //this.money = 3000.00;
        this.money = 8000.00;
    }

    void updateMoneyText(){
        Debug.Log("asdfasdf");
        if(("$" + this.money) != this.moneyText.text){
            this.moneyText.text = "$" + this.money;
        }
    }

    public void notifyObservers(string tag){
        this.dayCounter.updateFromPlayer(tag);
        this.foodManager.updateFromPlayer(tag);
        this.hygieneManager.updateFromPlayer(tag);
        this.preventiveManager.updateFromPlayer(tag);
    }

    public void watchHealth(){
        if(this.health >= 0.95){
            this.getVirusChance = 0.10;
        }
        else if (this.health >= 0.65){
            this.getVirusChance = 0.40;
        }
        else{
            this.getVirusChance = 0.80;
        }
    }

    public void encounteredVirus(){
        double randomNum = (new System.Random()).Next(0,101);
        Debug.Log("immunity to disease: " + this.getVirusChance);
        Debug.Log("random num: " + randomNum);
        if (randomNum <= (this.getVirusChance * 100)){
            this.infected = true;
            //set this to false if we check into the hospital 
            Debug.Log("PLAYER IS INFECTED BY VIRUS!!!!!");
        }
    }
    //listeners------------------------------------------------------------------------
    public void update(string statKey, double value){
        switch(statKey){
            case "SLEEP":   this.sleep = value;
                            break;
            case "HUNGER":  this.hunger = value;
                            break;
            case "IMMUNE_SYS": this.immuneSys = value;
                                break;

        }
        Debug.Log(statKey);
        Debug.Log("sleep: " + this.sleep + "\n hunger: " + this.hunger + 
        "\n immune: " + this.immuneSys + "\n health: " + this.health + 
        "\n money " + this.money);
    }

    public void updateDays(){
        this.updatePlayerHealth(this.hunger - 0.25, this.sleep - 0.25, this.immuneSys - 0.25);
    }

    public void updateFromLocation(string locName){
        switch(locName){
            case "HOSPITAL CHECK IN": 
                    this.notifyObservers(locName);
                    this.money = this.money - 5000.00; // $500 per day in hospital
                    break;
            case "HOSPITAL CHECK UP": 
                    //activate visuals for a day later
                    this.money = this.money - 500;
                    break;
            case "GROCERY BUY FOOD":
                    this.notifyObservers(locName);
                    this.money = this.money - 20;
                    break;
            case "GROCERY BUY SOAP":
                    this.notifyObservers(locName);
                    this.money = this.money - 60;
                    break;
            case "GROCERY BUY MASK":
                    this.notifyObservers(locName);
                    this.money = this.money - 40;
                    break;
            case "WORK OUTSIDE":
                    //time offset later implement
                    this.money = this.money + 200;
                    break;
            case "HOME SLEEP":
                    this.sleep = ((this.sleep + 0.50) > 1.0) ? 1.25 : (this.sleep + 0.50);
                    Debug.Log("sleep icon from home: " + this.sleep);
                    this.notifyObservers(locName);
                    break;
            /*case "HOME SHOWER":
                    this.immuneSys = ((this.immuneSys + 0.25) > 1.0) ? 1.25 : (this.immuneSys + 0.25);
                    break;*/
        }

        Debug.Log("new money: " + this.money);
    }
    //getters--------------------------------------------------------
    public double getHunger(){
        return this.hunger;
    }

    public double getSleep(){
        return this.sleep;
    }

    public double getImmuneSys(){
        return this.immuneSys;
    }

    //test------------------------------------------------------------------------------
    void updateTestText(){
        this.healthText.text = "Health " + this.health * 100 + "%";
        this.hungerText.text = "Fullness " + this.hunger * 100 + "%";
        this.immuneSysText.text = "Immune " + this.immuneSys * 100 + "%"; 
        this.sleepText.text = "Rest " + this.sleep * 100 + "%";
    }
}
