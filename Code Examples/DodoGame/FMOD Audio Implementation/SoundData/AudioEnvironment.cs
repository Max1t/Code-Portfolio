using System;
using UnityEngine;

[Serializable]
public class AudioEnvironment
{

    [FMODUnity.EventRef] public string Beehive_Spawn;
    [FMODUnity.EventRef] public string Beehive_Loop;

    [FMODUnity.EventRef] public string Berry_Spawn;

    [FMODUnity.EventRef] public string Bush;

    [FMODUnity.EventRef] public string Fuse_Break;
    [FMODUnity.EventRef] public string Fuse_Fix;

    [FMODUnity.EventRef] public string Mushroom_Spawn;

    [FMODUnity.EventRef] public string Pond_Loop;
}
