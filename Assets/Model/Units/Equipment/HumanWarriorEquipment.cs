using System;
using Unity.Netcode;

namespace Model.Units
{
    [Serializable]
    public class HumanWarriorEquipment : Equipment, INetworkSerializable
    {
        public static readonly string OffenceParamName = "offence";
        public static readonly string DefenceParamName = "defence";


        public long defencePoints;
        public long offencePoints;

        public override void NetworkSerialize<T>(BufferSerializer<T> serializer)
        {
            serializer.SerializeValue(ref id);
            serializer.SerializeValue(ref unitId);
            serializer.SerializeValue(ref defencePoints);
            serializer.SerializeValue(ref offencePoints);
        }
    }
}