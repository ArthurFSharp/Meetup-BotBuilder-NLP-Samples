﻿namespace RestaurantBot.Telemetry
{
    public interface ITelemetryService
    {
        void UnrecognizedIntent(string channel, string userId, string message, double witConfidence);
        void OrderAsked(string channel, string userId, string message, double witConfidence);
        void ReservationAsked(string channel, string userId, string message, double witConfidence);
    }
}
