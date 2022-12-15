using System;
using System.Collections.Generic;
using Services.Common;

namespace Services
{
    public interface IMatchMakingService
    {
        public void Register(IEnumerable<long> roster);

        public void Cancel();

        public void Status(EventHandler<MatchDto> onSuccess);

        public void ApplyForServer(string address, string port, EventHandler<MatchDto> onSuccess);
    }
}