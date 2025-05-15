using UnityEngine;

public enum ColorType
{
    Red,
    Green,
    Blue,
    None
}

public enum WeaponGrade
{
    Common,
    Rare,
    Epic,
    Legendary
}

public enum WeaponType
{
    Bow,
    CrossBow,
    Dagger,
    Staff
}

public static class GameConstants
{
    public const float SameColorBonusMultiplier = 1.3f;
    public const float DifferentColorPenaltyMultiplier = 0.8f;
    public const int MaxWeaponSlots = 3;
    public const int MaxPassiveSlots = 5;
}