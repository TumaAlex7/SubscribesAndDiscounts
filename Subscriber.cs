using System;

namespace SubscribesAndDiscounts
{
    public enum SubscriptionStatus : byte
    {
        Trial,
        Basic,
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

        public bool HasManyDevices => Devices > 3;

        public double GetStatusRation => Status switch
        {
            SubscriptionStatus.Trial => 0,
            SubscriptionStatus.Student => BasePrice * 0.5,
            SubscriptionStatus.Pro => BasePrice * GetTenureRatio,
            SubscriptionStatus.Basic => BasePrice,
            _ => throw new ArgumentException("Invalid subscription status")
        };

        private double GetTenureRatio => TenureMonths switch
        {
                >= 24 => 0.85,
                >= 12 => 0.9,
                _ => 1
        };
    }
}