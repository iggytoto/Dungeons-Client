using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model.Events;
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
         * Sets automatically after successful call ApplyAsServer.
         * Removes automatically after successful call SaveResult.
         */
        public EventInstance EventInfo { get; }

        /**
         * Information about events that user is registered to.
         */
        public ObservableCollection<EventInstance> EventInfos { get; }

        /**
         * Registers player's roster on given event type
         */
        public void Register(List<long> unitsIds, EventType type, Action<Event> onSuccess, Action<string> onError);

        /**
         * Cancel event registration 
         */
        public void Cancel(long eventId, Action<string> onError);

        /**
         * Request status on all events for the player
         */
        public void Status(Action<List<EventInstance>> onSuccess, Action<string> onError);

        /**
         * Server method for processing application
         */
        public void ApplyAsServer(string host, string port, Action<EventInstance> onSuccessHandler,
            Action<string> onError);

        /**
         * Gets units list that participate in event
         */
        public void GetEventInstanceRosters(Action<List<Unit>> onSuccessHandler,
            Action<string> onError);

        /**
         * Server method for saving processing result
         */
        public void SaveResult(EventInstanceResult result, Action<string> onError);
    }
}