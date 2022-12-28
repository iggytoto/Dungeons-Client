using System;
using System.Collections.Generic;
using Services.Common.Dto;

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
        public List<UnitDto> unitsState;
    }
}