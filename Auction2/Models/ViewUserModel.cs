using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction2.Models
{
    public class ViewUserModel
    {
        public List<BidUserModel> biduser { get; set; }
        public List<LoginUserModel> loginuser { get; set; }
    }
}