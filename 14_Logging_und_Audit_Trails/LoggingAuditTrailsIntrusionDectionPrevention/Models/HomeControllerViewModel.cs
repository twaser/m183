﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoggingAuditTrailsIntrusionDectionPrevention.Models
{
    public class HomeControllerViewModel
    {
        public string UserId { get; internal set; }
        public string LogId { get; internal set; }
        public string LogCreatedOn { get; internal set; }
    }
}