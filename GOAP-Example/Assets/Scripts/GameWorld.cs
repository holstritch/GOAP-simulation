using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameWorld 
{
    private static readonly GameWorld gameWorldInstance = new GameWorld();
    private static WorldStates world;

    static GameWorld()
    {
        world = new WorldStates();
    }

    private GameWorld()
    {

    }

    public static GameWorld Instance
    {
        get { return gameWorldInstance; }
    }

    public WorldStates GetWorld()
    {
        return world;
    }
}
