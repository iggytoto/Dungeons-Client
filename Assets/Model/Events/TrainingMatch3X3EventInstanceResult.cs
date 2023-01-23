using System;

namespace Model.Events
{
    public class TrainingMatch3X3EventInstanceResult : EventInstanceResult
    {
        public long WinnerId;
        public long UserOneId;
        public long UserTwoId;
        public DateTime Date;
    }
}