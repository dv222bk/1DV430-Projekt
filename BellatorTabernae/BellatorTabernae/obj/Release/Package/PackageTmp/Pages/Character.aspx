<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Shared/Site.Master" CodeBehind="Character.aspx.cs" Inherits="BellatorTabernae.Pages.Character" EnableViewState="true" %>

<asp:Content ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    Karaktärssidan - Bellator Tabernae
</asp:Content>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h2>Karaktärssidan</h2>
    <asp:Panel ID="CharacterPanel" runat="server" 
        Visible="False">
        <div id="Character">
            <asp:Image ID="CharacterImage" runat="server" />
            <span class="CharacterName"><asp:Literal ID="CharacterName" runat="server" /></span>
            <asp:Panel ID="CharacterInfo" runat="server">
                <span class="CharacterRace"><asp:Literal ID="CharacterRace" runat="server" /></span>
                <span class="CharacterLevel"><asp:Literal ID="CharacterLevel" runat="server" /></span>
                <span class="CharacterExperience"><asp:Literal ID="CharacterExperience" runat="server" /></span>
            </asp:Panel>
            <asp:Panel ID="CharacterVitals" runat="server">
                <span class="CharacterHealth"><asp:Literal ID="CharacterHealth" runat="server" /></span>
                <span class="CharacterStanima"><asp:Literal ID="CharacterStanima" runat="server" /></span>
            </asp:Panel>
            <asp:Panel ID="CharacterStats" runat="server">
                <span class="CharacterStrength"><asp:Literal ID="CharacterStrength" runat="server" /></span>
                <span class="CharacterSpeed"><asp:Literal ID="CharacterSpeed" runat="server" /></span>
                <span class="CharacterAgility"><asp:Literal ID="CharacterAgility" runat="server" /></span>
                <span class="CharacterDexterity"><asp:Literal ID="CharacterDexterity" runat="server" /></span>
                <asp:Button ID="LevelUp" runat="server"
                    Text="Level Upp!"
                    Visible="false"
                    OnClick="LevelUp_Click" />
            </asp:Panel>
            <asp:Button ID="RemoveCharacter" runat="server"
                Text="Ta bort karaktär" 
                OnClick="RemoveCharacter_Click" />
            <asp:Panel ID="RemoveCharacterPanel" runat="server"
                Visible="false">
                <strong>Är du verkligen säker på att du vill ta bort din karaktär?</strong>
                <asp:Button ID="ConfirmRemoval" runat="server"
                    Text="Ta bort karaktär"
                    OnClick="ConfirmRemoval_Click" />
                <asp:Button ID="CancelRemoval" runat="server"
                    Text="Avbryt"
                    OnClick="CancelRemoval_Click" />
            </asp:Panel>
        </div>
        <asp:Panel ID="CharacterInventory" runat="server">
            <h3>Ägodelar</h3>
            <span class="CharacterGold"><asp:Literal ID="CharacterGoldLiteral" runat="server" /></span>
            <asp:ListView ID="CharacterInventoryListView" runat="server"
                ItemType="BellatorTabernae.Model.Inventory"
                SelectMethod="CharacterInventoryListView_GetEquipments"
                UpdateMethod="CharacterInventoryListView_EquipItem"
                DataKeyNames="InventoryID">
                <LayoutTemplate>
                    <ul id="Inventory">
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <li class="InventoryItem">
                        <span class="EquipmentName"><%# Item.Name %></span>
                        <span class="EquipmentUse">
                            <asp:Button ID="Equip" runat="server" 
                                CommandName="Update"
                                Text="Ta på"
                                Visible="<%# !IsEquipped(Item.InventoryID) %>" />
                            <asp:Button ID="Unequip" runat="server" 
                                CommandName="Update"
                                Text="Ta av"
                                Visible="<%# IsEquipped(Item.InventoryID) %>" />
                        </span>
                        <ul>
                            <li>
                                <span class="EquipmentValue">Typ: </span><span class="EquipmentType"><%# Item.EquipType %></span>
                                <span class="EquipmentValue">Effekt: </span><span class="EquipmentEffect"><%# GetEquipEffects(Item.EquipStatsID) %></span>
                                <span class="EquipmentValue">Antal: </span><span class="EquipmentNumber"><%# Item.Number %></span>
                            </li>
                        </ul>
                    </li>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <p>Du har inga ägodelar!</p>
                </EmptyDataTemplate>
            </asp:ListView>
        </asp:Panel>
        <asp:Panel ID="CharacterBiografyPanel" runat="server">
            <h3>Biografi</h3>
            <p><asp:Literal ID="CharacterBiografyLiteral" runat="server" /></p>
            <asp:Button ID="EditCharacterBiografyButton" runat="server"
                 Text="Ändra" OnClick="EditCharacterBiografyButton_Click" />
            <asp:TextBox ID="EditCharacterBiografy" runat="server"
                TextMode="MultiLine"
                Visible="false" />
            <asp:Button ID="SubmitBiografy" runat="server"
                Text="Skicka" OnClick="SubmitBiografy_Click"
                Visible="false" />
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="NewCharacterPanel" runat="server" 
        Visible="False">
        <asp:ScriptManager ID="NewCharacterScriptManager" runat="server"
            EnablePartialRendering="true" />
        <h2>Ny karaktär</h2>
        <p>Eftersom du inte har en karaktär är det högtid att skapa en! Fyll i formuläret nedan!</p>
        <asp:RequiredFieldValidator ID="NameRequired" runat="server"
             ErrorMessage="Karaktären måste ha ett namn!"
             ControlToValidate="Name" 
             Display="None" />
        <asp:Label ID="NameLabel" runat="server" 
            Text="Namn:" 
            AssociatedControlID="Name" />
        <asp:TextBox ID="Name" runat="server" 
            MaxLength="50" />
        <asp:RequiredFieldValidator ID="RaceRequired" runat="server"
            ErrorMessage="Karaktären måste ha en ras!"
            ControlToValidate="RaceList" 
            Display="None" />
        <asp:Label ID="RaceLabel" runat="server" 
            Text="Ras:" 
            AssociatedControlID="RaceList" />
        <asp:UpdatePanel ID="RaceAndStatsUpdate" runat="server">
            <ContentTemplate>
                <asp:DropDownList ID="RaceList" runat="server"
                    AutoPostBack="True"
                    OnSelectedIndexChanged="ChangeRace" 
                    ViewStateMode="Enabled" 
                    EnableViewState="True"
                    CausesValidation="False" />
                <p><asp:Literal ID="RaceDesc" runat="server" /></p>
                <asp:Label ID="PointsLeft" runat="server" />
                <asp:RequiredFieldValidator ID="HealthRequired" runat="server"
                     ErrorMessage="Karaktären måste ha hälsa!"
                     ControlToValidate="Health" 
                     Display="None" />
                <asp:Label ID="HealthLabel" runat="server" Text="Livspoäng: " AssociatedControlID="Health" />
                <div class="statControls">
                    <asp:Button ID="HealthMinus" runat="server" 
                        Text="-" 
                        OnClick="HealthMinus_Click"
                        CausesValidation="False" />
                    <asp:TextBox ID="Health" runat="server" 
                        ReadOnly="True" 
                        MaxLength="2" />
                    <asp:Button ID="HealthPlus" runat="server" 
                        Text="+" 
                        OnClick="HealthPlus_Click"
                        CausesValidation="False" />
                </div>
                <asp:RequiredFieldValidator ID="StanimaRequired" runat="server"
                     ErrorMessage="Karaktären måste ha uthållighet!"
                     ControlToValidate="Stanima" 
                     Display="None" />
                <asp:Label ID="StanimaLabel" runat="server" Text="Uthållighetspoäng: " AssociatedControlID="Stanima" />
                <div class="statControls">
                    <asp:Button ID="StanimaMinus" runat="server" 
                        Text="-" 
                        OnClick="StanimaMinus_Click"
                        CausesValidation="False" />
                    <asp:TextBox ID="Stanima" runat="server" 
                        ReadOnly="True" 
                        MaxLength="2" />
                    <asp:Button ID="StanimaPlus" runat="server" 
                        Text="+" 
                        OnClick="StanimaPlus_Click"
                        CausesValidation="False" />
                </div>
                <asp:RequiredFieldValidator ID="StrengthRequired" runat="server"
                     ErrorMessage="Karaktären måste ha styrka!"
                     ControlToValidate="Strength" 
                     Display="None" />
                <asp:Label ID="StrengthLabel" runat="server" Text="Styrka: " AssociatedControlID="Strength" />
                <div class="statControls">
                    <asp:Button ID="StrengthMinus" runat="server" 
                        Text="-" 
                        OnClick="StrengthMinus_Click"
                        CausesValidation="False" />
                    <asp:TextBox ID="Strength" runat="server" 
                        ReadOnly="True" 
                        MaxLength="2" />
                    <asp:Button ID="StrengthPlus" runat="server" 
                        Text="+" 
                        OnClick="StrengthPlus_Click"
                        CausesValidation="False" />
                </div>
                <asp:RequiredFieldValidator ID="SpeedRequired" runat="server"
                     ErrorMessage="Karaktären måste ha snabbhet!"
                     ControlToValidate="Speed" 
                     Display="None" />
                <asp:Label ID="SpeedLabel" runat="server" Text="Snabbhet: " AssociatedControlID="Speed" />
                <div class="statControls">
                    <asp:Button ID="SpeedMinus" runat="server" 
                        Text="-" 
                        OnClick="SpeedMinus_Click"
                        CausesValidation="False" />
                    <asp:TextBox ID="Speed" runat="server" 
                        ReadOnly="True"
                        MaxLength="2" />
                    <asp:Button ID="SpeedPlus" runat="server" 
                        Text="+" 
                        OnClick="SpeedPlus_Click"
                        CausesValidation="False" />
                </div>
                <asp:RequiredFieldValidator ID="AgilityRequired" runat="server"
                     ErrorMessage="Karaktären måste ha träffsäkerhet!"
                     ControlToValidate="Agility" 
                     Display="None" />
                <asp:Label ID="AgilityLabel" runat="server" Text="Träffsäkerhet: " AssociatedControlID="Agility" />
                <div class="statControls">
                    <asp:Button ID="AgilityMinus" runat="server" 
                        Text="-" 
                        OnClick="AgilityMinus_Click"
                        CausesValidation="False" />
                    <asp:TextBox ID="Agility" runat="server" 
                        ReadOnly="True"
                        MaxLength="2" />
                    <asp:Button ID="AgilityPlus" runat="server" 
                        Text="+" 
                        OnClick="AgilityPlus_Click"
                        CausesValidation="False" />
                </div>
                <asp:RequiredFieldValidator ID="DexterityRequired" runat="server"
                     ErrorMessage="Karaktären måste ha undvika!"
                     ControlToValidate="Dexterity" 
                     Display="None" />
                <asp:Label ID="DexterityLabel" runat="server" Text="Undvika: " AssociatedControlID="Dexterity" />
                <div class="statControls">
                    <asp:Button ID="DexterityMinus" runat="server" 
                        Text="-" 
                        OnClick="DexterityMinus_Click"
                        CausesValidation="False" />
                    <asp:TextBox ID="Dexterity" runat="server" 
                        ReadOnly="True"
                        MaxLength="2" />
                    <asp:Button ID="DexterityPlus" runat="server" 
                        Text="+" 
                        OnClick="DexterityPlus_Click"
                        CausesValidation="False" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="RaceList" />
                <asp:AsyncPostBackTrigger ControlID="DexterityPlus" />
                <asp:AsyncPostBackTrigger ControlID="DexterityMinus" />
                <asp:AsyncPostBackTrigger ControlID="AgilityPlus" />
                <asp:AsyncPostBackTrigger ControlID="AgilityMinus" />
                <asp:AsyncPostBackTrigger ControlID="SpeedPlus" />
                <asp:AsyncPostBackTrigger ControlID="SpeedMinus" />
                <asp:AsyncPostBackTrigger ControlID="StrengthPlus" />
                <asp:AsyncPostBackTrigger ControlID="StrengthMinus" />
                <asp:AsyncPostBackTrigger ControlID="StanimaPlus" />
                <asp:AsyncPostBackTrigger ControlID="StanimaMinus" />
                <asp:AsyncPostBackTrigger ControlID="HealthPlus" />
                <asp:AsyncPostBackTrigger ControlID="HealthMinus" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Button ID="CreateCharacter" runat="server" 
            Text="Skapa karaktär"
            OnClick="CreateCharacter_Click" />
    </asp:Panel>
    <asp:Panel ID="CharacterDeadPanel" runat="server"
        Visible="false">
        <p>Tyvärr så har din karaktär dött i strid! Karaktären kommer alltid minnas som den hjälte den var!</p>
        <asp:Button ID="RemoveDeadCharacter" runat="server"
            Text="Skapa ny karaktär" 
            OnClick="RemoveCharacter_Click" />
    </asp:Panel>
</asp:Content>