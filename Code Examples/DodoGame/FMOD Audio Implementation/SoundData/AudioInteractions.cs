using System;
using UnityEngine;

[Serializable]
public class AudioInteractions
{
    [Header("Dodo")]
    [FMODUnity.EventRef] public string Dodo_Fall_Water;
    [FMODUnity.EventRef] public string Dodo_Drown;

    [Header("Player")]
    [FMODUnity.EventRef] public string Pet;
    [FMODUnity.EventRef] public string PickUp;
    [FMODUnity.EventRef] public string Pour_Food;
    [FMODUnity.EventRef] public string Pour_Water;
    [FMODUnity.EventRef] public string Putdown;
    [FMODUnity.EventRef] public string Take_Food;
    [FMODUnity.EventRef] public string Take_Water;
    [FMODUnity.EventRef] public string Trashcan;
}

