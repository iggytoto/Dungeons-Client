using System;

namespace Services.Common
{
    [Serializable]
    public class MatchResultDto
    {
        public long userOneId;
        public long userTwoId;
        public long winnerUserId;
        public string matchType;
        public DateTime date;
    }
}