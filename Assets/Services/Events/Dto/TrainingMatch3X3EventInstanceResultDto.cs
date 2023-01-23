using System;
using Model.Events;

namespace Services.Events.Dto
{
    [Serializable]
    public class TrainingMatch3X3EventInstanceResultDto : EventInstanceResultDto
    {
        public long winnerId;
        public long userOneId;
        public long userTwoId;
        public DateTime date;


        public new static EventInstanceResultDto FromDomain(EventInstanceResult r)
        {
            var domain = (TrainingMatch3X3EventInstanceResult)r;
            return new TrainingMatch3X3EventInstanceResultDto
            {
                eventInstanceId = r.EventInstanceId,
                eventType = r.EventType,
                UnitsHitPoints = r.UnitsHitPoints,
                winnerId = domain.WinnerId,
                userOneId = domain.UserOneId,
                userTwoId = domain.UserTwoId,
                date = domain.Date
            };
        }
    }
}