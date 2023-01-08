using System;
using Unity.Netcode;

namespace Model.Units
{
    [Serializable]
    public abstract class Skills : INetworkSerializable
    {
        public long id;
        public long unitId;
        public abstract void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter;

        public static Skills DefaultForType(UnitType type)
        {
            return type switch
            {
                UnitType.HumanWarrior => new HumanWarriorSkills(),
                UnitType.HumanSpearman => new HumanSpearmanSkills(),
                UnitType.HumanCleric => new HumanClericSkills(),
                UnitType.HumanArcher => new HumanArcherSkills(),
                _ => null
            };
        }
    }
}