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
            UnitType.Dummy => "Dummy its not a unit. Actually you should not see this text at all because dummy units are not supposed to appear in the live game. If you see this please notify developers.",
            UnitType.HumanWarrior => "Human warrior unit is equipped by two-handed longsword and heavy armor. Warriors are mainly frontline units and capable to absorb large amount of damage or deal a lot of it, depends on the skill build." +
                                     "Skills based on two pillars: offence and defence. Pretty obvious, defence skills upgrade make warrior more tanky and resistant to damage and offence make it do more damage." +
                                     "Ability: Spins his sword around himself for 3 seconds, dealing damage to all enemies in AR every 1/AS seconds. Each skill point increases duration by 1 second. Defence skill points boosts defence chars while ability active.",
            UnitType.HumanArcher => "",
            UnitType.HumanSpearman => "",
            UnitType.HumanCleric => "",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}