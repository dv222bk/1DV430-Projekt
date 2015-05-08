<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="_MainNavigation.ascx.cs" Inherits="BellatorTabernae.Pages.Shared._MainNavigation" ViewStateMode="Disabled" %>

<ul>
    <li>
        <asp:HyperLink runat="server" ID="StartHyperLink" 
            NavigateUrl='<%$ RouteUrl:routename=Default %>'>
            Start
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink runat="server" ID="ChatHyperLink" 
            NavigateUrl='<%$ RouteUrl:routename=Chat %>'>
            Chat
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink runat="server" ID="CharacterHyperLink" 
            NavigateUrl='<%$ RouteUrl:routename=Character %>'>
            Karaktär
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink runat="server" ID="MarketHyperLink" 
            NavigateUrl='<%$ RouteUrl:routename=Market %>'>
            Torget
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink runat="server" ID="BattleHyperLink" 
            NavigateUrl='<%$ RouteUrl:routename=Battle %>'>
            Strid
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink runat="server" ID="LeaderboardHyperLink" 
            NavigateUrl='<%$ RouteUrl:routename=Leaderboard %>'>
            Topplista
        </asp:HyperLink>
    </li>
</ul>