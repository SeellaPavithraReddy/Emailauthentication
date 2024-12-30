using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApi.Models.Domain
{
    public class Region
    {
        public Guid Id{set;get;}
        public string Code{get;set;}
        public string Name{get;set;}
        public string? RegionImageUrl{get;set;}
    }
}