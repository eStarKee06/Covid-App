using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryObserver{
    void update(string statKey, double value);
}
