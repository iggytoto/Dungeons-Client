using System;
using Services.Common.Dto;

namespace Services.MatchMaking
{
    [Serializable]
    public class MatchMakingApplyAsServerRequestDto : RequestDto
    {
        public string ip;
        public string port;
    }
}