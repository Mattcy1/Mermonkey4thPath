using MelonLoader;
using BTD_Mod_Helper;
using Mermonkey4thPath;
using Il2CppAssets.Scripts.Models.Towers;
using PathsPlusPlus;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using BTD_Mod_Helper.Api.Display;
using Il2CppAssets.Scripts.Unity.Display;
using UnityEngine;
using static Mermonkey4thPath.DoubleTrident.DarkBending.DarkInfusion.DarkGust.EyeOfTheStorm.Effect;
using System;
using System.IO;
using MelonLoader.Utils;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using HarmonyLib;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Powers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Unity.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.ServerEvents;
using static MelonLoader.MelonLogger;
using static Mermonkey4thPath.DoubleTrident.DarkBending.DarkInfusion.DarkGust.EyeOfTheStorm;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;


[assembly: MelonInfo(typeof(Mermonkey4thPath.Mermonkey4thPath), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace Mermonkey4thPath;

public class Mermonkey4thPath : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<Mermonkey4thPath>("Mermonkey4thPath loaded!");
    }
}

public class MerMonkeyPath : PathPlusPlus
{
    public override string Tower => TowerType.Mermonkey;
    public override int UpgradeCount => 5;
}

public class DoubleTrident : UpgradePlusPlus<MerMonkeyPath>
{
    public override int Cost => 750;
    public override int Tier => 1;
    public override string Icon => "I0001";

    public override string Portrait => "P0001";

    public override string Description => "Wielding two tridents makes it a lot more deadly.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        {
            foreach (var weaponModel in towerModel.GetWeapons())
            {
                foreach (var attackModel in towerModel.GetAttackModels())
                {
                    attackModel.AddWeapon(attackModel.weapons[0].Duplicate());
                    attackModel.weapons[1].ejectZ = 20;
                    attackModel.weapons[0].ejectX = 10;
                }
            }
        }
    }

    public class DarkBending : UpgradePlusPlus<MerMonkeyPath>
    {
        public override int Cost => 1400;
        public override int Tier => 2;
        public override string Icon => "I0002";

        public override string Portrait => "P0002";

        public override string Description => "The Mermonkey unlocks a powerful force within him called dark bending.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            {
                foreach (var weaponModel in towerModel.GetWeapons())
                {
                    foreach (var attackModel in towerModel.GetAttackModels())
                    {
                        attackModel.AddWeapon(Game.instance.model.GetTowerFromId("Druid-200").GetAttackModel().weapons[1].Duplicate());

                        CreateLightningEffectModel behavior = ProjectileModelBehaviorExt.GetBehavior<CreateLightningEffectModel>(attackModel.weapons[2].projectile);
                        behavior.displayPaths[0] = CreatePrefabReference<Lightning1>();
                        behavior.displayPaths[1] = CreatePrefabReference<Lightning2>();
                        behavior.displayPaths[2] = CreatePrefabReference<Lightning3>();
                        behavior.displayPaths[3] = CreatePrefabReference<Lightning4>();
                        behavior.displayPaths[4] = CreatePrefabReference<Lightning5>();
                        behavior.displayPaths[5] = CreatePrefabReference<Lightning6>();
                        behavior.displayPaths[6] = CreatePrefabReference<Lightning7>();
                        behavior.displayPaths[7] = CreatePrefabReference<Lightning8>();
                        behavior.displayPaths[8] = CreatePrefabReference<Lightning9>();

                        attackModel.weapons[2].projectile.pierce *= 1.2f;
                        attackModel.weapons[2].projectile.GetDamageModel().damage += 2;
                        attackModel.weapons[2].rate *= 0.75f;
                    }
                }
            }
        }

        public class DarkInfusion : UpgradePlusPlus<MerMonkeyPath>
        {
            public override int Cost => 2500;
            public override int Tier => 3;
            public override string Icon => "I0003";
            public override string Portrait => "P0003";

            public override string Description => "Transfer dark bending onto its trident, making it deal huge damage at the cost of 75% of its rate.";

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                {
                    foreach (var weaponModel in towerModel.GetWeapons())
                    {
                        foreach (var attackModel in towerModel.GetAttackModels())
                        {
                            attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustPierceModel>().projectile.GetDamageModel().damage += 10;
                            attackModel.weapons[1].projectile.GetBehavior<CreateProjectileOnExhaustPierceModel>().projectile.GetDamageModel().damage += 10;
                            attackModel.weapons[0].rate += .75f;
                            attackModel.weapons[1].rate += .75f;
                            attackModel.weapons[0].projectile.ApplyDisplay<Trident>();
                            attackModel.weapons[1].projectile.ApplyDisplay<Trident>();

                            attackModel.weapons[2].projectile.GetDamageModel().damage += 2;
                            attackModel.weapons[2].rate -= 0.07f;
                        }
                    }
                }
            }

            public class DarkGust : UpgradePlusPlus<MerMonkeyPath>
            {
                public override int Cost => 7500;
                public override int Tier => 4;
                public override string Icon => "I0004";
                public override string Portrait => "P0004";

                public override string Description => "Channels the chaotic winds from the darkness, twisting and forming it into a large whirlwind of pure dark magical essence, blowing away tons of Bloons and dealing high damage while doing so. Attack range increased slightly.";

                public override void ApplyUpgrade(TowerModel towerModel)
                {
                    {
                        var newAttackModel = Game.instance.model.GetTowerFromId("DartMonkey").GetAttackModel().Duplicate();

                        towerModel.GetAttackModel().weapons[2].projectile.GetDamageModel().damage += 3;
                        towerModel.GetAttackModel().weapons[2].rate -= 0.07f;

                        towerModel.GetAttackModel().weapons[0].rate -= .25f;
                        towerModel.GetAttackModel().weapons[1].rate -= .25f;

                        newAttackModel.weapons[0].rate = 8f;
                        newAttackModel.weapons[0].projectile.ApplyDisplay<Effect>();
                        newAttackModel.weapons[0].projectile.pierce = 10000000;
                        newAttackModel.weapons[0].projectile.GetDamageModel().damage = 35;
                        var KnockbackMeme = Game.instance.model.GetTowerFromId("NinjaMonkey-010").GetWeapon().projectile.GetBehavior<WindModel>().Duplicate<WindModel>();
                        KnockbackMeme.chance = 0.1f;
                        KnockbackMeme.distanceMin = 45;
                        KnockbackMeme.distanceMax = 70;
                        KnockbackMeme.affectMoab = true;
                        newAttackModel.weapons[0].projectile.AddBehavior(KnockbackMeme);
                        newAttackModel.weapons[0].projectile.GetBehavior<TravelStraitModel>().lifespan = 300;
                        newAttackModel.weapons[0].projectile.GetBehavior<TravelStraitModel>().speed = 50f;
                        newAttackModel.weapons[0].projectile.GetBehavior<TravelStraitModel>().speedFrames = 50f;
                        newAttackModel.weapons[0].projectile.GetBehavior<TravelStraitModel>().Speed = 50f;
                        newAttackModel.weapons[0].projectile.GetBehavior<TravelStraitModel>().Lifespan = 300;
                        newAttackModel.weapons[0].projectile.GetBehavior<TravelStraitModel>().lifespanFrames = 300;
                        towerModel.AddBehavior(newAttackModel);
                        towerModel.GetAttackModel().range += 10;
                        towerModel.GetAttackModel(1).range += 10;
                        towerModel.range += 10;
                    }
                }

                public class EyeOfTheStorm : UpgradePlusPlus<MerMonkeyPath>
                {
                    public override int Cost => 75000;
                    public override int Tier => 5;
                    public override string Icon => "I0005";
                    public override string Portrait => "P0005";

                    public override string Description => "No one should wields this much dark energy not even the VTSG";

                    public override void ApplyUpgrade(TowerModel towerModel)
                    {
                        {
                            OrbitModel orbitModel = Game.instance.model.GetTowerFromId("BoomerangMonkey-502").GetBehavior<OrbitModel>().Duplicate();

                            orbitModel.projectile.ApplyDisplay<MiniEffect>();
                            orbitModel.count = 8;
                            orbitModel.range += 5;
                            orbitModel.projectile.id = "TornadoGlaive";

                            towerModel.GetAttackModel().weapons[2].projectile.pierce += 999;

                            towerModel.AddBehavior(orbitModel);

                            towerModel.GetAttackModel().weapons[2].projectile.GetDamageModel().damage += 350;
                            towerModel.GetAttackModel().weapons[2].rate = 0.5f;

                            towerModel.GetAttackModel().weapons[0].rate = 0.2f;
                            towerModel.GetAttackModel().weapons[1].rate = 0.2f;

                            towerModel.GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnExhaustPierceModel>().projectile.GetDamageModel().damage += 3000;
                            towerModel.GetAttackModel().weapons[1].projectile.GetBehavior<CreateProjectileOnExhaustPierceModel>().projectile.GetDamageModel().damage += 3000;

                            towerModel.GetAttackModel(1).weapons[0].rate = 2f;
                            towerModel.GetAttackModel(1).weapons[0].emission = new ArcEmissionModel("ArcEmissionModel_", 3, 0, 100, null, false, false);
                            towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<WindModel>().chance = 0.4f;
                            towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<WindModel>().distanceMin = 140;
                            towerModel.GetAttackModel(1).weapons[0].projectile.GetBehavior<WindModel>().distanceMax = 140;
                            towerModel.GetAttackModel(1).weapons[0].projectile.GetDamageModel().damage += 575;

                            towerModel.GetAttackModel().range += 20;
                            towerModel.GetAttackModel(1).range += 20;
                            towerModel.range += 20;
                        }
                    }
                    public class Effect : ModDisplay
                    {
                        public override string BaseDisplay => "efc680bebf80d1e4584e9548ddfbff34";

                        public override void ModifyDisplayNode(UnityDisplayNode node)
                        {
                            node.transform.GetChild(0).transform.localScale *= 3f;

                            ParticleSystemRenderer[] renderers = node.GetComponentsInChildren<ParticleSystemRenderer>();

                            foreach (var renderer in renderers)
                            {
                                if (renderer != null && IsTargetRenderer(renderer))
                                {
                                    ChangeMaterialColor(renderer.materials[0], Color.black);
                                }
                            }
                        }

                        private bool IsTargetRenderer(ParticleSystemRenderer renderer)
                        {
                            bool isTarget = renderer.name.Contains("Sparks") || renderer.name.Contains("Wind");
                            return isTarget;
                        }

                        private void ChangeMaterialColor(Material material, Color newColor)
                        {
                            if (material != null)
                            {
                                if (material.HasProperty("_Color"))
                                {
                                    material.SetColor("_Color", newColor);
                                }
                                else if (material.HasProperty("_BaseColor"))
                                {
                                    material.SetColor("_BaseColor", newColor);
                                }
                                else if (material.HasProperty("_TintColor"))
                                {
                                    material.SetColor("_TintColor", newColor);
                                }
                                else if (material.HasProperty("_EmissionColor"))
                                {
                                    material.SetColor("_EmissionColor", newColor);
                                    material.EnableKeyword("_EMISSION");
                                }
                            }
                        }


                        public class Trident : ModDisplay
                        {
                            public override string BaseDisplay => Generic2dDisplay;

                            public override void ModifyDisplayNode(UnityDisplayNode node)
                            {
                                Set2DTexture(node, "DarkTrident");
                            }
                        }

                        public class Lightning1 : ModDisplay
                        {
                            public override string BaseDisplay => "548c26e4e668dac4a850a4c016916016";

                            public override void ModifyDisplayNode(UnityDisplayNode node)
                            {
                                SpriteRenderer renderer = node.GetRenderer<SpriteRenderer>(node);
                                renderer.color = Color.black;
                                renderer.SetOutlineColor(Color.gray);
                            }
                        }

                        public class Lightning2 : ModDisplay
                        {
                            public override string BaseDisplay => "ffed377b3e146f649b3e6d5767726a44";

                            public override void ModifyDisplayNode(UnityDisplayNode node)
                            {
                                SpriteRenderer renderer = node.GetRenderer<SpriteRenderer>(node);
                                renderer.color = Color.black;
                                renderer.SetOutlineColor(Color.gray);
                            }
                        }

                        public class Lightning3 : ModDisplay
                        {
                            public override string BaseDisplay => "c5e4bf0202becd0459c47b8184b4588f";

                            public override void ModifyDisplayNode(UnityDisplayNode node)
                            {
                                SpriteRenderer renderer = node.GetRenderer<SpriteRenderer>(node);
                                renderer.color = Color.black;
                                renderer.SetOutlineColor(Color.gray);
                            }
                        }

                        public class Lightning4 : ModDisplay
                        {
                            public override string BaseDisplay => "3e113b397a21a3a4687cf2ed0c436ec8";

                            public override void ModifyDisplayNode(UnityDisplayNode node)
                            {
                                SpriteRenderer renderer = node.GetRenderer<SpriteRenderer>(node);
                                renderer.color = Color.black;
                                renderer.SetOutlineColor(Color.gray);
                            }
                        }

                        public class Lightning5 : ModDisplay
                        {
                            public override string BaseDisplay => "c6c2049a0c01e8a4d9904db8c9b84ca0";

                            public override void ModifyDisplayNode(UnityDisplayNode node)
                            {
                                SpriteRenderer renderer = node.GetRenderer<SpriteRenderer>(node);
                                renderer.color = Color.black;
                                renderer.SetOutlineColor(Color.gray);
                            }
                        }

                        public class Lightning6 : ModDisplay
                        {
                            public override string BaseDisplay => "e9b2a3d6f0fe0e4419a423e4d2ebe6f6";

                            public override void ModifyDisplayNode(UnityDisplayNode node)
                            {
                                SpriteRenderer renderer = node.GetRenderer<SpriteRenderer>(node);
                                renderer.color = Color.black;
                                renderer.SetOutlineColor(Color.gray);
                            }
                        }

                        public class Lightning7 : ModDisplay
                        {
                            public override string BaseDisplay => "c8471dcde4c65fc459f7846c6a932a8c";

                            public override void ModifyDisplayNode(UnityDisplayNode node)
                            {
                                SpriteRenderer renderer = node.GetRenderer<SpriteRenderer>(node);
                                renderer.color = Color.black;
                                renderer.SetOutlineColor(Color.gray);
                            }
                        }

                        public class Lightning8 : ModDisplay
                        {
                            public override string BaseDisplay => "a73b565de9c31c14ebcd3317705ab17e";

                            public override void ModifyDisplayNode(UnityDisplayNode node)
                            {
                                SpriteRenderer renderer = node.GetRenderer<SpriteRenderer>(node);
                                renderer.color = Color.black;
                                renderer.SetOutlineColor(Color.gray);
                            }
                        }

                        public class Lightning9 : ModDisplay
                        {
                            public override string BaseDisplay => "bd23939e7362b8e40a3a39f595a2a1dc";

                            public override void ModifyDisplayNode(UnityDisplayNode node)
                            {
                                SpriteRenderer renderer = node.GetRenderer<SpriteRenderer>(node);
                                renderer.color = Color.black;
                                renderer.SetOutlineColor(Color.gray);
                            }
                        }

                        public class MiniEffect : ModDisplay
                        {
                            public override string BaseDisplay => "efc680bebf80d1e4584e9548ddfbff34";

                            public override void ModifyDisplayNode(UnityDisplayNode node)
                            {

                                ParticleSystemRenderer[] renderers = node.GetComponentsInChildren<ParticleSystemRenderer>();

                                foreach (var renderer in renderers)
                                {
                                    if (renderer != null && IsTargetRenderer(renderer))
                                    {
                                        ChangeMaterialColor(renderer.materials[0], Color.black);
                                    }
                                }
                            }

                            private bool IsTargetRenderer(ParticleSystemRenderer renderer)
                            {
                                bool isTarget = renderer.name.Contains("Sparks") || renderer.name.Contains("Wind");
                                return isTarget;
                            }

                            private void ChangeMaterialColor(Material material, Color newColor)
                            {
                                if (material != null)
                                {
                                    if (material.HasProperty("_Color"))
                                    {
                                        material.SetColor("_Color", newColor);
                                    }
                                    else if (material.HasProperty("_BaseColor"))
                                    {
                                        material.SetColor("_BaseColor", newColor);
                                    }
                                    else if (material.HasProperty("_TintColor"))
                                    {
                                        material.SetColor("_TintColor", newColor);
                                    }
                                    else if (material.HasProperty("_EmissionColor"))
                                    {
                                        material.SetColor("_EmissionColor", newColor);
                                        material.EnableKeyword("_EMISSION");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}