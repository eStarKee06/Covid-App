using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FoodProducts : MonoBehaviour, InventorySubject, PlayerObserver
{
    List<Food> foodProducts = new List<Food>();
    GameObject player;
    PlayerStatus playerStats;

    Collider2D foodObj;

    public TMPro.TextMeshProUGUI countText;
    
    bool touchFoodIcon = false;

    void Start()
    {
        this.player = GameObject.Find("Player");
        this.playerStats = this.player.GetComponent<PlayerStatus>();
        this.foodObj = GetComponent<Collider2D>();
        this.touchFoodIcon = false;
        
        this.countText.text = this.foodProducts.Count + "x";
        //-----------------------
        //this.testInitial();
    }

    // Update is called once per frame
    void Update()
    {
       
            this.touchCheck();
    }

    void addFoodProduct(){
        this.foodProducts.Add(new Food());
        
        this.countText.text = this.foodProducts.Count + "x";
    }

    double useFoodProduct(){
        //check later if empty
        double returnedVal = this.foodProducts[this.foodProducts.Count - 1].HUNGER_IMPACT;
        this.foodProducts.RemoveAt(this.foodProducts.Count - 1);
        
        this.countText.text = this.foodProducts.Count + "x";
        return returnedVal;

    }

    public void notifyObservers(){
        double currHungerLvl = this.playerStats.getHunger();
        double decHungerBy = this.useFoodProduct();
        
        //Debug.Log("current hunger " + currHungerLvl);
        //Debug.Log("dec hunger " + decHungerBy);
        double newHungerLvl = ((currHungerLvl + decHungerBy) > 1.0) ? 1.25 : (currHungerLvl + decHungerBy);
        (this.playerStats).update("HUNGER", newHungerLvl);
        //Debug.Log("Food notify end");
    }

    void touchCheck(){

        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0); 
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position); 
            if(touch.phase == TouchPhase.Began){ 
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos);
                if(this.foodObj == touchedCollider){
                    this.touchFoodIcon = true;
                    //Debug.Log("food icon touched");
                }
            }

            if(touch.phase == TouchPhase.Ended){
                if(this.touchFoodIcon){
                    this.notifyObservers();
                    this.touchFoodIcon = false;
                }
            }
        }
    }

    //------------------TESTS--------------------------------
    void testInitial(){
        for(int i = 0; i < 5; i++){
            this.addFoodProduct();
        }
    }

    //listener
    public void updateFromPlayer(string tag){
        switch(tag){
            case "GROCERY BUY FOOD":
                    this.addFoodProduct();
                    break;
        }
        //Debug.Log("food count " + this.foodProducts.Count);
    }
}
