<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Shared/Site.Master" CodeBehind="Leaderboard.aspx.cs" Inherits="BellatorTabernae.Pages.Leaderboard" EnableViewState="false" %>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h2>Topplista</h2>
    <asp:Label ID="LeaderboardTypeLabel" runat="server" 
        Text="Välj topplista:"
        AssociatedControlID="LeaderBoardType" />
    <asp:DropDownList ID="LeaderboardType" runat="server"
        AutoPostBack="true">
        <asp:ListItem Text="Högst Level" />
        <asp:ListItem Text="Starkast" />
        <asp:ListItem Text="Snabbast" />
        <asp:ListItem Text="Träffsäkrast" />
        <asp:ListItem Text="Bäst Undvika" />
        <asp:ListItem Text="Mest Hälsa" />
        <asp:ListItem Text="Uthålligast" />
    </asp:DropDownList>
    <asp:ListView ID="LeaderboardListView" runat="server"
        ItemType="BellatorTabernae.Model.Leaderboard"
        SelectMethod="LeaderboardListView_GetLeaderboard"
        DataKeyNames="RowNumber">
        <LayoutTemplate>
            <ol id="Leaderboard">
                <h3>
                    <span class="LeaderboardRank">Rank</span>
                    <span class="LeaderboardName">Namn</span>
                    <span class="LeaderboardRace">Ras</span>
                    <span class="LeaderboardLevel">Level</span>
                </h3>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </ol>
            <div class="Pagination">
                <asp:DataPager runat="server" ID="DataPager" 
                    PageSize="30">
                    <Fields>
                        <asp:NextPreviousPagerField ShowFirstPageButton="true"
                            FirstPageText=" << "
                            ShowNextPageButton="false"
                            ShowPreviousPageButton ="false" />
                        <asp:NumericPagerField />
                        <asp:NextPreviousPagerField ShowLastPageButton="true"
                            LastPageText=" >> "
                            ShowNextPageButton="false"
                            ShowPreviousPageButton="false" />
                    </Fields>
                </asp:DataPager>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <li>
                <span class="LeaderboardRank"><%# Item.Rank %></span>
                <a href="<%#: GetRouteUrl("OtherCharacter", new {charID = Item.CharID}) %>">
                    <span class="LeaderboardName"><%# Item.Name %></span>
                </a>
                <span class="LeaderboardRace"><%# Item.Race %></span>
                <span class="LeaderboardLevel"><%# Item.Level %></span>
            </li>
        </ItemTemplate>
        <EmptyDataTemplate>
            <p>Det går inte att titta på topplistan just nu!</p>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>