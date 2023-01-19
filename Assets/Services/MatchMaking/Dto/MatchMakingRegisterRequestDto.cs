using System;
using System.Collections.Generic;
using Services.Common.Dto;

namespace Services.MatchMaking
{
    [Serializable]
    public class MatchMakingRegisterRequestDto : RequestDto
    {
        public List<long> rosterUnitsIds;
    }
}