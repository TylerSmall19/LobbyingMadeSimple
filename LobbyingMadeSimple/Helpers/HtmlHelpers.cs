using LobbyingMadeSimple.Models;
using System;
using System.Collections.Generic;

namespace LobbyingMadeSimple.Helpers
{
    public class HtmlHelpers
    {
        /// <summary>
        /// Gets the CSS Bootstrap property of Success or Danger or text elements.
        /// 
        /// Used to color the strings of percentages and give back properties to javascript via JSON
        /// </summary>
        /// <param name="percentage">The percentage to check</param>
        /// <returns>text-success if percentage is above 67 percent, text-danger otherwise</returns>
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

        /// <summary>
        /// Gets the vote button property needed to display the proper color of the voting buttons
        /// </summary>
        /// <param name="userId">The UserId of the User voting (to filter Issue Votes)</param>
        /// <param name="issue">The Issue object being checked</param>
        /// <param name="isUpVoteButton">Denotes the type of button the property is being displayed on</param>
        /// <returns></returns>
        public static string GetVoteButtonColor(string userId, Issue issue, bool isUpVoteButton)
        {
            Vote vote = issue.GetVoteForUser(userId);

            if (vote != null)
            {
                if (vote.IsUpvote)
                {
                    return isUpVoteButton ? "btn-success" : "btn-default";
                } else
                {
                    return !isUpVoteButton ? "btn-danger" : "btn-default";
                }
            }

            return "btn-primary";
        }
    }
}