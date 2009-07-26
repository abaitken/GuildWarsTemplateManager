using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateParser.Equipment;

namespace TemplateParser.Tests
{
    [TestClass]
    public class EquipmentParserTests
    {
        private readonly NativeEquipmentItem blueAxe = new NativeEquipmentItem
                                                           {
                                                               ColorId = (int) ItemColor.Blue,
                                                               ItemId = (int) ItemIndex.PvPAxe,
                                                               SlotId = (int) EquipmentSlot.Weapon,
                                                               ModifierIds = new[]
                                                                                 {
                                                                                     (int)
                                                                                     ModifierIndex.SunderingAxeHaft,
                                                                                     (int)
                                                                                     ModifierIndex.
                                                                                         AxeGripofFortitude,
                                                                                     (int)
                                                                                     ModifierIndex.StrengthandHonor
                                                                                 }
                                                           };

        private readonly NativeEquipmentItem greyBoots = new NativeEquipmentItem
                                                             {
                                                                 ColorId = (int) ItemColor.Grey,
                                                                 ItemId = (int) ItemIndex.GladiatorBootsWarrior,
                                                                 SlotId = (int) EquipmentSlot.Feet,
                                                                 ModifierIds = new[]
                                                                                   {
                                                                                       (int)
                                                                                       ModifierIndex.SurvivorInsignia,
                                                                                       (int)
                                                                                       ModifierIndex.
                                                                                           RuneofMinorTacticsWarrior
                                                                                   }
                                                             };

        private readonly NativeEquipmentItem greyCuirass = new NativeEquipmentItem
                                                               {
                                                                   ColorId = (int) ItemColor.Grey,
                                                                   ItemId = (int) ItemIndex.GladiatorCuirassWarrior,
                                                                   SlotId = (int) EquipmentSlot.Chest,
                                                                   ModifierIds = new[]
                                                                                     {
                                                                                         (int)
                                                                                         ModifierIndex.RadiantInsignia,
                                                                                         (int)
                                                                                         ModifierIndex.
                                                                                             RuneofSuperiorAbsorptionWarrior
                                                                                     }
                                                               };

        private readonly NativeEquipmentItem greyGauntlets = new NativeEquipmentItem
                                                                 {
                                                                     ColorId = (int) ItemColor.Grey,
                                                                     ItemId = (int) ItemIndex.PlatedGauntletsWarrior,
                                                                     SlotId = (int) EquipmentSlot.Hands,
                                                                     ModifierIds = new[]
                                                                                       {
                                                                                           (int)
                                                                                           ModifierIndex.
                                                                                               StonefistInsigniaWarrior,
                                                                                           (int)
                                                                                           ModifierIndex.
                                                                                               RuneofSuperiorVigor
                                                                                       }
                                                                 };

        private readonly NativeEquipmentItem greyHelm = new NativeEquipmentItem
                                                            {
                                                                ColorId = (int) ItemColor.Grey,
                                                                ItemId = (int) ItemIndex.ExecutionerHelmWarrior,
                                                                SlotId = (int) EquipmentSlot.Head,
                                                                ModifierIds = new[]
                                                                                  {
                                                                                      (int)
                                                                                      ModifierIndex.SurvivorInsignia,
                                                                                      (int)
                                                                                      ModifierIndex.
                                                                                          RuneofMinorAxeMasteryWarrior
                                                                                  }
                                                            };

        private readonly NativeEquipmentItem greyLeggings = new NativeEquipmentItem
                                                                {
                                                                    ColorId = (int) ItemColor.Grey,
                                                                    ItemId = (int) ItemIndex.GladiatorLeggingsWarrior,
                                                                    SlotId = (int) EquipmentSlot.Legs,
                                                                    ModifierIds = new[]
                                                                                      {
                                                                                          (int)
                                                                                          ModifierIndex.RadiantInsignia,
                                                                                          (int)
                                                                                          ModifierIndex.
                                                                                              RuneofMinorStrengthWarrior
                                                                                      }
                                                                };

        private readonly NativeEquipmentItem yellowShield = new NativeEquipmentItem
                                                                {
                                                                    ColorId = (int) ItemColor.Yellow,
                                                                    ItemId = (int) ItemIndex.PvPStrengthShield,
                                                                    SlotId = (int) EquipmentSlot.OffHand,
                                                                    ModifierIds = new[]
                                                                                      {
                                                                                          (int)
                                                                                          ModifierIndex.
                                                                                              ShieldHandleofFortitude,
                                                                                          (int)
                                                                                          ModifierIndex.LuckoftheDraw
                                                                                      }
                                                                };

        [TestMethod]
        public void CanCreateTemplateCode()
        {
            const string expected = "Pk5hbWgmjkaikmWVqIh5ID1Qj5IRBhipITRhgpIPhzh5O9E";

            var parser = new EquipmentParser();
            var source = new NativeEquipmentBuild
                             {
                                 blueAxe,
                                 yellowShield,
                                 greyCuirass,
                                 greyLeggings,
                                 greyHelm,
                                 greyBoots,
                                 greyGauntlets
                             };

            var result = parser.CreateTemplateCode(source);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CanParseNewStyleEquipmentTemplateCode()
        {
            var parser = new EquipmentParser();
            var result = parser.ParseTemplateCode("Pk5hbWgmjkaikmWVqIh5ID1Qj5IRBhipITRhgpIPhzh5O9E");

            Assert.AreEqual(7, result.Count());

            ValidateItem(result.First(),
                         blueAxe);

            ValidateItem(result.Skip(1).First(),
                         yellowShield);

            ValidateItem(result.Skip(2).First(),
                         greyCuirass);

            ValidateItem(result.Skip(3).First(),
                         greyLeggings);

            ValidateItem(result.Skip(4).First(),
                         greyHelm);

            ValidateItem(result.Skip(5).First(),
                         greyBoots);

            ValidateItem(result.Skip(6).First(),
                         greyGauntlets);
        }

        [TestMethod]
        public void CanParseOldStyleEquipmentTemplateCode()
        {
            var parser = new EquipmentParser();
            var result = parser.ParseTemplateCode("RmVYosJTLlNhCpsJNAlNxqosJA");

            Assert.AreEqual(5, result.Count());

            ValidateItem(result.First(),
                         ItemColor.Grey,
                         ItemIndex.RogueAttireMesmer,
                         EquipmentSlot.Chest,
                         new List<ModifierIndex>
                             {
                                 ModifierIndex.RuneofSuperiorInspirationMagicMesmer
                             });

            ValidateItem(result.Skip(1).First(),
                         ItemColor.Grey,
                         ItemIndex.EnchanterHoseMesmer,
                         EquipmentSlot.Legs,
                         new List<ModifierIndex>
                             {
                                 ModifierIndex.RuneofSuperiorInspirationMagicMesmer
                             });

            ValidateItem(result.Skip(2).First(),
                         ItemColor.Grey,
                         ItemIndex.AnimalMaskMesmer,
                         EquipmentSlot.Head,
                         new List<ModifierIndex>
                             {
                                 ModifierIndex.RuneofSuperiorInspirationMagicMesmer
                             });

            ValidateItem(result.Skip(3).First(),
                         ItemColor.Grey,
                         ItemIndex.EnchanterFootwearMesmer,
                         EquipmentSlot.Feet,
                         new List<ModifierIndex>
                             {
                                 ModifierIndex.RuneofSuperiorInspirationMagicMesmer
                             });

            ValidateItem(result.Skip(4).First(),
                         ItemColor.Grey,
                         ItemIndex.EnchanterGlovesMesmer,
                         EquipmentSlot.Hands,
                         new List<ModifierIndex>
                             {
                                 ModifierIndex.RuneofSuperiorInspirationMagicMesmer
                             });
        }

        [TestMethod]
        public void CannotParseInvalidBase64String()
        {
            var parser = new EquipmentParser();
            var result = parser.ParseTemplateCode("pad!£$%^&*()_+-={}[]:@~;'#<>?,./");

            Assert.IsNull(result);
        }


        private static void ValidateItem(NativeEquipmentItem obj, NativeEquipmentItem other)
        {
            ValidateItem(obj,
                         other.ColorId,
                         other.ItemId,
                         other.SlotId,
                         other.ModifierIds);
        }

        private static void ValidateItem(NativeEquipmentItem equipmentItem,
                                         ItemColor itemColor,
                                         ItemIndex itemIndex,
                                         EquipmentSlot equipmentSlot,
                                         IEnumerable<ModifierIndex> list)
        {
            ValidateItem(equipmentItem, (int) itemColor, (int)itemIndex, (int)equipmentSlot, list.Cast<int>());
        }

        private static void ValidateItem(NativeEquipmentItem equipmentItem,
                                         int itemColor,
                                         int itemIndex,
                                         int equipmentSlot,
                                         IEnumerable<int> list)
        {
            Assert.AreEqual(itemColor, equipmentItem.ColorId);
            Assert.AreEqual(itemIndex, equipmentItem.ItemId);
            Assert.AreEqual(equipmentSlot, equipmentItem.SlotId);

            Assert.AreEqual(list.Count(), equipmentItem.ModifierIds.Count());
            foreach(var modifier in list)
                Assert.IsTrue(equipmentItem.ModifierIds.Contains(modifier));
        }
    }
}