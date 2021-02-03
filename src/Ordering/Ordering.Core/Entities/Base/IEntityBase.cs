using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Entities.Base
{
    public class IEntityBase<TId>
    {
        TId Id { get; }
    }
}
