using System;
using Unity.Netcode;

namespace Model.Units
{
    [Serializable]
    public class HumanClericEquipment : Equipment, INetworkSerializable
    {
        public static readonly string Discipline = "discipline";
        public static readonly string Shatter = "shatter";
        public static readonly string Divine = "divine";
        public static readonly string Purge = "purge";

        public long disciplinePoints;
        public bool shatter;
        public bool divine;
        public bool purge;

        public override void NetworkSerialize<T>(BufferSerializer<T> serializer)
        {
            serializer.SerializeValue(ref id);
            serializer.SerializeValue(ref unitId);
            serializer.SerializeValue(ref disciplinePoints);
            serializer.SerializeValue(ref shatter);
            serializer.SerializeValue(ref divine);
            serializer.SerializeValue(ref purge);
        }
    }
}