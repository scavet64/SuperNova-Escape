using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    public void loadLevel(string name) {
        LevelManager.instance.StartLoading(name);
    }

}
