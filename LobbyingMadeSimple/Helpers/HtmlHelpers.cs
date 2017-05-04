using System;

namespace LobbyingMadeSimple.Helpers
{
    public class HtmlHelpers
    {
        public static string GetCssClassForVotePercentage(string percentage)
        {
            if (Int32.Parse(percentage) >= 67)
            {
                return "text-success";
            } else
            {
                return "text-danger";
            }
        }
    }
}