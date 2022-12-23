using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ffplayer.Models
{
  public class Stats
  {
    public string Name { get; set; }
    public string Date { get; set; }
    public string Week { get; set; }
    public string Team { get; set; }
    public string Opp { get; set; }
    public string Carries { get; set; }
    public string Targets { get; set; }
    public string Tds { get; set; }

    public Stats(string name, string date, string week, string team, string opp, string carries, string targets, string tds)
    {
      Name = name;
      Date = date;
      Week = week;
      Team = team;
      Opp = opp;
      Carries = carries;
      Targets = targets;
      Tds = tds;
    }
  }
}
