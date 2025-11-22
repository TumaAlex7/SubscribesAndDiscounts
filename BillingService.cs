using System;

namespace SubscribesAndDiscounts
{
    public static class BillingService
    {
        public static double CalcTotal(Subscriber s)
        {
            ArgumentNullException.ThrowIfNull(s);

            double PriceAfterStatus() => ApplyStatusDiscount(s.Status, s.TenureMonths, s.BasePrice);
            double WithDevices(double x) => ApplyDeviceTax(s.Devices, x);
            double WithTax(double x) => ApplyTax(s.Region, x);
            return WithTax(WithDevices(PriceAfterStatus()));
        }

        private static double ApplyStatusDiscount(SubscriptionStatus status, int tenure, double basePrice)
        => status switch
        {
            SubscriptionStatus.Trial => 0,
            SubscriptionStatus.Student => basePrice * 0.5,
            SubscriptionStatus.Pro => tenure switch
            {
                >= 24 => basePrice * 0.85,
                >= 12 => basePrice * 0.9,
                _ => basePrice
            },
            SubscriptionStatus.Base => basePrice,
            _ => throw new ArgumentException("Invalid subscription status")
        };

        private static double ApplyDeviceTax(int devices, double basePrice) => devices switch
        {
            >= 3 => basePrice + 4.99,
            _ => basePrice
        };

        private static double ApplyTax(string region, double basePrice) => region switch
        {
            "EU" => basePrice * 1.21,
            "US" => basePrice * 1.07,
            _ => throw new ArgumentException("Invalid region")
        };
   }
}