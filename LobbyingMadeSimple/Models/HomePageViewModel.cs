﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LobbyingMadeSimple.Models
{
    public class HomePageViewModel
    {
        public List<Issue> VotableIssues { get; set; }
        public List<Issue> FundableIssues { get; set; }
    }
}