using System;
using System.Collections.Generic;
using Model.Events;
using Services.Dto;
using Services.Events;
using EventType = Model.Events.EventType;

namespace Services
{
    /**
     * Game events service
     */
    public interface IEventsService : IService
    {
        /**
         * Current event that is being processed by the server instance.
         * Sets automatically after successfull call ApplyAsServer.
         * Removes automatically after successufll call SaveResult.
         */
        public EventInfo EventInfo { get; }

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
         * Gets units list that participate in event
         */
        public void GetEventInstanceRosters(EventHandler<List<Unit>> onSuccessHandler,
            EventHandler<ErrorResponseDto> onError);

        /**
         * Server method for saving processing result
         */
        public void SaveResult(EventInstanceResult result, EventHandler<ErrorResponseDto> onError);
    }
}