<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Shared/Site.Master" CodeBehind="Battle.aspx.cs" Inherits="BellatorTabernae.Pages.Battle" EnableViewState="false" %>

<asp:Content ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    Strid - Bellator Tabernae
</asp:Content>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h2>Strid</h2>
    <asp:Panel ID="MonsterPanel" runat="server"
        Visible="False">
        <asp:ListView ID="MonstersListView" runat="server"
            ItemType="BellatorTabernae.Model.Character"
            SelectMethod="MonstersListView_GetMonsters"
            UpdateMethod="MonstersListView_ChallangeMonster"
            DataKeyNames="CharID">
            <LayoutTemplate>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </LayoutTemplate>
            <ItemTemplate>
                <div class="Monster">
                    <span class="MonsterName"><%#: Item.Name %></span>
                    <span class="MonsterLevel">Level: <%#: Item.Level %></span>
                    <span class="MonsterScenarioTitle">Scenario:</span>
                    <span class="MonsterScenario"><%# Item.Biografy %></span>
                    <asp:Button ID="ChallangeMonster" runat="server" 
                        CommandName="Update"
                        Text="Utmana!" />
                </div>
            </ItemTemplate>
            <EmptyDataTemplate>
                <p>Det finns inga monster att slåss mot just nu!</p>
            </EmptyDataTemplate>
        </asp:ListView>
    </asp:Panel>
    <asp:Panel ID="NoCharacterPanel" runat="server" 
        Visible="False">
        <p>Utan karaktär går det inte att strida! Gå till <asp:HyperLink ID="CharacterHyperLink" runat="server" NavigateUrl="<%$RouteUrl:RouteName=Character %>">karaktärssidan</asp:HyperLink> för att skapa en karaktär!</p>
    </asp:Panel>
</asp:Content>