using System;

namespace Concepts.Models
{
    // public enum RoomType { Individual, Double, Junior, Suite }
    public class Booking
    {
        public string BookingId { get; set; }
        public string Client { get; set; }
        // 100: Individual; 200: Double; 300: Junior; 400: Suite
        // public RoomType RoomType { get; set; }
        public int RoomType;  // { get; set; } causes CS0206 error - https://stackoverflow.com/a/4520101
        public bool Smoker { get; set; }
    }
}