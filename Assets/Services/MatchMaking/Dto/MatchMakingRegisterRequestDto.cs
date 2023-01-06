using System;
using System.Collections.Generic;

namespace Services.MatchMaking
{
    [Serializable]
    public class MatchMakingRegisterRequestDto
    {
        public List<long> rosterUnitsIds;
    }
}