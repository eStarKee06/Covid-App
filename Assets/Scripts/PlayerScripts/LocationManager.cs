using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LocationManager : MonoBehaviour
{
    string[] locations = {"HOME", "GROCERY", "WORK", "HOSPITAL"};
    string currentLocation;


    public GameObject homePanel;
    public GameObject workPanel;
    public GameObject groceryPanel;
    public GameObject hospitalPanel;


    public TMPro.TextMeshProUGUI locationLabel;

    GameObject[] locationPanels = new GameObject[4];
    int activePanelIdx;
    // Start is called before the first frame update

    PlayerStatus playerStats;
    void Start()
    {
        this.currentLocation = locations[0];
        this.playerStats = GetComponent<PlayerStatus>();

        this.homePanel.SetActive(true);
        this.workPanel.SetActive(false);
        this.groceryPanel.SetActive(false);
        this.hospitalPanel.SetActive(false);
        this.activePanelIdx = 0;
        //this.locationPanels = {this.homePanel, this.groceryPanel, this.workPanel, this.hospitalPanel};

        this.locationLabel.text = currentLocation;
        this.locationPanels[0] = this.homePanel;
        this.locationPanels[1] = this.groceryPanel;
        this.locationPanels[2] = this.workPanel;
        this.locationPanels[3] = this.hospitalPanel;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void checkForVirusEncounter(){
        if(this.currentLocation == "GROCERY" || this.currentLocation == "WORK"){
            double limit = (new EncounterChance()).randomPickEncounter() * 100;

            double randomNum = (new System.Random()).Next(0,101);

            //Debug.Log("encounter chance: " + limit);
            //Debug.Log("shuffle chance: " + randomNum);
            if( randomNum <= limit){ 
                this.playerStats.encounteredVirus();
            }
        }
    }
    public void switchLocation(int locationIndex){
        this.currentLocation = locations[locationIndex];

        this.locationLabel.text = currentLocation;

        this.locationPanels[this.activePanelIdx].SetActive(false);
        this.locationPanels[locationIndex].SetActive(true);
        this.activePanelIdx = locationIndex;


        this.checkForVirusEncounter();
        Debug.Log("switched location: " + this.currentLocation);
    }

    public string getCurrLocation(){
        return this.currentLocation;
    }
}
