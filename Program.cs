using System;
using System.Collections.Generic;

namespace SubscribesAndDiscounts
{
    class Program
    {
        static void Main(string[] args)
        {
            var subscribers = new List<Subscriber>
            {
                new Subscriber("A-1", "EU", SubscriptionStatus.Trial) {
                    TenureMonths = 0, Devices = 1, BasePrice = 9.99 },
                new Subscriber("B-2", "US", SubscriptionStatus.Pro) {
                    TenureMonths = 18, Devices = 4, BasePrice = 14.99 },
                new Subscriber("C-3", "EU", SubscriptionStatus.Student) {
                    TenureMonths = 6, Devices = 2, BasePrice = 12.99 }
            };

            foreach (var subscriber in subscribers)
            {
                Console.WriteLine($"{subscriber.Id} - {subscriber.Region} - {subscriber.Status} - {subscriber.TenureMonths} - {subscriber.Devices} - {subscriber.BasePrice} - {BillingService.CalcTotal(subscriber)}");
            }
        }
    }
}
