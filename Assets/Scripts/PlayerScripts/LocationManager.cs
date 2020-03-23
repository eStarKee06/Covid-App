using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    string[] locations = {"HOME", "GROCERY", "WORK", "HOSPITAL"};
    string currentLocation;
    // Start is called before the first frame update

    PlayerStatus playerStats;
    void Start()
    {
        this.currentLocation = locations[0];
        this.playerStats = GetComponent<PlayerStatus>();
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
        this.checkForVirusEncounter();
        Debug.Log("switched location: " + this.currentLocation);
    }

    public string getCurrLocation(){
        return this.currentLocation;
    }
}
