using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
public class EncounterChance : MonoBehaviour
{
    double[] ENCOUNTER_CHANCES = {0.30, 0.45, 0.60, 0.75}; 
    
    public double randomPickEncounter(){
        System.Random random = new System.Random();
        int randomIdx  = random.Next(0, ENCOUNTER_CHANCES.Length);

        return ENCOUNTER_CHANCES[randomIdx];
        //return 1.0;
    }
}
