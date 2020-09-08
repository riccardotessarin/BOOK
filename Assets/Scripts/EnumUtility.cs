using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EnumUtility {
    public enum CharacterType {
        Genee,
        Rayaz,
        Ryuyuki
    } // Also used for plant types


    public enum PageType {
        BlindingCloud,
        VenomousNeedle,
        FirePillar,
        Fireball,
        IceStalagmite,
        BodyFreeze,
        WaterShield,
        SurgingTide,
        LightningSpeed,
        ElectricalDischarge
    }

    public enum AttackType : short {
        Neutral,
        Basilisk, //toxic
        Inferno, //fire
        Niflheim, //ice
        Neptunian, //water
        Raijin, //electric}
        Nothing
    }

    public enum PlayerClass {
        RayazPlayer,
        GeneePlayer,
        RyuyukiPlayer
    }
}