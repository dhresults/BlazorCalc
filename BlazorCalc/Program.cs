using WebWindows.Blazor;
using System;

namespace BlazorCalc
{
    public class Program
    {
        static void Main(string[] args)
        {
            ComponentsDesktop.Run<Startup>("BlazorCalc", "wwwroot/index.html");
        }
    }
}
