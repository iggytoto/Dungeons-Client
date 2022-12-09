using System;

namespace Services.MatchMaking
{
    [Serializable]
    public class MatchMakingApplyAsServerRequest
    {
        public string ip;
        public string port;
    }
}