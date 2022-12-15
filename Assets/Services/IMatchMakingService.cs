using System.Collections.Generic;
using Services.Common;

namespace Services
{
    public interface IMatchMakingService
    {
        public MatchDto MatchContext { get; }

        public void Register(IEnumerable<long> roster);

        public void Cancel();

        public void ApplyForServer(string address, string port);
    }
}