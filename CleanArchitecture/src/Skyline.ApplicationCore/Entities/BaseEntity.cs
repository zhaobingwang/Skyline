using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.ApplicationCore.Entities
{
    public class BaseEntity<TKey>
    {
        public virtual TKey Id { get; protected set; }
    }
}
