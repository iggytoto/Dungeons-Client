using System;
using Services.Common;
using Services.Dto;

namespace Services.MatchMaking
{
    [Serializable]
    public class MatchMakingApplyAsServerResponse : ResponseBase
    {
        public MatchDto match;
    }
}