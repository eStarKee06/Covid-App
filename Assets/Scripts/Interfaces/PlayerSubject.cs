using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerSubject{
    void notifyObservers(string tag);
}