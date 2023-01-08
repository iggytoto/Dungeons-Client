using Unity.Netcode;

namespace Model.Units
{
    public class HumanSpearmanSkills : Skills
    {
        public static readonly string DoubleEdgeParamName = "doubleEdge";
        public static readonly string MidRangeParamName = "midRange";

        public long doubleEdgePoints;
        public long midRangePoints;

        public override void NetworkSerialize<T>(BufferSerializer<T> serializer)
        {
            serializer.SerializeValue(ref id);
            serializer.SerializeValue(ref unitId);
            serializer.SerializeValue(ref doubleEdgePoints);
            serializer.SerializeValue(ref midRangePoints);
        }
    }
}