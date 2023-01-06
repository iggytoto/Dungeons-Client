using System;

namespace Services.MatchMaking
{
    [Serializable]
    public class MatchMakingApplyAsServerRequestDto
    {
        public string ip;
        public string port;
    }
}