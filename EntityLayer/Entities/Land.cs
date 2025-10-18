namespace EntityLayer.Entities;

public partial class Land : Property
{
    public bool ZoningStatus { get; set; }
    public string? SheetNumber { get; set; }
    public bool LandShareEligibility { get; set; }
    public bool TitleSheetState { get; set; }
}
