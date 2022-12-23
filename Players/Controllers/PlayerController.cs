using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Ffplayer.Models;
using System.Net;
using System.Linq;

namespace Ffplayer.Controllers
{
  public class PlayersController : Controller
  {
    static HtmlDocument GetDocument(string url)//this function lets other functions call on a page
      {
          HtmlWeb web = new HtmlWeb();
          HtmlDocument doc = web.Load(url);
          return doc;
      }

    static int FindZero(HtmlNodeCollection nodes, int rank)
    {
      try
      {
        Int32.Parse(nodes[rank].InnerText);
      }
      catch (System.Exception)
      {
        return 0;
        throw;
      }
      return Int32.Parse(nodes[rank].InnerText);
    }
    
      /*static List<Player> GetPlayersName(string url)
      {
        WebClient webClient = new WebClient();
        string page = webClient.DownloadString(url);
        //HtmlDocument doc = GetDocument(url);
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(page);
        //Console.WriteLine(doc.DocumentNode.SelectNodes("/p/strong").InnerText);
        HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//tbody/tr/td[@data-stat=\"player\"]/a");
        List<Player> players = new List<Player>();
        foreach (var node in nodes)
        {
          //Console.WriteLine(node.InnerText);// I can now use this to create players
          /*var nameXpath = "//td[@data-stat=\"player\"]/a";
          var posXpath = "//td[@data-stat=\"pos\"]/a";
          Player temp = new Player(node.InnerText);
          // temp.Name = node.InnerText;
          // temp.Pos = node(posXpath).InnerText;
          //Console.WriteLine(temp.Name);
          players.Add(temp);
        }
        /*foreach(Player temp in players)
        {
          Console.WriteLine(temp.Name);
        }
        return players;
      }
      static List<Player> GetPlayersNameWOcomments(string url)// 
      {
        WebClient webClient = new WebClient();
        string page = webClient.DownloadString(url);
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(page);
        HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//tbody/tr/td[@data-stat=\"player\"]/a");
        List<Player> players = new List<Player>();
        foreach (var node in nodes)
        {
          Player temp = new Player(node.InnerText);
          players.Add(temp);
      }
      return players;
      }*/
      static List<Player> GetPlayers(string url)// this needs to change line 67 and below in order to grab every stat of a player
      {
        WebClient webClient = new WebClient();
        string page = webClient.DownloadString(url);
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(page);
        HtmlNodeCollection names = doc.DocumentNode.SelectNodes("//tbody/tr/td[@data-stat=\"player\"]/a");
        HtmlNodeCollection pos = doc.DocumentNode.SelectNodes("//tbody/tr/td[@data-stat=\"pos\"]");
        HtmlNodeCollection targets = doc.DocumentNode.SelectNodes("//tbody/tr/td[@data-stat=\"targets\"]");
        HtmlNodeCollection carries = doc.DocumentNode.SelectNodes("//tbody/tr/td[@data-stat=\"rush_att\"]");
        HtmlNodeCollection yards = doc.DocumentNode.SelectNodes("//tbody/tr/td[@data-stat=\"yds_from_scrimmage\"]");
        HtmlNodeCollection link = doc.DocumentNode.SelectNodes("//tbody/tr/td[@data-stat=\"player\"]/a");
        List<Player> players = new List<Player>();
        for (int rank = 1; rank < names.Count+1; rank++)
        {
          Player temp = new Player(rank, names[rank-1].InnerText, pos[rank-1].InnerText, FindZero(carries, rank-1), FindZero(targets, rank-1), Int32.Parse(yards[rank-1].InnerText), names[rank-1].Attributes["href"].Value);
          players.Add(temp);
        }
      return players;
      }
      static List<Stats> GetStats(string url)
      {
        WebClient webClient = new WebClient();
        string page = webClient.DownloadString(url);
        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(page);

        HtmlNode name = doc.DocumentNode.SelectSingleNode("//div/h1/span");
        //HtmlNode pos = doc.DocumentNode.SelectSingleNode("//td[@data-stat\"pos\"]");
        HtmlNodeCollection dates = doc.DocumentNode.SelectNodes("//tbody/tr/th[@data-stat=\"game_date\"]/a");
        HtmlNodeCollection weeks = doc.DocumentNode.SelectNodes("//tbody/tr/td[@data-stat=\"week_num\"]");
        //HtmlNodeCollection teams = doc.DocumentNode.SelectNodes("//tbody/tr/th[@data-stat=\"team\"]/a");
        HtmlNodeCollection opps = doc.DocumentNode.SelectNodes("//tbody/tr/th[@data-stat=\"opp\"]/a");
        HtmlNodeCollection carries = doc.DocumentNode.SelectNodes("//tbody/tr/td[@data-stat=\"rush_att\"]");
        HtmlNodeCollection targets = doc.DocumentNode.SelectNodes("//tbody/tr/td[@data-stat=\"targets\"]");
        HtmlNodeCollection tds = doc.DocumentNode.SelectNodes("//tbody/tr/td[@data-stat=\"all_td\"]");
        List<Stats> statlist = new List<Stats>();
        for (int i = 0; i < dates.Count; i++)
        {
          Stats temp = new Stats(name.InnerText, dates[i].InnerText, weeks[i].InnerText, "IND", "HOU", carries[i].InnerText, targets[i].InnerText, tds[i].InnerText);
          statlist.Add(temp);
        }

        return statlist;
      }
    public ActionResult Index()
    {
      return View();
    }
    public ActionResult two021()
    {
      List<Player> players = GetPlayers("https://www.pro-football-reference.com/years/2021/scrimmage.htm");
      return View(players);
    }
    public ActionResult two022()
    {
      List<Player> players = GetPlayers("https://www.pro-football-reference.com/years/2022/scrimmage.htm");
      return View(players);
    }
    public ActionResult specificPlayer(string link)
    {
      List<Stats> stats = GetStats(string.Format("https://www.pro-football-reference.com{0}", link));
      return View(stats);
    }
  }
}