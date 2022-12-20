using System;
using JetBrains.Annotations;
using Services.Common;
using Services.Dto;

namespace Services.MatchMaking
{
    [Serializable]
    public class MatchMakingStatusResponse : ResponseBaseDto
    {
        [CanBeNull] public MatchDto match;
    }
}