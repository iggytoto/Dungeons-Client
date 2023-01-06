using System;
using Services.Common;
using Services.Dto;

namespace Services.MatchMaking
{
    [Serializable]
    public class MatchMakingApplyAsServerResponseDto : ResponseBaseDto
    {
        public MatchDto match;
    }
}