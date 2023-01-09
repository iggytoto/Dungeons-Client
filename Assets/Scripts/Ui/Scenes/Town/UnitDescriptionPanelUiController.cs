using System;
using Model.Units;
using TMPro;
using UnityEngine;

public class UnitDescriptionPanelUiController : MonoBehaviour
{
    [SerializeField] private TMP_Text descriptionText;

    public Unit Unit
    {
        set => SetUnit(value);
    }

    private void SetUnit(Unit value)
    {
        descriptionText.text = value.type switch
        {
            UnitType.Dummy =>
                "Dummy its not a unit. Actually you should not see this text at all because dummy units are not supposed to appear in the live game. If you see this please notify developers.",
            UnitType.HumanWarrior =>
                "Human warrior unit is equipped by two-handed longsword and heavy armor. Warriors are mainly frontline units and capable to absorb large amount of damage or deal a lot of it, depends on the skill build." +
                "Skills based on two pillars: offence and defence. Defence skills upgrade make warrior more tanky and resistant to damage and offence make it do more damage." +
                "Ability: Spins his sword around himself for 3 seconds, dealing damage to all enemies in AR every 1/AS seconds. Each skill point increases duration by 1 second. Defence skill points boosts defence chars while ability active.",
            UnitType.HumanArcher =>
                "Human archers is a range combat unit specialized on damage dealing equipped by a bow. Can be skilled between mid and long range combat. While mid range combat provides MS and AS, long range provides AR and DMG increase. Ability Triple shot lets archer to do one attack on 3 random targets. Mid range skill provides additional MS after case for a short period, and long range provides DMG",
            UnitType.HumanSpearman =>
                "Close to mid range combat unit, has extended AR and faster AS but has lack of defence. Specializes on mid range combat or welding double-age spear. Double-edge spear gives chance to damage twice enemy during circle swing, meanwhile mid-range skill provides DMG. Ability Circle swing: swings his spear around dealing DMG to all enemies in AR and throwing them back. Each level of double-edged increases chance of do damage 2 times to each enemy. Each level of mid-range combat increases throwback distance.",
            UnitType.HumanCleric =>
                "Ranged support unit specializes to debuff enemies or buff allies.Clerics are support units can be balanced between support allies or debuff of enemies. While discipline increases magic power of the cleric it has a choice of various additional skill options.Cleric provides massive attack on several targets, allies and enemies, allies gets buffs enemies gets debuffs. Number of targets is calculated on level of discipline and Shatter, Divine and Purge provides additional effects. Base number of targets = 1, each level of discipline gets +1. At discipline level 5 multicast will target 6 targets. Shatter on enemy hit will cause debuff to -25%ARM for 1+Discipline level seconds, on ally hit does +25%ARM for same amount of seconds. Divine on enemy hit will cause magic damage equal 5*Discipline level, on ally hit heals ally for 5*Discipline level HP to ally Purge on enemy hit will remove all positive effects from it, on ally hit will remove all negative effects",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}