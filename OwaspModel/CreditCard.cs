using System;

namespace OwaspModel
{
    public class CreditCard
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Cvv { get; set; }
        public string Owner { get; set; }
    }
}