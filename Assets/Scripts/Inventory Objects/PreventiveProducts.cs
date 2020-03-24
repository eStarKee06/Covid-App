using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PreventiveProducts : MonoBehaviour, PlayerObserver
{
    List<Preventive> preventiveProducts = new List<Preventive>();
    GameObject player;

    
    public TMPro.TextMeshProUGUI countText; //implement later
    
    void Start()
    {
        this.player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addPreventiveProduct(){
        this.preventiveProducts.Add(new Preventive());
    }

    void usePreventiveProduct(){
        //check later if empty
        //double returnedVal = this.preventiveProducts[this.preventiveProducts.Count - 1].;
        this.preventiveProducts.RemoveAt(this.preventiveProducts.Count - 1);
    }

    public void notifyObservers(){
        /*double currHungerLvl = this.playerStats.getHunger();
        double decHungerBy = this.useFoodProduct();
        double newHungerLvl = ((currHungerLvl + decHungerBy) >= 1) ? 1.0 : (currHungerLvl + decHungerBy);
        (this.playerStats).update("HUNGER", newHungerLvl);*/
    }

    void touchCheck(){
        /*bool touchFoodIcon = false;

        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0); 
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position); 
            if(touch.phase == TouchPhase.Began){ 
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPos);
                if(this.foodObj == touchedCollider){
                    touchFoodIcon = true;
                    Debug.Log("food icon touched");
                }
            }

            if(touch.phase == TouchPhase.Ended){
                this.notifyObservers();
                touchFoodIcon = false;
            }
        }*/
    }

    //------------------TESTS--------------------------------
    void testInitial(){
        for(int i = 0; i < 5; i++){
            this.addPreventiveProduct();
        }
    }

    //listener
    public void updateFromPlayer(string tag){
        switch(tag){
            case "GROCERY BUY MASK":
                    this.addPreventiveProduct();
                    break;
        }
        Debug.Log(this.preventiveProducts.Count);
    }
}
