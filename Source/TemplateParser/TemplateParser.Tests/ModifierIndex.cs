namespace TemplateParser.Tests
{
    /// <summary>
    /// Represents item modifiers
    /// </summary>
    public enum ModifierIndex
    {
        ///<summary>
        /// Icy Axe Haft
        ///</summary>
        IcyAxeHaft = 1,
        ///<summary>
        /// Ebon Axe Haft
        ///</summary>
        EbonAxeHaft = 2,
        ///<summary>
        /// Shocking Axe Haft
        ///</summary>
        ShockingAxeHaft = 3,
        ///<summary>
        /// Fiery Axe Haft
        ///</summary>
        FieryAxeHaft = 4,
        ///<summary>
        /// Icy Bow String
        ///</summary>
        IcyBowString = 5,
        ///<summary>
        /// Ebon Bow String
        ///</summary>
        EbonBowString = 6,
        ///<summary>
        /// Shocking Bow String
        ///</summary>
        ShockingBowString = 7,
        ///<summary>
        /// Fiery Bow String
        ///</summary>
        FieryBowString = 8,
        ///<summary>
        /// Icy Hammer Haft
        ///</summary>
        IcyHammerHaft = 9,
        ///<summary>
        /// Ebon Hammer Haft
        ///</summary>
        EbonHammerHaft = 10,
        ///<summary>
        /// Shocking Hammer Haft
        ///</summary>
        ShockingHammerHaft = 11,
        ///<summary>
        /// Fiery Hammer Haft
        ///</summary>
        FieryHammerHaft = 12,
        ///<summary>
        /// Icy Sword Hilt
        ///</summary>
        IcySwordHilt = 13,
        ///<summary>
        /// Ebon Sword Hilt
        ///</summary>
        EbonSwordHilt = 14,
        ///<summary>
        /// Shocking Sword Hilt
        ///</summary>
        ShockingSwordHilt = 15,
        ///<summary>
        /// Fiery Sword Hilt
        ///</summary>
        FierySwordHilt = 16,
        ///<summary>
        /// Furious Axe Haft
        ///</summary>
        FuriousAxeHaft = 17,
        ///<summary>
        /// Furious Hammer Haft
        ///</summary>
        FuriousHammerHaft = 18,
        ///<summary>
        /// Furious Sword Hilt
        ///</summary>
        FuriousSwordHilt = 19,
        ///<summary>
        /// Rune of Minor Fast Casting (Mesmer)
        ///</summary>
        RuneofMinorFastCastingMesmer = 22,
        ///<summary>
        /// Rune of Minor Domination Magic (Mesmer)
        ///</summary>
        RuneofMinorDominationMagicMesmer = 23,
        ///<summary>
        /// Rune of Minor Illusion Magic (Mesmer)
        ///</summary>
        RuneofMinorIllusionMagicMesmer = 24,
        ///<summary>
        /// Rune of Minor Inspiration Magic (Mesmer)
        ///</summary>
        RuneofMinorInspirationMagicMesmer = 25,
        ///<summary>
        /// Rune of Minor Blood Magic (Necromancer)
        ///</summary>
        RuneofMinorBloodMagicNecromancer = 26,
        ///<summary>
        /// Rune of Minor Death Magic (Necromancer)
        ///</summary>
        RuneofMinorDeathMagicNecromancer = 27,
        ///<summary>
        /// Rune of Minor Curses (Necromancer)
        ///</summary>
        RuneofMinorCursesNecromancer = 28,
        ///<summary>
        /// Rune of Minor Soul Reaping (Necromancer)
        ///</summary>
        RuneofMinorSoulReapingNecromancer = 29,
        ///<summary>
        /// Rune of Minor Energy Storage (Elementalist)
        ///</summary>
        RuneofMinorEnergyStorageElementalist = 30,
        ///<summary>
        /// Rune of Minor Fire Magic (Elementalist)
        ///</summary>
        RuneofMinorFireMagicElementalist = 31,
        ///<summary>
        /// Rune of Minor Air Magic (Elementalist)
        ///</summary>
        RuneofMinorAirMagicElementalist = 32,
        ///<summary>
        /// Rune of Minor Earth Magic (Elementalist)
        ///</summary>
        RuneofMinorEarthMagicElementalist = 33,
        ///<summary>
        /// Rune of Minor Water Magic (Elementalist)
        ///</summary>
        RuneofMinorWaterMagicElementalist = 34,
        ///<summary>
        /// Rune of Minor Healing Prayers (Monk)
        ///</summary>
        RuneofMinorHealingPrayersMonk = 35,
        ///<summary>
        /// Rune of Minor Smiting Prayers (Monk)
        ///</summary>
        RuneofMinorSmitingPrayersMonk = 36,
        ///<summary>
        /// Rune of Minor Protection Prayers (Monk)
        ///</summary>
        RuneofMinorProtectionPrayersMonk = 37,
        ///<summary>
        /// Rune of Minor Divine Favor (Monk)
        ///</summary>
        RuneofMinorDivineFavorMonk = 38,
        ///<summary>
        /// Rune of Minor Tactics (Warrior)
        ///</summary>
        RuneofMinorTacticsWarrior = 39,
        ///<summary>
        /// Rune of Minor Strength (Warrior)
        ///</summary>
        RuneofMinorStrengthWarrior = 40,
        ///<summary>
        /// Rune of Minor Axe Mastery (Warrior)
        ///</summary>
        RuneofMinorAxeMasteryWarrior = 41,
        ///<summary>
        /// Rune of Minor Hammer Mastery (Warrior)
        ///</summary>
        RuneofMinorHammerMasteryWarrior = 42,
        ///<summary>
        /// Rune of Minor Swordsmanship (Warrior)
        ///</summary>
        RuneofMinorSwordsmanshipWarrior = 43,
        ///<summary>
        /// Rune of Minor Wilderness Survival (Ranger)
        ///</summary>
        RuneofMinorWildernessSurvivalRanger = 44,
        ///<summary>
        /// Rune of Minor Expertise (Ranger)
        ///</summary>
        RuneofMinorExpertiseRanger = 45,
        ///<summary>
        /// Rune of Minor Beast Mastery (Ranger)
        ///</summary>
        RuneofMinorBeastMasteryRanger = 46,
        ///<summary>
        /// Rune of Minor Marksmanship (Ranger)
        ///</summary>
        RuneofMinorMarksmanshipRanger = 47,
        ///<summary>
        /// Rune of Major Fast Casting (Mesmer)
        ///</summary>
        RuneofMajorFastCastingMesmer = 48,
        ///<summary>
        /// Rune of Major Domination Magic (Mesmer)
        ///</summary>
        RuneofMajorDominationMagicMesmer = 49,
        ///<summary>
        /// Rune of Major Illusion Magic (Mesmer)
        ///</summary>
        RuneofMajorIllusionMagicMesmer = 50,
        ///<summary>
        /// Rune of Major Inspiration Magic (Mesmer)
        ///</summary>
        RuneofMajorInspirationMagicMesmer = 51,
        ///<summary>
        /// Rune of Major Blood Magic (Necromancer)
        ///</summary>
        RuneofMajorBloodMagicNecromancer = 52,
        ///<summary>
        /// Rune of Major Death Magic (Necromancer)
        ///</summary>
        RuneofMajorDeathMagicNecromancer = 53,
        ///<summary>
        /// Rune of Major Curses (Necromancer)
        ///</summary>
        RuneofMajorCursesNecromancer = 54,
        ///<summary>
        /// Rune of Major Soul Reaping (Necromancer)
        ///</summary>
        RuneofMajorSoulReapingNecromancer = 55,
        ///<summary>
        /// Rune of Major Energy Storage (Elementalist)
        ///</summary>
        RuneofMajorEnergyStorageElementalist = 56,
        ///<summary>
        /// Rune of Major Fire Magic (Elementalist)
        ///</summary>
        RuneofMajorFireMagicElementalist = 57,
        ///<summary>
        /// Rune of Major Air Magic (Elementalist)
        ///</summary>
        RuneofMajorAirMagicElementalist = 58,
        ///<summary>
        /// Rune of Major Earth Magic (Elementalist)
        ///</summary>
        RuneofMajorEarthMagicElementalist = 59,
        ///<summary>
        /// Rune of Major Water Magic (Elementalist)
        ///</summary>
        RuneofMajorWaterMagicElementalist = 60,
        ///<summary>
        /// Rune of Major Healing Prayers (Monk)
        ///</summary>
        RuneofMajorHealingPrayersMonk = 61,
        ///<summary>
        /// Rune of Major Smiting Prayers (Monk)
        ///</summary>
        RuneofMajorSmitingPrayersMonk = 62,
        ///<summary>
        /// Rune of Major Protection Prayers (Monk)
        ///</summary>
        RuneofMajorProtectionPrayersMonk = 63,
        ///<summary>
        /// Rune of Major Divine Favor (Monk)
        ///</summary>
        RuneofMajorDivineFavorMonk = 64,
        ///<summary>
        /// Rune of Major Tactics (Warrior)
        ///</summary>
        RuneofMajorTacticsWarrior = 65,
        ///<summary>
        /// Rune of Major Strength (Warrior)
        ///</summary>
        RuneofMajorStrengthWarrior = 66,
        ///<summary>
        /// Rune of Major Axe Mastery (Warrior)
        ///</summary>
        RuneofMajorAxeMasteryWarrior = 67,
        ///<summary>
        /// Rune of Major Hammer Mastery (Warrior)
        ///</summary>
        RuneofMajorHammerMasteryWarrior = 68,
        ///<summary>
        /// Rune of Major Swordsmanship (Warrior)
        ///</summary>
        RuneofMajorSwordsmanshipWarrior = 69,
        ///<summary>
        /// Rune of Major Wilderness Survival (Ranger)
        ///</summary>
        RuneofMajorWildernessSurvivalRanger = 70,
        ///<summary>
        /// Rune of Major Expertise (Ranger)
        ///</summary>
        RuneofMajorExpertiseRanger = 71,
        ///<summary>
        /// Rune of Major Beast Mastery (Ranger)
        ///</summary>
        RuneofMajorBeastMasteryRanger = 72,
        ///<summary>
        /// Rune of Major Marksmanship (Ranger)
        ///</summary>
        RuneofMajorMarksmanshipRanger = 73,
        ///<summary>
        /// Rune of Superior Fast Casting (Mesmer)
        ///</summary>
        RuneofSuperiorFastCastingMesmer = 74,
        ///<summary>
        /// Rune of Superior Domination Magic (Mesmer)
        ///</summary>
        RuneofSuperiorDominationMagicMesmer = 75,
        ///<summary>
        /// Rune of Superior Illusion Magic (Mesmer)
        ///</summary>
        RuneofSuperiorIllusionMagicMesmer = 76,
        ///<summary>
        /// Rune of Superior Inspiration Magic (Mesmer)
        ///</summary>
        RuneofSuperiorInspirationMagicMesmer = 77,
        ///<summary>
        /// Rune of Superior Death Magic (Necromancer)
        ///</summary>
        RuneofSuperiorDeathMagicNecromancer = 78,
        ///<summary>
        /// Rune of Superior Curses (Necromancer)
        ///</summary>
        RuneofSuperiorCursesNecromancer = 80,
        ///<summary>
        /// Rune of Superior Soul Reaping (Necromancer)
        ///</summary>
        RuneofSuperiorSoulReapingNecromancer = 81,
        ///<summary>
        /// Rune of Superior Energy Storage (Elementalist)
        ///</summary>
        RuneofSuperiorEnergyStorageElementalist = 82,
        ///<summary>
        /// Rune of Superior Fire Magic (Elementalist)
        ///</summary>
        RuneofSuperiorFireMagicElementalist = 83,
        ///<summary>
        /// Rune of Superior Air Magic (Elementalist)
        ///</summary>
        RuneofSuperiorAirMagicElementalist = 84,
        ///<summary>
        /// Rune of Superior Earth Magic (Elementalist)
        ///</summary>
        RuneofSuperiorEarthMagicElementalist = 85,
        ///<summary>
        /// Rune of Superior Water Magic (Elementalist)
        ///</summary>
        RuneofSuperiorWaterMagicElementalist = 86,
        ///<summary>
        /// Rune of Superior Healing Prayers (Monk)
        ///</summary>
        RuneofSuperiorHealingPrayersMonk = 87,
        ///<summary>
        /// Rune of Superior Smiting Prayers (Monk)
        ///</summary>
        RuneofSuperiorSmitingPrayersMonk = 88,
        ///<summary>
        /// Rune of Superior Protection Prayers (Monk)
        ///</summary>
        RuneofSuperiorProtectionPrayersMonk = 89,
        ///<summary>
        /// Rune of Superior Divine Favor (Monk)
        ///</summary>
        RuneofSuperiorDivineFavorMonk = 90,
        ///<summary>
        /// Rune of Superior Tactics (Warrior)
        ///</summary>
        RuneofSuperiorTacticsWarrior = 91,
        ///<summary>
        /// Rune of Superior Strength (Warrior)
        ///</summary>
        RuneofSuperiorStrengthWarrior = 92,
        ///<summary>
        /// Rune of Superior Axe Mastery (Warrior)
        ///</summary>
        RuneofSuperiorAxeMasteryWarrior = 93,
        ///<summary>
        /// Rune of Superior Hammer Mastery (Warrior)
        ///</summary>
        RuneofSuperiorHammerMasteryWarrior = 94,
        ///<summary>
        /// Rune of Superior Swordsmanship (Warrior)
        ///</summary>
        RuneofSuperiorSwordsmanshipWarrior = 95,
        ///<summary>
        /// Rune of Superior Wilderness Survival (Ranger)
        ///</summary>
        RuneofSuperiorWildernessSurvivalRanger = 96,
        ///<summary>
        /// Rune of Superior Expertise (Ranger)
        ///</summary>
        RuneofSuperiorExpertiseRanger = 97,
        ///<summary>
        /// Rune of Superior Beast Mastery (Ranger)
        ///</summary>
        RuneofSuperiorBeastMasteryRanger = 98,
        ///<summary>
        /// Rune of Superior Marksmanship (Ranger)
        ///</summary>
        RuneofSuperiorMarksmanshipRanger = 99,
        ///<summary>
        /// Defensive Staff Head
        ///</summary>
        DefensiveStaffHead = 100,
        ///<summary>
        /// Barbed Axe Haft
        ///</summary>
        BarbedAxeHaft = 101,
        ///<summary>
        /// Barbed Sword Hilt
        ///</summary>
        BarbedSwordHilt = 102,
        ///<summary>
        /// Crippling Axe Haft
        ///</summary>
        CripplingAxeHaft = 103,
        ///<summary>
        /// Crippling Sword Hilt
        ///</summary>
        CripplingSwordHilt = 104,
        ///<summary>
        /// Cruel Axe Haft
        ///</summary>
        CruelAxeHaft = 105,
        ///<summary>
        /// Cruel Hammer Haft
        ///</summary>
        CruelHammerHaft = 106,
        ///<summary>
        /// Cruel Sword Hilt
        ///</summary>
        CruelSwordHilt = 107,
        ///<summary>
        /// Insightful Staff Head
        ///</summary>
        InsightfulStaffHead = 108,
        ///<summary>
        /// Hale Staff Head
        ///</summary>
        HaleStaffHead = 109,
        ///<summary>
        /// Poisonous Axe Haft
        ///</summary>
        PoisonousAxeHaft = 110,
        ///<summary>
        /// Poisonous Bow String
        ///</summary>
        PoisonousBowString = 111,
        ///<summary>
        /// Poisonous Sword Hilt
        ///</summary>
        PoisonousSwordHilt = 112,
        ///<summary>
        /// Heavy Axe Haft
        ///</summary>
        HeavyAxeHaft = 113,
        ///<summary>
        /// Heavy Hammer Haft
        ///</summary>
        HeavyHammerHaft = 114,
        ///<summary>
        /// Zealous Axe Haft
        ///</summary>
        ZealousAxeHaft = 115,
        ///<summary>
        /// Zealous Hammer Haft
        ///</summary>
        ZealousHammerHaft = 116,
        ///<summary>
        /// Zealous Bow String
        ///</summary>
        ZealousBowString = 117,
        ///<summary>
        /// Zealous Sword Hilt
        ///</summary>
        ZealousSwordHilt = 118,
        ///<summary>
        /// Vampiric Axe Haft
        ///</summary>
        VampiricAxeHaft = 119,
        ///<summary>
        /// Vampiric Hammer Haft
        ///</summary>
        VampiricHammerHaft = 120,
        ///<summary>
        /// Vampiric Bow String
        ///</summary>
        VampiricBowString = 121,
        ///<summary>
        /// Vampiric Sword Hilt
        ///</summary>
        VampiricSwordHilt = 122,
        ///<summary>
        /// Rune of Minor Mysticism (Dervish)
        ///</summary>
        RuneofMinorMysticismDervish = 123,
        ///<summary>
        /// Rune of Minor Earth Prayers (Dervish)
        ///</summary>
        RuneofMinorEarthPrayersDervish = 124,
        ///<summary>
        /// Rune of Minor Scythe Mastery (Dervish)
        ///</summary>
        RuneofMinorScytheMasteryDervish = 125,
        ///<summary>
        /// Rune of Minor Wind Prayers (Dervish)
        ///</summary>
        RuneofMinorWindPrayersDervish = 126,
        ///<summary>
        /// Axe Grip of Defense
        ///</summary>
        AxeGripofDefense = 127,
        ///<summary>
        /// Bow Grip of Defense
        ///</summary>
        BowGripofDefense = 128,
        ///<summary>
        /// Axe Grip of Warding
        ///</summary>
        AxeGripofWarding = 129,
        ///<summary>
        /// Bow Grip of Warding
        ///</summary>
        BowGripofWarding = 130,
        ///<summary>
        /// Hammer Grip of Warding
        ///</summary>
        HammerGripofWarding = 131,
        ///<summary>
        /// Staff Wrapping of Warding
        ///</summary>
        StaffWrappingofWarding = 132,
        ///<summary>
        /// Sword Pommel of Warding
        ///</summary>
        SwordPommelofWarding = 133,
        ///<summary>
        /// Hammer Grip of Defense
        ///</summary>
        HammerGripofDefense = 134,
        ///<summary>
        /// Axe Grip of Shelter
        ///</summary>
        AxeGripofShelter = 135,
        ///<summary>
        /// Bow Grip of Shelter
        ///</summary>
        BowGripofShelter = 136,
        ///<summary>
        /// Hammer Grip of Shelter
        ///</summary>
        HammerGripofShelter = 137,
        ///<summary>
        /// Staff Wrapping of Shelter
        ///</summary>
        StaffWrappingofShelter = 138,
        ///<summary>
        /// Sword Pommel of Shelter
        ///</summary>
        SwordPommelofShelter = 139,
        ///<summary>
        /// Staff Wrapping of Defense
        ///</summary>
        StaffWrappingofDefense = 140,
        ///<summary>
        /// Sword Pommel of Defense
        ///</summary>
        SwordPommelofDefense = 141,
        ///<summary>
        /// Axe Grip of Fortitude
        ///</summary>
        AxeGripofFortitude = 142,
        ///<summary>
        /// Bow Grip of Fortitude
        ///</summary>
        BowGripofFortitude = 143,
        ///<summary>
        /// Hammer Grip of Fortitude
        ///</summary>
        HammerGripofFortitude = 144,
        ///<summary>
        /// Staff Wrapping of Fortitude
        ///</summary>
        StaffWrappingofFortitude = 145,
        ///<summary>
        /// Sword Pommel of Fortitude
        ///</summary>
        SwordPommelofFortitude = 146,
        ///<summary>
        /// Axe Grip of Enchanting
        ///</summary>
        AxeGripofEnchanting = 147,
        ///<summary>
        /// Bow Grip of Enchanting
        ///</summary>
        BowGripofEnchanting = 148,
        ///<summary>
        /// Hammer Grip of Enchanting
        ///</summary>
        HammerGripofEnchanting = 149,
        ///<summary>
        /// Staff Wrapping of Enchanting
        ///</summary>
        StaffWrappingofEnchanting = 150,
        ///<summary>
        /// Sword Pommel of Enchanting
        ///</summary>
        SwordPommelofEnchanting = 151,
        ///<summary>
        /// Axe Grip of Mastery
        ///</summary>
        AxeGripofMastery = 152,
        ///<summary>
        /// Bow Grip of Mastery
        ///</summary>
        BowGripofMastery = 153,
        ///<summary>
        /// Hammer Grip of Mastery
        ///</summary>
        HammerGripofMastery = 154,
        ///<summary>
        /// Sword Pommel of Mastery
        ///</summary>
        SwordPommelofMastery = 155,
        ///<summary>
        /// Rune of Minor Vigor
        ///</summary>
        RuneofMinorVigor = 156,
        ///<summary>
        /// Rune of Major Vigor
        ///</summary>
        RuneofMajorVigor = 157,
        ///<summary>
        /// Rune of Superior Vigor
        ///</summary>
        RuneofSuperiorVigor = 158,
        ///<summary>
        /// Rune of Minor Absorption (Warrior)
        ///</summary>
        RuneofMinorAbsorptionWarrior = 159,
        ///<summary>
        /// Rune of Major Absorption (Warrior)
        ///</summary>
        RuneofMajorAbsorptionWarrior = 160,
        ///<summary>
        /// Rune of Superior Absorption (Warrior)
        ///</summary>
        RuneofSuperiorAbsorptionWarrior = 161,
        ///<summary>
        /// Rune of Minor Critical Strikes (Assassin)
        ///</summary>
        RuneofMinorCriticalStrikesAssassin = 162,
        ///<summary>
        /// Rune of Minor Dagger Mastery (Assassin)
        ///</summary>
        RuneofMinorDaggerMasteryAssassin = 163,
        ///<summary>
        /// Rune of Minor Deadly Arts (Assassin)
        ///</summary>
        RuneofMinorDeadlyArtsAssassin = 164,
        ///<summary>
        /// Rune of Minor Shadow Arts (Assassin)
        ///</summary>
        RuneofMinorShadowArtsAssassin = 165,
        ///<summary>
        /// Rune of Minor Channeling Magic (Ritualist)
        ///</summary>
        RuneofMinorChannelingMagicRitualist = 166,
        ///<summary>
        /// Rune of Minor Restoration Magic (Ritualist)
        ///</summary>
        RuneofMinorRestorationMagicRitualist = 167,
        ///<summary>
        /// Rune of Minor Communing (Ritualist)
        ///</summary>
        RuneofMinorCommuningRitualist = 168,
        ///<summary>
        /// Rune of Minor Spawning Power (Ritualist)
        ///</summary>
        RuneofMinorSpawningPowerRitualist = 169,
        ///<summary>
        /// Rune of Major Critical Strikes (Assassin)
        ///</summary>
        RuneofMajorCriticalStrikesAssassin = 170,
        ///<summary>
        /// Rune of Major Dagger Mastery (Assassin)
        ///</summary>
        RuneofMajorDaggerMasteryAssassin = 171,
        ///<summary>
        /// Rune of Major Deadly Arts (Assassin)
        ///</summary>
        RuneofMajorDeadlyArtsAssassin = 172,
        ///<summary>
        /// Rune of Major Shadow Arts (Assassin)
        ///</summary>
        RuneofMajorShadowArtsAssassin = 173,
        ///<summary>
        /// Rune of Major Channeling Magic (Ritualist)
        ///</summary>
        RuneofMajorChannelingMagicRitualist = 174,
        ///<summary>
        /// Rune of Major Restoration Magic (Ritualist)
        ///</summary>
        RuneofMajorRestorationMagicRitualist = 175,
        ///<summary>
        /// Rune of Major Communing (Ritualist)
        ///</summary>
        RuneofMajorCommuningRitualist = 176,
        ///<summary>
        /// Rune of Major Spawning Power (Ritualist)
        ///</summary>
        RuneofMajorSpawningPowerRitualist = 177,
        ///<summary>
        /// Rune of Superior Critical Strikes (Assassin)
        ///</summary>
        RuneofSuperiorCriticalStrikesAssassin = 178,
        ///<summary>
        /// Rune of Superior Dagger Mastery (Assassin)
        ///</summary>
        RuneofSuperiorDaggerMasteryAssassin = 179,
        ///<summary>
        /// Rune of Superior Deadly Arts (Assassin)
        ///</summary>
        RuneofSuperiorDeadlyArtsAssassin = 180,
        ///<summary>
        /// Rune of Superior Shadow Arts (Assassin)
        ///</summary>
        RuneofSuperiorShadowArtsAssassin = 181,
        ///<summary>
        /// Rune of Superior Channeling Magic (Ritualist)
        ///</summary>
        RuneofSuperiorChannelingMagicRitualist = 182,
        ///<summary>
        /// Rune of Superior Restoration Magic (Ritualist)
        ///</summary>
        RuneofSuperiorRestorationMagicRitualist = 183,
        ///<summary>
        /// Rune of Superior Communing (Ritualist)
        ///</summary>
        RuneofSuperiorCommuningRitualist = 184,
        ///<summary>
        /// Rune of Superior Spawning Power (Ritualist)
        ///</summary>
        RuneofSuperiorSpawningPowerRitualist = 185,
        ///<summary>
        /// Icy Dagger Tang
        ///</summary>
        IcyDaggerTang = 186,
        ///<summary>
        /// Ebon Dagger Tang
        ///</summary>
        EbonDaggerTang = 187,
        ///<summary>
        /// Fiery Dagger Tang
        ///</summary>
        FieryDaggerTang = 188,
        ///<summary>
        /// Shocking Dagger Tang
        ///</summary>
        ShockingDaggerTang = 189,
        ///<summary>
        /// Zealous Dagger Tang
        ///</summary>
        ZealousDaggerTang = 190,
        ///<summary>
        /// Vampiric Dagger Tang
        ///</summary>
        VampiricDaggerTang = 191,
        ///<summary>
        /// Barbed Dagger Tang
        ///</summary>
        BarbedDaggerTang = 192,
        ///<summary>
        /// Crippling Dagger Tang
        ///</summary>
        CripplingDaggerTang = 193,
        ///<summary>
        /// Cruel Dagger Tang
        ///</summary>
        CruelDaggerTang = 194,
        ///<summary>
        /// Poisonous Dagger Tang
        ///</summary>
        PoisonousDaggerTang = 195,
        ///<summary>
        /// Silencing Dagger Tang
        ///</summary>
        SilencingDaggerTang = 196,
        ///<summary>
        /// Furious Dagger Tang
        ///</summary>
        FuriousDaggerTang = 197,
        ///<summary>
        /// Rune of Minor Leadership (Paragon)
        ///</summary>
        RuneofMinorLeadershipParagon = 198,
        ///<summary>
        /// Dagger Handle of Mastery
        ///</summary>
        DaggerHandleofMastery = 199,
        ///<summary>
        /// Dagger Handle of Defense
        ///</summary>
        DaggerHandleofDefense = 200,
        ///<summary>
        /// Dagger Handle of Shelter
        ///</summary>
        DaggerHandleofShelter = 201,
        ///<summary>
        /// Dagger Handle of Warding
        ///</summary>
        DaggerHandleofWarding = 202,
        ///<summary>
        /// Dagger Handle of Enchanting
        ///</summary>
        DaggerHandleofEnchanting = 203,
        ///<summary>
        /// Dagger Handle of Fortitude
        ///</summary>
        DaggerHandleofFortitude = 204,
        ///<summary>
        /// Barbed Bow String
        ///</summary>
        BarbedBowString = 205,
        ///<summary>
        /// Crippling Bow String
        ///</summary>
        CripplingBowString = 206,
        ///<summary>
        /// Silencing Bow String
        ///</summary>
        SilencingBowString = 207,
        ///<summary>
        /// Sundering Axe Haft
        ///</summary>
        SunderingAxeHaft = 208,
        ///<summary>
        /// Sundering Bow String
        ///</summary>
        SunderingBowString = 209,
        ///<summary>
        /// Sundering Hammer Haft
        ///</summary>
        SunderingHammerHaft = 210,
        ///<summary>
        /// Sundering Sword Hilt
        ///</summary>
        SunderingSwordHilt = 211,
        ///<summary>
        /// Sundering Dagger Tang
        ///</summary>
        SunderingDaggerTang = 212,
        ///<summary>
        /// Rune of Minor Motivation (Paragon)
        ///</summary>
        RuneofMinorMotivationParagon = 213,
        ///<summary>
        /// Rune of Minor Command (Paragon)
        ///</summary>
        RuneofMinorCommandParagon = 214,
        ///<summary>
        /// Rune of Minor Spear Mastery (Paragon)
        ///</summary>
        RuneofMinorSpearMasteryParagon = 215,
        ///<summary>
        /// Rune of Major Mysticism (Dervish)
        ///</summary>
        RuneofMajorMysticismDervish = 216,
        ///<summary>
        /// Rune of Major Earth Prayers (Dervish)
        ///</summary>
        RuneofMajorEarthPrayersDervish = 217,
        ///<summary>
        /// Rune of Major Scythe Mastery (Dervish)
        ///</summary>
        RuneofMajorScytheMasteryDervish = 218,
        ///<summary>
        /// Rune of Major Wind Prayers (Dervish)
        ///</summary>
        RuneofMajorWindPrayersDervish = 219,
        ///<summary>
        /// Rune of Major Leadership (Paragon)
        ///</summary>
        RuneofMajorLeadershipParagon = 220,
        ///<summary>
        /// Rune of Major Motivation (Paragon)
        ///</summary>
        RuneofMajorMotivationParagon = 221,
        ///<summary>
        /// Rune of Major Command (Paragon)
        ///</summary>
        RuneofMajorCommandParagon = 222,
        ///<summary>
        /// Rune of Major Spear Mastery (Paragon)
        ///</summary>
        RuneofMajorSpearMasteryParagon = 223,
        ///<summary>
        /// Rune of Superior Mysticism (Dervish)
        ///</summary>
        RuneofSuperiorMysticismDervish = 224,
        ///<summary>
        /// Rune of Superior Earth Prayers (Dervish)
        ///</summary>
        RuneofSuperiorEarthPrayersDervish = 225,
        ///<summary>
        /// Rune of Superior Scythe Mastery (Dervish)
        ///</summary>
        RuneofSuperiorScytheMasteryDervish = 226,
        ///<summary>
        /// Rune of Superior Wind Prayers (Dervish)
        ///</summary>
        RuneofSuperiorWindPrayersDervish = 227,
        ///<summary>
        /// Rune of Superior Leadership (Paragon)
        ///</summary>
        RuneofSuperiorLeadershipParagon = 228,
        ///<summary>
        /// Rune of Superior Motivation (Paragon)
        ///</summary>
        RuneofSuperiorMotivationParagon = 229,
        ///<summary>
        /// Rune of Superior Command (Paragon)
        ///</summary>
        RuneofSuperiorCommandParagon = 230,
        ///<summary>
        /// Rune of Superior Spear Mastery (Paragon)
        ///</summary>
        RuneofSuperiorSpearMasteryParagon = 231,
        ///<summary>
        /// Icy Scythe Snathe
        ///</summary>
        IcyScytheSnathe = 232,
        ///<summary>
        /// Ebon Scythe Snathe
        ///</summary>
        EbonScytheSnathe = 233,
        ///<summary>
        /// Zealous Scythe Snathe
        ///</summary>
        ZealousScytheSnathe = 234,
        ///<summary>
        /// Vampiric Scythe Snathe
        ///</summary>
        VampiricScytheSnathe = 235,
        ///<summary>
        /// Sundering Scythe Snathe
        ///</summary>
        SunderingScytheSnathe = 236,
        ///<summary>
        /// Barbed Scythe Snathe
        ///</summary>
        BarbedScytheSnathe = 237,
        ///<summary>
        /// Crippling Scythe Snathe
        ///</summary>
        CripplingScytheSnathe = 238,
        ///<summary>
        /// Cruel Scythe Snathe
        ///</summary>
        CruelScytheSnathe = 239,
        ///<summary>
        /// Furious Scythe Snathe
        ///</summary>
        FuriousScytheSnathe = 240,
        ///<summary>
        /// Poisonous Scythe Snathe
        ///</summary>
        PoisonousScytheSnathe = 241,
        ///<summary>
        /// Heavy Scythe Snathe
        ///</summary>
        HeavyScytheSnathe = 242,
        ///<summary>
        /// Scythe Grip of Mastery
        ///</summary>
        ScytheGripofMastery = 243,
        ///<summary>
        /// Scythe Grip of Defense
        ///</summary>
        ScytheGripofDefense = 244,
        ///<summary>
        /// Scythe Grip of Shelter
        ///</summary>
        ScytheGripofShelter = 245,
        ///<summary>
        /// Scythe Grip of Warding
        ///</summary>
        ScytheGripofWarding = 246,
        ///<summary>
        /// Scythe Grip of Enchanting
        ///</summary>
        ScytheGripofEnchanting = 247,
        ///<summary>
        /// Scythe Grip of Fortitude
        ///</summary>
        ScytheGripofFortitude = 248,
        ///<summary>
        /// Fiery Spearhead
        ///</summary>
        FierySpearhead = 249,
        ///<summary>
        /// Shocking Spearhead
        ///</summary>
        ShockingSpearhead = 250,
        ///<summary>
        /// Zealous Spearhead
        ///</summary>
        ZealousSpearhead = 251,
        ///<summary>
        /// Vampiric Spearhead
        ///</summary>
        VampiricSpearhead = 252,
        ///<summary>
        /// Sundering Spearhead
        ///</summary>
        SunderingSpearhead = 253,
        ///<summary>
        /// Barbed Spearhead
        ///</summary>
        BarbedSpearhead = 254,
        ///<summary>
        /// Crippling Spearhead
        ///</summary>
        CripplingSpearhead = 255,
        ///<summary>
        /// Cruel Spearhead
        ///</summary>
        CruelSpearhead = 256,
        ///<summary>
        /// Furious Spearhead
        ///</summary>
        FuriousSpearhead = 257,
        ///<summary>
        /// Poisonous Spearhead
        ///</summary>
        PoisonousSpearhead = 258,
        ///<summary>
        /// Silencing Spearhead
        ///</summary>
        SilencingSpearhead = 259,
        ///<summary>
        /// Heavy Spearhead
        ///</summary>
        HeavySpearhead = 260,
        ///<summary>
        /// Spear Grip of Mastery
        ///</summary>
        SpearGripofMastery = 261,
        ///<summary>
        /// Spear Grip of Defense
        ///</summary>
        SpearGripofDefense = 262,
        ///<summary>
        /// Spear Grip of Shelter
        ///</summary>
        SpearGripofShelter = 263,
        ///<summary>
        /// Spear Grip of Warding
        ///</summary>
        SpearGripofWarding = 264,
        ///<summary>
        /// Spear Grip of Enchanting
        ///</summary>
        SpearGripofEnchanting = 265,
        ///<summary>
        /// Spear Grip of Fortitude
        ///</summary>
        SpearGripofFortitude = 266,
        ///<summary>
        /// Focus Core of Endurance
        ///</summary>
        FocusCoreofEndurance = 267,
        ///<summary>
        /// Focus Core of Valor
        ///</summary>
        FocusCoreofValor = 268,
        ///<summary>
        /// Fiery Scythe Snathe
        ///</summary>
        FieryScytheSnathe = 269,
        ///<summary>
        /// Shocking Scythe Snathe
        ///</summary>
        ShockingScytheSnathe = 270,
        ///<summary>
        /// Icy Spearhead
        ///</summary>
        IcySpearhead = 271,
        ///<summary>
        /// Ebon Spearhead
        ///</summary>
        EbonSpearhead = 272,
        ///<summary>
        /// Swift Staff Head
        ///</summary>
        SwiftStaffHead = 273,
        ///<summary>
        /// Staff Wrapping of Devotion
        ///</summary>
        StaffWrappingofDevotion = 274,
        ///<summary>
        /// Staff Wrapping of Endurance
        ///</summary>
        StaffWrappingofEndurance = 275,
        ///<summary>
        /// Staff Wrapping of Valor
        ///</summary>
        StaffWrappingofValor = 276,
        ///<summary>
        /// "Let the Memory Live Again"
        ///</summary>
        LettheMemoryLiveAgain = 277,
        ///<summary>
        /// "Have Faith"
        ///</summary>
        HaveFaith = 278,
        ///<summary>
        /// "Don't call it a comeback!"
        ///</summary>
        Dontcallitacomeback = 279,
        ///<summary>
        /// "I am Sorrow"
        ///</summary>
        IamSorrow = 280,
        ///<summary>
        /// "Don't Think Twice"
        ///</summary>
        DontThinkTwice = 281,
        ///<summary>
        /// "Too Much Information"
        ///</summary>
        TooMuchInformation = 282,
        ///<summary>
        /// "Guided by Fate"
        ///</summary>
        GuidedbyFate = 283,
        ///<summary>
        /// "Soundness of Mind"
        ///</summary>
        SoundnessofMind = 284,
        ///<summary>
        /// "Only the Strong Survive"
        ///</summary>
        OnlytheStrongSurvive = 285,
        ///<summary>
        /// "Don't Fear the Reaper"
        ///</summary>
        DontFeartheReaper = 286,
        ///<summary>
        /// "Dance with Death"
        ///</summary>
        DancewithDeath = 287,
        ///<summary>
        /// "Brawn over Brains"
        ///</summary>
        BrawnoverBrains = 288,
        ///<summary>
        /// "To the Pain!"
        ///</summary>
        TothePain = 289,
        ///<summary>
        /// Survivor Insignia
        ///</summary>
        SurvivorInsignia = 290,
        ///<summary>
        /// Radiant Insignia
        ///</summary>
        RadiantInsignia = 291,
        ///<summary>
        /// Stalwart Insignia
        ///</summary>
        StalwartInsignia = 292,
        ///<summary>
        /// Brawler's Insignia
        ///</summary>
        BrawlersInsignia = 293,
        ///<summary>
        /// Blessed Insignia
        ///</summary>
        BlessedInsignia = 294,
        ///<summary>
        /// Herald's Insignia
        ///</summary>
        HeraldsInsignia = 295,
        ///<summary>
        /// Sentry's Insignia
        ///</summary>
        SentrysInsignia = 296,
        ///<summary>
        /// Vanguard's Insignia (Assassin)
        ///</summary>
        VanguardsInsigniaAssassin = 297,
        ///<summary>
        /// Infiltrator's Insignia (Assassin)
        ///</summary>
        InfiltratorsInsigniaAssassin = 298,
        ///<summary>
        /// Saboteur's Insignia (Assassin)
        ///</summary>
        SaboteursInsigniaAssassin = 299,
        ///<summary>
        /// Nightstalker's Insignia (Assassin)
        ///</summary>
        NightstalkersInsigniaAssassin = 300,
        ///<summary>
        /// Virtuoso's Insignia (Mesmer)
        ///</summary>
        VirtuososInsigniaMesmer = 301,
        ///<summary>
        /// Bloodstained Insignia (Necromancer)
        ///</summary>
        BloodstainedInsigniaNecromancer = 302,
        ///<summary>
        /// Tormentor's Insignia (Necromancer)
        ///</summary>
        TormentorsInsigniaNecromancer = 303,
        ///<summary>
        /// Bonelace Insignia (Necromancer)
        ///</summary>
        BonelaceInsigniaNecromancer = 304,
        ///<summary>
        /// Minion Master's Insignia (Necromancer)
        ///</summary>
        MinionMastersInsigniaNecromancer = 305,
        ///<summary>
        /// Blighter's Insignia (Necromancer)
        ///</summary>
        BlightersInsigniaNecromancer = 306,
        ///<summary>
        /// Hydromancer's Insignia (Elementalist)
        ///</summary>
        HydromancersInsigniaElementalist = 307,
        ///<summary>
        /// Geomancer's Insignia (Elementalist)
        ///</summary>
        GeomancersInsigniaElementalist = 308,
        ///<summary>
        /// Pyromancer's Insignia (Elementalist)
        ///</summary>
        PyromancersInsigniaElementalist = 309,
        ///<summary>
        /// Aeromancer's Insignia (Elementalist)
        ///</summary>
        AeromancersInsigniaElementalist = 310,
        ///<summary>
        /// Wanderer's Insignia (Monk)
        ///</summary>
        WanderersInsigniaMonk = 311,
        ///<summary>
        /// Disciple's Insignia (Monk)
        ///</summary>
        DisciplesInsigniaMonk = 312,
        ///<summary>
        /// Knight's Insignia (Warrior)
        ///</summary>
        KnightsInsigniaWarrior = 313,
        ///<summary>
        /// Lieutenant's Insignia (Warrior)
        ///</summary>
        LieutenantsInsigniaWarrior = 314,
        ///<summary>
        /// Stonefist Insignia (Warrior)
        ///</summary>
        StonefistInsigniaWarrior = 315,
        ///<summary>
        /// Dreadnought Insignia (Warrior)
        ///</summary>
        DreadnoughtInsigniaWarrior = 316,
        ///<summary>
        /// Sentinel's Insignia (Warrior)
        ///</summary>
        SentinelsInsigniaWarrior = 317,
        ///<summary>
        /// Frostbound Insignia (Ranger)
        ///</summary>
        FrostboundInsigniaRanger = 318,
        ///<summary>
        /// Pyrebound Insignia (Ranger)
        ///</summary>
        PyreboundInsigniaRanger = 319,
        ///<summary>
        /// Stormbound Insignia (Ranger)
        ///</summary>
        StormboundInsigniaRanger = 320,
        ///<summary>
        /// Scout's Insignia (Ranger)
        ///</summary>
        ScoutsInsigniaRanger = 321,
        ///<summary>
        /// Shaman's Insignia (Ritualist)
        ///</summary>
        ShamansInsigniaRitualist = 322,
        ///<summary>
        /// Ghost Forge Insignia (Ritualist)
        ///</summary>
        GhostForgeInsigniaRitualist = 323,
        ///<summary>
        /// Mystic's Insignia (Ritualist)
        ///</summary>
        MysticsInsigniaRitualist = 324,
        ///<summary>
        /// "Faith is My Shield"
        ///</summary>
        FaithisMyShield = 325,
        ///<summary>
        /// "Live for Today"
        ///</summary>
        LiveforToday = 326,
        ///<summary>
        /// "Serenity Now"
        ///</summary>
        SerenityNow = 327,
        ///<summary>
        /// "Forget Me Not"
        ///</summary>
        ForgetMeNot = 328,
        ///<summary>
        /// "I have the power!"
        ///</summary>
        Ihavethepower = 329,
        ///<summary>
        /// "Luck of the Draw"
        ///</summary>
        LuckoftheDraw = 330,
        ///<summary>
        /// "Sheltered by Faith"
        ///</summary>
        ShelteredbyFaith = 331,
        ///<summary>
        /// "Nothing to Fear"
        ///</summary>
        NothingtoFear = 332,
        ///<summary>
        /// "Run For Your Life!"
        ///</summary>
        RunForYourLife = 333,
        ///<summary>
        /// "Master of My Domain"
        ///</summary>
        MasterofMyDomain = 334,
        ///<summary>
        /// "Aptitude not Attitude"
        ///</summary>
        AptitudenotAttitude = 335,
        ///<summary>
        /// "Seize the Day"
        ///</summary>
        SeizetheDay = 336,
        ///<summary>
        /// "Hale and Hearty"
        ///</summary>
        HaleandHearty = 337,
        ///<summary>
        /// "Strength and Honor"
        ///</summary>
        StrengthandHonor = 338,
        ///<summary>
        /// "Vengeance is Mine"
        ///</summary>
        VengeanceisMine = 339,
        ///<summary>
        /// Focus Core of Fortitude
        ///</summary>
        FocusCoreofFortitude = 340,
        ///<summary>
        /// Focus Core of Devotion
        ///</summary>
        FocusCoreofDevotion = 341,
        ///<summary>
        /// Focus Core of Swiftness
        ///</summary>
        FocusCoreofSwiftness = 342,
        ///<summary>
        /// Focus Core of Aptitude
        ///</summary>
        FocusCoreofAptitude = 343,
        ///<summary>
        /// Wand Wrapping of Quickening
        ///</summary>
        WandWrappingofQuickening = 344,
        ///<summary>
        /// Wand Wrapping of Memory
        ///</summary>
        WandWrappingofMemory = 345,
        ///<summary>
        /// Shield Handle of Fortitude
        ///</summary>
        ShieldHandleofFortitude = 346,
        ///<summary>
        /// Shield Handle of Devotion
        ///</summary>
        ShieldHandleofDevotion = 347,
        ///<summary>
        /// Shield Handle of Endurance
        ///</summary>
        ShieldHandleofEndurance = 348,
        ///<summary>
        /// Shield Handle of Valor
        ///</summary>
        ShieldHandleofValor = 349,
        ///<summary>
        /// Adept Staff Head
        ///</summary>
        AdeptStaffHead = 350,
        ///<summary>
        /// Staff Wrapping of Mastery
        ///</summary>
        StaffWrappingofMastery = 351,
        ///<summary>
        /// Rune of Attunement
        ///</summary>
        RuneofAttunement = 352,
        ///<summary>
        /// Rune of Vitae
        ///</summary>
        RuneofVitae = 353,
        ///<summary>
        /// Rune of Recovery
        ///</summary>
        RuneofRecovery = 354,
        ///<summary>
        /// Rune of Restoration
        ///</summary>
        RuneofRestoration = 355,
        ///<summary>
        /// Rune of Clarity
        ///</summary>
        RuneofClarity = 356,
        ///<summary>
        /// Rune of Purity
        ///</summary>
        RuneofPurity = 357,
        ///<summary>
        /// Artificer's Insignia (Mesmer)
        ///</summary>
        ArtificersInsigniaMesmer = 358,
        ///<summary>
        /// Prodigy's Insignia (Mesmer)
        ///</summary>
        ProdigysInsigniaMesmer = 359,
        ///<summary>
        /// Undertaker's Insignia (Necromancer)
        ///</summary>
        UndertakersInsigniaNecromancer = 360,
        ///<summary>
        /// Prismatic Insignia (Elementalist)
        ///</summary>
        PrismaticInsigniaElementalist = 361,
        ///<summary>
        /// Anchorite's Insignia (Monk)
        ///</summary>
        AnchoritesInsigniaMonk = 362,
        ///<summary>
        /// Earthbound Insignia (Ranger)
        ///</summary>
        EarthboundInsigniaRanger = 363,
        ///<summary>
        /// Beastmaster's Insignia (Ranger)
        ///</summary>
        BeastmastersInsigniaRanger = 364,
        ///<summary>
        /// Windwalker Insignia (Dervish)
        ///</summary>
        WindwalkerInsigniaDervish = 365,
        ///<summary>
        /// Forsaken Insignia (Dervish)
        ///</summary>
        ForsakenInsigniaDervish = 366,
        ///<summary>
        /// Centurion's Insignia (Paragon)
        ///</summary>
        CenturionsInsigniaParagon = 367,
        ///<summary>
        /// "Ignorance is Bliss"
        ///</summary>
        IgnoranceisBliss = 368,
        ///<summary>
        /// "Life is Pain"
        ///</summary>
        LifeisPain = 369,
        ///<summary>
        /// "Man for All Seasons"
        ///</summary>
        ManforAllSeasons = 370,
        ///<summary>
        /// "Survival of the Fittest"
        ///</summary>
        SurvivaloftheFittest = 371,
        ///<summary>
        /// "Might makes Right"
        ///</summary>
        MightmakesRight = 372,
        ///<summary>
        /// "Knowing is Half the Battle"
        ///</summary>
        KnowingisHalftheBattle = 373,
        ///<summary>
        /// "Down But Not Out"
        ///</summary>
        DownButNotOut = 374,
        ///<summary>
        /// "Hail to the King"
        ///</summary>
        HailtotheKing = 375,
        ///<summary>
        /// "Be Just and Fear Not"
        ///</summary>
        BeJustandFearNot = 376,
        ///<summary>
        /// "Not the face!"
        ///</summary>
        Nottheface = 377,
        ///<summary>
        /// "Leaf on the Wind"
        ///</summary>
        LeafontheWind = 378,
        ///<summary>
        /// "Like a Rolling Stone"
        ///</summary>
        LikeaRollingStone = 379,
        ///<summary>
        /// "Riders on the Storm"
        ///</summary>
        RidersontheStorm = 380,
        ///<summary>
        /// "Sleep Now in the Fire"
        ///</summary>
        SleepNowintheFire = 381,
        ///<summary>
        /// "Through Thick and Thin"
        ///</summary>
        ThroughThickandThin = 382,
        ///<summary>
        /// "The Riddle of Steel"
        ///</summary>
        TheRiddleofSteel = 383,
        ///<summary>
        /// "Fear Cuts Deeper"
        ///</summary>
        FearCutsDeeper = 384,
        ///<summary>
        /// "I Can See Clearly Now"
        ///</summary>
        ICanSeeClearlyNow = 385,
        ///<summary>
        /// "Swift as the Wind"
        ///</summary>
        SwiftastheWind = 386,
        ///<summary>
        /// "Strength of Body"
        ///</summary>
        StrengthofBody = 387,
        ///<summary>
        /// "Cast Out the Unclean"
        ///</summary>
        CastOuttheUnclean = 388,
        ///<summary>
        /// "Pure of Heart"
        ///</summary>
        PureofHeart = 389,
    }
}