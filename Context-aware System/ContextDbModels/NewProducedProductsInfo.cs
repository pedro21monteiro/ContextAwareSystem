using System;
using System.Collections.Generic;

namespace Context_aware_System.ContextDbModels;

public partial class NewProducedProductsInfo
{
    public int Id { get; set; }

    public int ProductionId { get; set; }

    public int ProductionObjective { get; set; }

    public int ProductionProduced { get; set; }

    public int ProductionLineId { get; set; }

    public DateTime VerificationDate { get; set; }
}
