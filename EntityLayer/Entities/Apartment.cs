
using System;
using System.Collections.Generic;

namespace EntityLayer.Entities;

public partial class Apartment : Property
{
    public int? SquareMeterSizeGross { get; set; }
    public string NumberOfRooms { get; set; }
    public string? AgeOfBuilding { get; set; }
    public int? Floor { get; set; }
    public int? NumberOfFloors { get; set; }
    public string HeatingType { get; set; }
    public int? NumberOfBathRooms { get; set; }
    public int? NumberOfBalcony { get; set; }
    public bool HasElevator { get; set; }
    public bool HasParkingLot { get; set; }
    public bool IsFurnished { get; set; }
    public string? UsageState { get; set; }
    public bool IsInResidentalComplex { get; set; }
    public bool IsEligibleForLoan { get; set; }
    public int Dues { get; set; }
}
