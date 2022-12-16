using System;

namespace Services.Common
{
    [Serializable]
    public class MatchDto
    {
        public long id;
        public long userOneId;
        public long userTwoId;
        public string status;
        public string serverIpAddress;
        public string serverPort;
        public string createdAt;
    }
}