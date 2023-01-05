using System;
using Unity.Netcode;

namespace Model.Units
{
    [Serializable]
    public abstract class Equipment : INetworkSerializable
    {
        public long id;
        public long unitId;
        public abstract void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter;

        public static Equipment DefaultForType(UnitType type)
        {
            return type switch
            {
                UnitType.HumanWarrior => new HumanWarriorEquipment(),
                UnitType.HumanSpearman => new HumanSpearmanEquipment(),
                UnitType.HumanArcher => new HumanArcherEquipment(),
                _ => null
            };
        }
    }
}