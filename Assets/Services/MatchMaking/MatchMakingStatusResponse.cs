using System;
using Newtonsoft.Json;
using Services.Common;
using Services.Dto;

namespace Services.MatchMaking
{
    [Serializable]
    public class MatchMakingStatusResponse : ResponseBaseDto
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MatchDto match;
    }
}