using System;
using System.Collections.Generic;
using Model.Events;
using Services.Dto;
using EventType = Model.Events.EventType;

namespace Services
{
    /**
     * Game events service
     */
    public interface IEventsService : IService
    {
        /**
         * Registers player's roster on given event type
         */
        public void Register(List<long> unitsIds, EventType type, EventHandler<ErrorResponseDto> onError);

        /**
         * Gets all events on which player registered
         */
        public void Status(EventHandler<List<Event>> onSuccessHandler, EventHandler<ErrorResponseDto> onError);

        /**
         * Server method for processing application
         */
        public void ApplyAsServer(string host, string port, EventHandler<EventInstance> onSuccessHandler,
            EventHandler<ErrorResponseDto> onError);

        /**
         * Server method for saving processing result
         */
        public void SaveResult(EventInstanceResult result, EventHandler<ErrorResponseDto> onError);
    }
}