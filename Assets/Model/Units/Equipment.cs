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
    }
}