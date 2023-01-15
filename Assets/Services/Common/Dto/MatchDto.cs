using System;
using Services.Dto;

namespace Services.Common
{
    [Serializable]
    public class MatchDto : ResponseBaseDto
    {
        public long id;
        public long? userOneId;
        public long? userTwoId;
        public string status;
        public string serverIpAddress;
        public string serverPort;
        public string createdAt;
    }
}