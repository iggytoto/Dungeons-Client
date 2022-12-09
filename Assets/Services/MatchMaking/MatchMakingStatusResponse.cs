using System;
using Services.Common;
using Services.Dto;

namespace Services.MatchMaking
{
    [Serializable]
    public class MatchMakingStatusResponse : ResponseBase
    {
        public MatchDto match;
    }
}