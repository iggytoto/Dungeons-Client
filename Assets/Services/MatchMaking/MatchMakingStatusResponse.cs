using System;
using Services.Common;
using Services.Dto;

namespace Services.MatchMaking
{
    [Serializable]
    public class MatchMakingStatusResponse : ResponseBaseDto
    {
        public MatchDto match;
    }
}