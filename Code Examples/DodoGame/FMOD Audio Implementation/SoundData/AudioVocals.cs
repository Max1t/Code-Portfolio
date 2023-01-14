using System;
using UnityEngine;

[Serializable]
public class AudioVocals
{
    [FMODUnity.EventRef] public string Dodo_Eat;
    [FMODUnity.EventRef] public string Dodo_Happy;
    [FMODUnity.EventRef] public string Dodo_Idle;
    [FMODUnity.EventRef] public string Dodo_In_Danger;

    [FMODUnity.EventRef] public string Player_Pickup;
    [FMODUnity.EventRef] public string Player_Putdown;


    [FMODUnity.EventRef] public string Raptor_BasicAttack;
    [FMODUnity.EventRef] public string Raptor_Jump;
    [FMODUnity.EventRef] public string Raptor_JumpAttack;
    [FMODUnity.EventRef] public string Raptor_Inspecting;
    [FMODUnity.EventRef] public string Raptor_Preying;
    [FMODUnity.EventRef] public string Raptor_Attack_Attack_Ready;

}
