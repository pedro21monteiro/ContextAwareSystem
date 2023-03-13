using System;
using System.Collections.Generic;

namespace Context_aware_System.ContextDbModels;

public partial class StopsVerification
{
    public int Id { get; set; }

    public DateTime VerificationDate { get; set; }

    public int NewStopsCount { get; set; }
}
