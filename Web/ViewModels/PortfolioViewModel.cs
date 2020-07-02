using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class PortfolioViewModel
    {
        public Guid ID { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }

        public IFormFile File { get; set; }//to deal with img or any type of files
    }
}
