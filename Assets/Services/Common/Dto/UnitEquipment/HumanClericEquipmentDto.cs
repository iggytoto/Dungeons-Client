using System;
using Model.Units;

namespace Services.Common.Dto
{
    [Serializable]
    public class HumanClericEquipmentDto : EquipmentDto
    {
        public long disciplinePoints;
        public bool shatter;
        public bool divine;
        public bool purge;


        public override Equipment ToDomain()
        {
            return new HumanClericEquipment
            {
                id = id,
                unitId = unitId,
                disciplinePoints = disciplinePoints,
                shatter = shatter,
                divine = divine,
                purge = purge
            };
        }
    }
}