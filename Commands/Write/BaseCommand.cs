using CQRSlite.Commands;
using CQRSlite.Events;

namespace CQRS_with_event_Sourcing_pattern.Commands
{
    public class BaseCommand : ICommand
    {
        /// <summary>
        /// The Aggregate ID of the Aggregate Root being changed
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The Expected Version which the Aggregate will become.
        /// </summary>
        public int ExpectedVersion { get; set; }
    }

    public class BaseEvent : IEvent
    {
        /// <summary>
        /// The ID of the Aggregate being affected by this event
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The Version of the Aggregate which results from this event
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// The UTC time when this event occurred.
        /// </summary>
        public DateTimeOffset TimeStamp { get; set; }
    }
}
