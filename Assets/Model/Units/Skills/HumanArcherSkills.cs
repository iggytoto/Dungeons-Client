using System;
using Unity.Netcode;

namespace Model.Units
{
    [Serializable]
    public class HumanArcherSkills : Skills, INetworkSerializable
    {
        public static readonly string MidRangeParamName = "midRange";
        public static readonly string LongRangeParamName = "longRange";
        public static readonly string FireArrowsParamName = "fireArrow";
        public static readonly string PoisonArrowsParamName = "poisonArrow";

        public long midRangePoints;
        public long longRangePoints;
        public bool fireArrows;
        public bool poisonArrows;

        public override void NetworkSerialize<T>(BufferSerializer<T> serializer)
        {
            serializer.SerializeValue(ref id);
            serializer.SerializeValue(ref unitId);
            serializer.SerializeValue(ref midRangePoints);
            serializer.SerializeValue(ref longRangePoints);
            serializer.SerializeValue(ref fireArrows);
            serializer.SerializeValue(ref poisonArrows);
        }
    }
}