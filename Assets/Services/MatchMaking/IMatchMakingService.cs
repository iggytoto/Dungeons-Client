using System;
using System.Collections.Generic;
using Services.Common;
using Services.Dto;

namespace Services
{
    /**
     * Matchmaking service is entry point for registering on a match as a client/player and as the server that
     * is responsible to handle the match.
     */
    [Obsolete("Will be transferred to the events")]
    public interface IMatchMakingService : IService
    {
        /**
         * Player/client entry point. Player registers roster on a match.
         */
        public void Register(
            IEnumerable<long> roster,
            EventHandler<MatchDto> onSuccess,
            EventHandler<ErrorResponseDto> onError);

        /**
         * Player can cancel its request for a match. This is working until server is not found for the match.
         */
        public void Cancel();

        /**
         * Player can request match status.
         */
        public void Status(
            EventHandler<MatchDto> onSuccess,
            EventHandler<ErrorResponseDto> onError);

        /**
         * Server that is responsible to handle the match can register and apply its services as the players
         * cannot host the matches, only servers do.
         * Receives not null response match if match found for server successfully, otherwise null 
         */
        public void ApplyForServer(
            string address,
            string port,
            EventHandler<MatchDto> onSuccess,
            EventHandler<ErrorResponseDto> onError);
    }
}