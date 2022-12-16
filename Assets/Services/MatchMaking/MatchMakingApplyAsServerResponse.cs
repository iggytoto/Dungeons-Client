using System;
using Services.Common;
using Services.Dto;

namespace Services.MatchMaking
{
    [Serializable]
    public class MatchMakingApplyAsServerResponse : ResponseBaseDto
    {
        public MatchDto match;
    }
}