using System;
using UnityEngine;

[Serializable]
public class MapModel : BaseModel
{
    public string Scene;
    public string Name;
    public string Description;
    public int MaxPlayers;
    public string SceneIcon;
}

public class GameModeModel : BaseModel
{
    public string Name;
    public string Description;
    public int MaxPlayers;
    public string GameModeIcon;
}

public class AttackData : BaseModel
{
    public int Damage = 20;
    public float CoolDown = 0.5f;
    public int AttackForce = 100;
}

public class OverrideAnimatorModel : BaseModel
{
    public string AnimatorControllerForCharacter;
    public string AnimatorControllerForCharacterFirstPerson;
    public string AnimatorControllerForItem;
    public string AnimatorControllerForItemFirstPerson;
}

public class SkinContainer : BaseModel
{
    public string Name;
    public string Mesh;
    public string Material;
    public string firtPersonMesh;
    public string firstPresonMaterial;
}

public enum WeaponSlotType
{
    Melee,
    Primary,
    Secondary,
    Grenades,
    Special
}

public enum FireMode
{
    Automatic,
    Single,
    SemiAuto
}

public enum SlotType
{
    Normal,
    PocketItem,
}

public class RecoilData : BaseModel
{
    public int MaxCrossHairSize = 256;
    public int MinCrosshairSize = 64;
    public bool HideCrosshairWhenAiming = true;
    public float RecoilMinAngle;
    public float RecoilMaxAngle;
    public float RecoilScopeMultiplier;
    public float RecoilAngleAddedOnShot;
    public float RecoilStabilizationSpeed;
    public float RecoilWalkMultiplier = 1.5f;
    public float RecoilY = 0.05f;
    public float RecoilX;
    public float ModelRecoil = 0.05f;
    public bool ChangeSensitivityOnScope = false;
    public float FovLerpSpeed = 20;
    public float ModelLerpSpeed = 2;
    public float ScopeFov = 50;
}

public class ItemModel : BaseModel
{
    public string Name;
    public WeaponSlotType WeaponSlotType;
    public FireMode Firemode = FireMode.Automatic;
    public SlotType SlotType = SlotType.Normal;
    public Vector3 FirstPersonModelOffset;
    public string TakeClip;
    public bool CanBeDropped = true;
    public float TakingTime = 0.1f;

    public int MaxAmmoSupply = 100;
    public int MagazineCapacity = 30;

    public float Speed;
    public float Duration;

    public string ItemIcon;
    public string ItemUI;

    public int RangeAttackModel;
    public int MelleAttackModel;
    public int RecoilDataModel;
    public int OverrideAnimatorData;
    public int[] CompatibleClasses;
}