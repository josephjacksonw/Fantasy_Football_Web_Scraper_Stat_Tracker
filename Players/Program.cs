using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace Ffplayer
{
  public class Program
  {
    static void Main(string[] args)
    {
        var host = new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();
      
      host.Run();
    }
  }
}