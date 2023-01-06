using System;
using Newtonsoft.Json;
using Services.Common;
using Services.Dto;

namespace Services.MatchMaking
{
    [Serializable]
    public class MatchMakingStatusResponseDto : ResponseBaseDto
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MatchDto match;
    }
}