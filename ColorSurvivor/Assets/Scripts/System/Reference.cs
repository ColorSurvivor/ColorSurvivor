using UnityEngine;

public enum ColorType
{
    None = 0,
    Red = 1,
    Green = 2,
    Blue = 3
}

public enum WeaponGrade
{
    Common = 0,
    Rare = 1,
    Epic = 2,
    Legendary = 3
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