<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Shared/Site.Master" CodeBehind="Market.aspx.cs" Inherits="BellatorTabernae.Pages.Market" EnableViewState="false" %>

<asp:Content ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    Torget - Bellator Tabernae
</asp:Content>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h2>Torget</h2>
    <p>Välkommen till torget! Det är här karaktärerna i Bellator Tabernae byter till sig ny utrustning för sina välförtjänta guldmynt!</p>
    <span class="CharacterGold"><asp:Literal ID="CharacterGoldLiteral" runat="server" /></span>
    <asp:Panel ID="MarketPanel" runat="server">
        <asp:ListView ID="MarketListView" runat="server"
            ItemType="BellatorTabernae.Model.Equipment"
            SelectMethod="MarketListView_GetEquipments"
            UpdateMethod="MarketListView_BuyEquipment"
            DataKeyNames="EquipID">
            <LayoutTemplate>
                <ul id="Inventory">
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                </ul>
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
                <li class="InventoryItem">
                    <span class="EquipmentName"><%# Item.Name %></span>
                    <asp:Button ID="BuyEquipment" runat="server" 
                        CommandName="Update"
                        Text="Köp" />
                    <ul>
                        <li>
                            <span class="EquipmentValue">Typ: </span><span class="EquipmentType"><%# Item.EquipType %></span>
                            <span class="EquipmentValue">Effekt: </span><span class="EquipmentEffect"><%# GetEquipEffects(Item.EquipStatsID) %></span>
                            <span class="EquipmentValue">Kostand: </span><span class="EquipmentCost"><%# Item.Value %> Guld</span>
                        </li>
                    </ul>
                </li>
            </ItemTemplate>
            <EmptyDataTemplate>
                <p>Torget har stängt just nu!</p>
            </EmptyDataTemplate>
        </asp:ListView>
    </asp:Panel>
    <asp:Panel ID="NoCharacterPanel" runat="server"
        Visible="false">
        <p>Utan karaktär går det inte att handla på torget! Gå till <asp:HyperLink ID="CharacterHyperLink" runat="server" NavigateUrl="<%$RouteUrl:RouteName=Character %>">karaktärssidan</asp:HyperLink> för att skapa en karaktär!</p>
    </asp:Panel>
</asp:Content>