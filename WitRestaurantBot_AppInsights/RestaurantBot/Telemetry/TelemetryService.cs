using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantBot.Telemetry
{
    public class TelemetryService : ITelemetryService
    {
        private const string MessageReceivedEventName = "message_received";
        private const string OrderAskedEventName = "order_asked";
        private const string ReservationAskedEventName = "reservation_asked";
        private const string UnrecognizedIntentEventName = "not_understood_raised";

        private readonly TelemetryClient _telemetryClient;

        public TelemetryService(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public void OrderAsked(string channel, string userId, string message, double witConfidence, string food, DateTime dateTime)
        {
            var param = new Dictionary<string, string>
            {
                { "food", food },
                { "dateTime", dateTime.ToString("yyyy-MM-dd hh:mm:ss.fffffff") }
            };

            PerformTelemetry(OrderAskedEventName, channel, userId, param);
            MessageReceived(channel, userId, OrderAskedEventName, message, witConfidence);
        }

        public void ReservationAsked(string channel, string userId, string message, double witConfidence, int peopleCount)
        {
            var param = new Dictionary<string, string> { { "peopleCount", peopleCount.ToString() } };

            PerformTelemetry(ReservationAskedEventName, channel, userId, param);
            MessageReceived(channel, userId, OrderAskedEventName, message, witConfidence);
        }

        public void UnrecognizedIntent(string channel, string userId, string message, double witConfidence)
        {
            PerformTelemetry(UnrecognizedIntentEventName, channel, userId);
            MessageReceived(channel, userId, OrderAskedEventName, message, witConfidence);
        }

        private void PerformTelemetry(string eventName, string channel, string userId, Dictionary<string, string> additionalParams = null)
        {
            try
            {
                var properties = new Dictionary<string, string>
                {
                    { "userId", userId },
                    { "channel", channel },
                };

                if (additionalParams != null)
                {
                    properties = properties.Concat(additionalParams).ToDictionary(k => k.Key, v => v.Value);
                }

                _telemetryClient.TrackEvent(eventName, properties);
            }
            catch (Exception)
            {
            }
        }

        private void MessageReceived(string channel, string userId, string action, string message, double witConfidence)
        {
            try
            {
                _telemetryClient.TrackEvent(MessageReceivedEventName, new Dictionary<string, string>
                {
                    { "userId", userId },
                    { "action", action },
                    { "message", message },
                    { "channel", channel },
                    { "confidence", witConfidence.ToString() }
                });
            }
            catch (Exception)
            {
            }
        }
    }
}