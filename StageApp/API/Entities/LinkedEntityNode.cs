using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
public class LinkedEntityNode
{
    public int Id { get; set; }
    public int? NextId { get; set; }
    public int? PreviousId { get; set; }
}
}