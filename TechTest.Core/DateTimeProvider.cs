using System;
using Tappau.DateTimeProvider.Abstractions;

namespace TechTest.Core
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;

        public DateTime Now => DateTime.Now;
    }
}