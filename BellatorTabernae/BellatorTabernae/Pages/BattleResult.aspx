<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Shared/Site.Master" CodeBehind="BattleResult.aspx.cs" Inherits="BellatorTabernae.Pages.BattleResult" EnableViewState="false"%>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <asp:Panel ID="CombatLogPanel" runat="server"
        Visible="False">
        <asp:ListView ID="CombatLogListView" runat="server"
            ItemType="BellatorTabernae.Model.CombatLog"
            SelectMethod="CombatLogListView_ShowCombatLog"
            DataKeyNames="CombatLogID">
            <LayoutTemplate>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </LayoutTemplate>
            <ItemTemplate>
                <div class="combatLogEntry">
                    <%# Item.Text %>
                </div>
                <asp:Panel ID="combatLogEntryInfoPanel" runat="server" Visible="<%# Item.Attacker != null ? true : false %>">
                    <span class="attacker"><%# Item.Attacker %></span>
                    <span class="damageTaken">Skada tagen: <%# Item.AttackerDamage %></span>
                    <span class="defender"><%# Item.Defender %></span>
                    <span class="damageTaken">Skada tagen: <%# Item.DefenderDamage %></span>
                </asp:Panel>
            </ItemTemplate>
        </asp:ListView>
    </asp:Panel>
    <asp:Panel ID="NoCombatLogPanel" runat="server" 
        Visible="False">
        <p>Du har inte stridit nyligen! Gå till <asp:HyperLink ID="BattleHyperLink" runat="server" NavigateUrl="<%$RouteUrl:RouteName=Battle %>">stridssisdan</asp:HyperLink> för att strida!</p>
    </asp:Panel>
</asp:Content>