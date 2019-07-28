using System.Collections.Generic;

namespace ApiControllers.Models
{
    public interface IRepository
    {
        IEnumerable<Year> years { get; }
        IEnumerable<Make> makes { get; }
    }
}
