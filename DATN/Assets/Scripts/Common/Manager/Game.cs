using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Manager {

    public GameLoader gameLoader;

    public static Game Instance
    {
        get
        {
            return GameManager.Instance.GetManager<Game>() as Game;
        }
    }

    public GameObject content;
    public GameObject Player;
}
