using System;
using System.Collections.Generic;

namespace Services.MatchMaking
{
    [Serializable]
    public class MatchMakingRegisterRequest
    {
        public List<long> rosterUnitsIds;
    }
}