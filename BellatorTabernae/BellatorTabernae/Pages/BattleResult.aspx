<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Shared/Site.Master" CodeBehind="BattleResult.aspx.cs" Inherits="BellatorTabernae.Pages.BattleResult" EnableViewState="false"%>

<asp:Content ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    Stridsrapport - Bellator Tabernae
</asp:Content>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h2>Stridsrapport</h2>
    <asp:Panel ID="CombatLogPanel" runat="server"
        Visible="False">
        <asp:ListView ID="CombatantsListView" runat="server"
            ItemType="BellatorTabernae.Model.Combatant"
            SelectMethod="CombatantsListView_ShowCombatants"
            DataKeyNames="CombatantID">
            <LayoutTemplate>
                <div class="CombatantTeams">
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <div class="Team">
                    <span class="TeamNumber">Lag <%# Item.TeamNumber %>:</span>
                    <span class="TeamMember">
                        <%# Item.Name %>
                        <span class="CombatantLevel">(<%# Item.Level %>)</span>
                    </span>
                </div>
            </ItemTemplate>
        </asp:ListView>
        <asp:ListView ID="CombatLogListView" runat="server"
            ItemType="BellatorTabernae.Model.CombatLog"
            SelectMethod="CombatLogListView_ShowCombatLog"
            DataKeyNames="CombatLogID">
            <LayoutTemplate>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </LayoutTemplate>
            <ItemTemplate>
                <div class="logEntry">
                    <asp:Panel ID="Panel1" runat="server" Visible="<%# Item.Attacker != null ? false : true %>">
                        <%# Item.Text %>
                    </asp:Panel>
                    <asp:Panel ID="combatLogEntryPanel" runat="server" Visible="<%# Item.Attacker != null ? true : false %>">
                        <div class="combatLogEntry">
                            <%# Item.Text %>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="combatLogEntryInfoPanel" runat="server" Visible="<%# Item.Attacker != null ? true : false %>">
                        <div class="infoPanel">
                            <div class="infoPanelDamage">
                                <span class="attacker"><%# Item.Attacker %></span>
                                <span class="damageTaken">Skada tagen: <%# Item.AttackerDamage %></span>
                            </div>
                            <div class="infoPanelDamage">
                                <span class="defender"><%# Item.Defender %></span>
                                <span class="damageTaken">Skada tagen: <%# Item.DefenderDamage %></span>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </asp:Panel>
    <asp:Panel ID="NoCombatLogPanel" runat="server" 
        Visible="False">
        <p>Du har inte stridit nyligen! Gå till <asp:HyperLink ID="BattleHyperLink" runat="server" NavigateUrl="<%$RouteUrl:RouteName=Battle %>">stridssisdan</asp:HyperLink> för att strida!</p>
    </asp:Panel>
</asp:Content>