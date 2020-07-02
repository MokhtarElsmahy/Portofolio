using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class PortofolioItem : EntityBase
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }
}
