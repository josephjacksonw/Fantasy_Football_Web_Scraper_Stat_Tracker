using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ffplayer.Models
{
  public class Player
  {
    public int Rank { get; set; }
    public string Name { get; set; }
    public string Pos { get; set; }
    public int Carries { get; set; }
    public int Targets { get; set; }
    public int Yards { get; set; }
    public string Link { get; set; }
  
    public Player(int rank, string name, string pos, int carries, int targets, int yards, string link)
    {
      Rank = rank;
      Name = name;
      Pos = pos;
      Carries = carries;
      Targets = targets;
      Yards = yards;
      Link = link;
    }
  }
}