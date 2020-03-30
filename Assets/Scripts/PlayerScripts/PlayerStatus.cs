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
    public GameObject healthReportIcon;
    public GameObject playerHealth;

    //double playerHealthHeight;


    private FoodProducts foodManager;
    private HygieneProducts hygieneManager;
    private PreventiveProducts preventiveManager;
    private healthReports healthReportManager;

    private bool wearingMask;
    
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


    int logLines; 
    DayCounter dayCounter;

    double getVirusChance;

    bool infected;
    int infectionDayCount;

    public TMPro.TextMeshProUGUI moneyText;
    public TMPro.TextMeshProUGUI logText;

    GameManager gameManager;
    // Start is called before the first frame update

        
    public GameObject infectedDeathModal;
    public Button infected_resetGame; 
    
    public GameObject winModal;
    public Button win_resetGame;

    public GameObject healthDeathModal;
    public TMPro.TextMeshProUGUI healthDeathText;
    public Button death_resetGame;  

    public GameObject instructionsBG;
    public Button closeIns;
    public Button openIns;


    double topYPosHealthBar;
    public double bottomYPosHealthBar;
    double healthHeight;

    SpriteEnabler spriteEnabler;

    void Start()
    { //logic to load game later
        this.infected = false;

        this.initialStates();

        //Debug.Log(background);
        this.dayCounter = GetComponent<DayCounter>();


        this.foodManager = this.FoodIcon.GetComponent<FoodProducts>();
        this.hygieneManager = this.HygieneIcon.GetComponent<HygieneProducts>();
        this.preventiveManager = this.PreventiveIcon.GetComponent<PreventiveProducts>();
        this.healthReportManager = healthReportIcon.GetComponent<healthReports>();

        //this.playerHealthHeight = this.playerHealth.GetComponent<SpriteRenderer>().sprite.rect.height;
        this.topYPosHealthBar = this.playerHealth.GetComponent<Transform>().position.y;
        Debug.Log(this.topYPosHealthBar);
        this.healthHeight = topYPosHealthBar - this.bottomYPosHealthBar;
        
        this.wearingMask = false;
        this.infectionDayCount = 0;
        //this.logText = this.gameLog.GetComponent<TMPro.TextMeshProUGUI>();
        this.logText.text="";
        this.logLines = 0;


        this.gameManager = GameObject.FindObjectOfType<GameManager>();
        infectedDeathModal.gameObject.SetActive(false);
        winModal.gameObject.SetActive(false);
        healthDeathModal.gameObject.SetActive(false);

        this.spriteEnabler = GameObject.Find("SpriteEnabler").GetComponent<SpriteEnabler>();
        this.spriteEnabler.initialize();
        this.spriteEnabler.disableSprites();

        this.instructionsBG.gameObject.SetActive(true);
        this.closeIns.interactable = true;
        this.openIns.interactable = true;
        this.closeIns.onClick.AddListener(closeInstructions);
        this.openIns.onClick.AddListener(openInstructions);
    }

    // Update is called once per frame
    void Update()
    {
        this.updateTestText();
        this.watchHealth();
        this.updateMoneyText();
        //this.logTextHints();

    }

    void closeInstructions(){
        this.instructionsBG.gameObject.SetActive(false);
        this.spriteEnabler.enableSprites();
        Debug.Log("close instructions");
    }    
    void openInstructions(){
        this.spriteEnabler.disableSprites();
        this.instructionsBG.gameObject.SetActive(true);
        Debug.Log("open instructions");        
    }

    void updatePlayerHealth(double newHunger, double newSleep, double newImmuneSys){
        this.hunger = (newHunger) > 1.0 ? 1.25 :
                                (newHunger) < 0 ? 0 : newHunger;
        this.sleep = (newSleep) > 1.0 ? 1.25 :
                            (newSleep) < 0 ? 0 : newSleep; 
        this.immuneSys = (newImmuneSys > 1.25) ? 1.0 :
                            (newImmuneSys < 0) ? 0 : newImmuneSys;
        
        double newHealth = ((this.hunger * HUNGER_CONTRIBUTION)
                    + (this.sleep * SLEEP_CONTRIBUTION)
                    + (this.immuneSys * IMMUNE_SYS_CONTRIBUTION));

        newHealth =  (newHealth >= 0.99) ? 1.0 : newHealth;
        if(newHealth != this.health){
            if(newHealth > this.health){
                Debug.Log("new health " + newHealth);
                Debug.Log("health " + this.health);
                this.resizeHealth(true, newHealth);
                this.health = newHealth;
            }
            else{
                this.resizeHealth(false, newHealth);
                this.health = newHealth;
            }
            //this.resizeHealth(true, newHealth);
            //this.health = newHealth;   
        }

        if(this.sleep <= 0){
            healthDeathModal.gameObject.SetActive(true);
            healthDeathText.text = "Death caused by lack of sleep";
            death_resetGame.onClick.AddListener(reset);
        }
        if(this.hunger <= 0){
            healthDeathModal.gameObject.SetActive(true);
            healthDeathText.text = "Death caused by starvation";
            death_resetGame.onClick.AddListener(reset);
        }
        if(this.immuneSys <= 0){
            healthDeathModal.gameObject.SetActive(true);
            healthDeathText.text = "Death caused by bacterial infection due to unsanitary conditions";
            death_resetGame.onClick.AddListener(this.reset);
        }
        if(this.health <= 0){
            healthDeathModal.gameObject.SetActive(true);
            healthDeathText.text = "player health reaches 0";
            death_resetGame.onClick.AddListener(this.reset);
        }

    }


    void initialStates(){ //startup Test
        this.health = 1.0;
        this.updatePlayerHealth(1.0, 1.0, 1.0);
        this.money = 8000.00;
    }

    void updateMoneyText(){
        if(("$" + this.money) != this.moneyText.text){
            this.moneyText.text = "$" + this.money;
        }
    }

    public void notifyObservers(string tag){
        this.dayCounter.updateFromPlayer(tag);
        this.foodManager.updateFromPlayer(tag);
        this.hygieneManager.updateFromPlayer(tag);
        this.preventiveManager.updateFromPlayer(tag);
        this.healthReportManager.updateFromPlayer(tag);
    }

    void resizeHealth(bool positiveChange, double newHealth){
        /*if(positiveChange){
            //double yOffset = this.playerHealthHeight - (this.playerHealthHeight * this.health);
            double yOffset = (this.healthHeight * newHealth) - (healthHeight * this.health);
            double yOffsetRatio = yOffset/this.playerHealthHeight;
            Vector3 position = this.playerHealth.GetComponent<Transform>().position;
            position.y = position.y + ((float)position.y * (float)yOffsetRatio);   
            
            Debug.Log("positive:" + yOffsetRatio);
            this.playerHealth.GetComponent<Transform>().position = position;
        }
        else{
             //double yOffset = this.playerHealthHeight - (this.playerHealthHeight * this.health);
             double yOffset = (this.healthHeight * this.health) -(this.healthHeight * newHealth);
             double yOffsetRatio = yOffset/this.healthHeight;
             Vector3 position = this.playerHealth.GetComponent<Transform>().position;
            position.y = position.y - ((float)position.y * (float)yOffsetRatio);
            
            Debug.Log("negative" + yOffsetRatio);
        this.playerHealth.GetComponent<Transform>().position = position;
        }*/
        //this.health.GetComponent<Transform>.position.y = 
        //if(!positiveChange){
            Vector3 currPosVec = this.playerHealth.GetComponent<Transform>().position;
            float newYPos = (float) (this.topYPosHealthBar - (this.healthHeight * (1.0 - newHealth)));
            this.playerHealth.GetComponent<Transform>().position = new Vector3(currPosVec.x, newYPos, currPosVec.z);
                        Debug.Log(this.topYPosHealthBar);
      /*  }
        else{
            Vector3 currPosVec = this.playerHealth.GetComponent<Transform>().position;
            float newYPos = (float) (this.bottomYPosHealthBar + (this.healthHeight * newHealth));
            this.playerHealth.GetComponent<Transform>().position = new Vector3(currPosVec.x, newYPos, currPosVec.z);
            
            Debug.Log(this.topYPosHealthBar);
        }*/
        
    }

    public void watchHealth(){
        if(this.health >= 0.95 || this.wearingMask){
            this.getVirusChance = 0.05;
        }
        else if (this.health >= 0.65){
            this.getVirusChance = 0.40;
        }
        else{
            this.getVirusChance = 0.80;
        }
        Debug.Log("get virus chance " + this.getVirusChance + " " + this.wearingMask);
    }

    public void encounteredVirus(){
        double randomNum = (new System.Random()).Next(0,101);
        if (randomNum <= (this.getVirusChance * 100)){
            this.infected = true;
        }
    }

    void logTextHints(){
        string[] randomSickText = {"I don't feel good today", 
                                    "I might have a slight fever", 
                                            "I feel stuffy", 
                                            "I feel kinda weak...", 
                                            "why is it so cold?"};
        
        double randomIdx = (new System.Random()).Next(0,randomSickText.Length);
        
        double sickTextPicker = (new System.Random()).Next(0,101);

        double chanceOfTextShowing = 10;
        if(this.infected){
            chanceOfTextShowing = 70;
        }

        if(sickTextPicker <= chanceOfTextShowing){
            Debug.Log(randomSickText.Length);
            Debug.Log("random idx: " + randomIdx);
            Debug.Log("sickTextPicker: " + sickTextPicker);
            Debug.Log("chance of showing: " + chanceOfTextShowing);
            this.logText.text += randomSickText[(int)randomIdx];
        }else{
            this.logText.text += "feel great";
        }

    }

    public void updateMoney(double newMoney){
        this.money = (newMoney) < 0 ? 0 : newMoney;
    }
    //listeners------------------------------------------------------------------------
    public void update(string statKey, double value){
        switch(statKey){
            case "SLEEP":   //this.sleep = value;
                            updatePlayerHealth(this.hunger, value, this.immuneSys);
                            break;
            case "HUNGER":  //this.hunger = value;
                            updatePlayerHealth(value, this.sleep, this.immuneSys);
                            break;
            case "IMMUNE_SYS": //this.immuneSys = value;
                            updatePlayerHealth(this.hunger, this.sleep, value);
                            break;
            case "WEAR_MASK":
                            this.wearingMask = true;
                            break;

        }
    }

    void reset(){
        this.gameManager.resetGame();
    }

    public void updateDays(){
        if(this.infected && this.infectionDayCount == 7){
            infectedDeathModal.gameObject.SetActive(true);
            this.infected_resetGame.onClick.AddListener(reset);
        }
        if(this.infected){
            this.infectionDayCount++;
        }
        if(this.dayCounter.getDay() == 100){
            this.winModal.gameObject.SetActive(true);
            this.win_resetGame.onClick.AddListener(reset);
        }
        
        if(this.logLines < 25){
            this.logText.text+= "\n";
            this.logTextHints();
            this.logLines++;
        }else{
            
            this.logText.text = "";
            this.logTextHints();
            this.logLines = 0;
        }

        this.wearingMask = false;
        
        this.updatePlayerHealth(this.hunger - 0.25, this.sleep - 0.25, this.immuneSys - 0.25);
    }

    public void updateFromLocation(string locName){
        switch(locName){
            case "HOSPITAL CHECK IN": 
                    //this.money = this.money - 5000.00; // $500 per day in hospital
                    if(this.money - 5000 >= 0){
                        this.notifyObservers(locName);
                        this.infected = false;
                        this.infectionDayCount = 0;
                        
                    this.updateMoney(this.money - 5000.0);
                    }
                    break;
            case "HOSPITAL CHECK UP": 
                    //activate visuals for a day later
                    if(this.money - 500 >= 0){
                        this.notifyObservers(locName);
                    this.updateMoney(this.money - 500);
                    }
                    break;
            case "GROCERY BUY FOOD":
                    if(this.money - 20 >= 0){
                        this.notifyObservers(locName);
                        this.updateMoney(this.money - 20);
                    }
                    //this.money = this.money - 20;
                    break;
            case "GROCERY BUY SOAP":
                    if(this.money - 60 >= 0){
                        this.notifyObservers(locName);
                        this.updateMoney(this.money - 60);
                    }
                    //this.money = this.money - 60;
                    break;
            case "GROCERY BUY MASK":
                    if(this.money - 40 >= 0){
                        this.notifyObservers(locName);
                        this.updateMoney(this.money - 40);
                    }
                    //this.money = this.money - 40;
                    break;
            case "WORK OUTSIDE":
                    //time offset later implement
                    double newSleep = (this.sleep - 0.20);
                    this.updatePlayerHealth(this.hunger, newSleep, this.immuneSys);
                    this.money = this.money + 200;
                    break;
            case "HOME SLEEP":
                    double newSleep2 = ((this.sleep + 0.50) > 1.0) ? 1.25 : (this.sleep + 0.50);
                    this.updatePlayerHealth(this.hunger, newSleep2, this.immuneSys);
                    //Debug.Log("sleep icon from home: " + this.sleep);
                    this.notifyObservers(locName);
                    break;
            /*case "HOME SHOWER":
                    this.immuneSys = ((this.immuneSys + 0.25) > 1.0) ? 1.25 : (this.immuneSys + 0.25);
                    break;*/
        }

      //  Debug.Log("new money: " + this.money);
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

    public string getHealthReport(){
        double localHealth = (this.health > 1.0) ? 1.0 : this.health; 
        double localHunger = (this.hunger > 1.0) ? 1.0 : this.hunger;
        double localImmuneSys = (this.immuneSys > 1.0) ? 1.0 : this.immuneSys;
        double localSleep = (this.sleep > 1.0) ? 1.0 : this.sleep;
        string returnedString = (" Health " + localHealth * 100 + "% \n Hunger " 
                + (1.0 - localHunger) * 100 + "% \n Hygiene " + localImmuneSys * 100 
                + "% \n Rest " + localSleep * 100 + "%");
        if(this.infected){
            returnedString = returnedString + "\n You tested positive for the C91 Virus \n"+
                                " please check in as soon as possible, others with the same" +
                                " disease pass away after 7 days of infection";
        }

        return returnedString;
    }
}
