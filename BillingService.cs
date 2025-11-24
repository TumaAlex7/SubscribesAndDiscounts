using System;

namespace SubscribesAndDiscounts
{
    public static class BillingService
    {
        public static double CalcTotal(Subscriber s)
        {
            ArgumentNullException.ThrowIfNull(s);

            double WithDevices(double x) => s.HasManyDevices ? x + 4.99 : x;
            double WithTax(double x) => s.Region switch
            {
                "EU" => x * 1.21,
                "US" => x * 1.07,
                _ => x
            };
            return WithTax(WithDevices(s.GetStatusRation));
        }
   }
}