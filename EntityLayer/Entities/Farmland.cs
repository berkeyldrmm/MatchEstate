﻿
using EntityLayer.Entities;
using System;
using System.Collections.Generic;

namespace EntityLayer.Entities;

public partial class Farmland : Property
{
    public bool ZoningStatus { get; set; }
    public string? SheetNumber { get; set; }
    public bool TitleDeedState { get; set; }
}
