using System;

namespace SubscribesAndDiscounts
{
    public enum SubscriptionStatus : byte
    {
        Trial,
        Base,
        Pro,
        Student
    }

    public class Subscriber(string id, string region, SubscriptionStatus status)
    {
        public string Id { get; } = string.IsNullOrWhiteSpace(id) ? throw new ArgumentException("Id required") : id;
        public string Region { get; } = string.IsNullOrWhiteSpace(region) ? throw new ArgumentException("Region required") : region;
        public SubscriptionStatus Status { get; } = status;

        public int TenureMonths
        {
            get;
            set => field = value < 0 ? throw new ArgumentException("Tenure months must be non-negative") : value;
        }
        public int Devices 
        {
            get;
            set => field = value <= 0 ? throw new ArgumentException("Devices must be greater than 0") : value;
        }
        public double BasePrice
        {
            get;
            set => field = value <= 0 ? throw new ArgumentException("Base price must be greater than 0") : value;
        }
    }
}