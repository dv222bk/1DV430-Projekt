<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Shared/Site.Master" CodeBehind="Character.aspx.cs" Inherits="BellatorTabernae.Pages.Character" EnableViewState="false" %>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <asp:Panel ID="CharacterPanel" runat="server" 
        Visible="False">
        <asp:Image ID="CharacterImage" runat="server" />
        <asp:Label ID="CharacterName" runat="server" />
        <asp:Panel ID="CharacterInfo" runat="server">
            <asp:Label ID="CharacterRace" runat="server" />
            <asp:Label ID="CharacterLevel" runat="server" />
            <asp:Label ID="CharacterExperience" runat="server" />
            <asp:Label ID="CharacterHealth" runat="server" />
            <asp:Label ID="CharacterStanima" runat="server" />
        </asp:Panel>
        <asp:Panel ID="CharacterStats" runat="server">
            <asp:Label ID="CharacterStrength" runat="server" />
            <asp:Label ID="CharacterSpeed" runat="server" />
            <asp:Label ID="CharacterAgility" runat="server" />
            <asp:Label ID="CharacterDexterity" runat="server" />
        </asp:Panel>
        <asp:Button ID="RemoveCharacter" runat="server"
             Text="Ta bort karaktär" />
        <!-- TO DO: Add Character Inventory -->
        <asp:Panel ID="CharacterBiografyPanel" runat="server">
            <h3>Biografi</h3>
            <asp:Label ID="CharacterBiografy" runat="server" />
            <asp:Button ID="EditCharacterBiografy" runat="server"
                 Text="Ändra" />
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="NewCharacterPanel" runat="server" Visible="False">
        <asp:ScriptManager EnablePartialRendering="true"
            ID="NewCharacterScriptManager" runat="server"></asp:ScriptManager>
        <h2>Ny karaktär</h2>
        <p>Eftersom du inte har en karaktär är det högtid att skapa en! Fyll i formuläret nedan!</p>
        <asp:RequiredFieldValidator ID="NameRequired" runat="server"
             ErrorMessage="Karaktären måste ha ett namn!"
             ControlToValidate="Name" 
             Display="None" />
        <asp:RangeValidator ID="NameRange" runat="server" 
            ErrorMessage="Karaktärens namn måste vara mellan 1 och 50 tecken långt"
            ControlToValidate="Name"
            MaximumValue="50" 
            MinimumValue="1" 
            Display="None" />
        <asp:Label ID="NameLabel" runat="server" 
            Text="Namn:" 
            AssociatedControlID="Name" />
        <asp:TextBox ID="Name" runat="server" 
            MaxLength="50" />
        <asp:Label ID="RaceLabel" runat="server" 
            Text="Race:" 
            AssociatedControlID="RaceList" />
        <asp:UpdatePanel ID="RaceAndStatsUpdate" runat="server">
            <ContentTemplate>
                <asp:DropDownList ID="RaceList" runat="server"
                    AutoPostBack="True"
                    OnSelectedIndexChanged="GetRaceEffect" />
                <asp:Label ID="RaceDesc" runat="server"
                    AssociatedControlID="RaceList" />
                <asp:Label ID="PointsLeft" runat="server" />
                <asp:Label ID="HealthLabel" runat="server" Text="Livspoäng: " AssociatedControlID="Health" />
                <asp:Button ID="HealthMinus" runat="server" 
                    Text="-" 
                    OnClick="HealthMinus_Click" />
                <asp:TextBox ID="Health" runat="server" 
                    ReadOnly="True" 
                    MaxLength="2" />
                <asp:Button ID="HealthPlus" runat="server" 
                    Text="+" 
                    OnClick="HealthPlus_Click" />
                <asp:Label ID="StanimaLabel" runat="server" Text="Uthållighetspoäng: " AssociatedControlID="Stanima" />
                <asp:Button ID="StanimaMinus" runat="server" 
                    Text="-" 
                    OnClick="StanimaMinus_Click" />
                <asp:TextBox ID="Stanima" runat="server" 
                    ReadOnly="True" 
                    MaxLength="2" />
                <asp:Button ID="StanimaPlus" runat="server" 
                    Text="+" OnClick="StanimaPlus_Click" />
                <asp:Label ID="StrengthLabel" runat="server" Text="Styrka: " AssociatedControlID="Strength" />
                <asp:Button ID="StrengthMinus" runat="server" 
                    Text="-" 
                    OnClick="StrengthMinus_Click" />
                <asp:TextBox ID="Strength" runat="server" 
                    ReadOnly="True" 
                    MaxLength="2" />
                <asp:Button ID="StrengthPlus" runat="server" 
                    Text="+" 
                    OnClick="StrengthPlus_Click" />
                <asp:Label ID="SpeedLabel" runat="server" Text="Snabbhet: " AssociatedControlID="Speed" />
                <asp:Button ID="SpeedMinus" runat="server" 
                    Text="-" 
                    OnClick="SpeedMinus_Click" />
                <asp:TextBox ID="Speed" runat="server" 
                    ReadOnly="True"
                    MaxLength="2" />
                <asp:Button ID="SpeedPlus" runat="server" 
                    Text="+" 
                    OnClick="SpeedPlus_Click" />
                <asp:Label ID="AgilityLabel" runat="server" Text="Träffsäkerhet: " AssociatedControlID="Agility" />
                <asp:Button ID="AgilityMinus" runat="server" 
                    Text="-" 
                    OnClick="AgilityMinus_Click" />
                <asp:TextBox ID="Agility" runat="server" 
                    ReadOnly="True"
                    MaxLength="2" />
                <asp:Button ID="AgilityPlus" runat="server" 
                    Text="+" OnClick="AgilityPlus_Click" />
                <asp:Label ID="DexterityLabel" runat="server" Text="Undvika: " AssociatedControlID="Dexterity" />
                <asp:Button ID="DexterityMinus" runat="server" 
                    Text="-" 
                    OnClick="DexterityMinus_Click" />
                <asp:TextBox ID="Dexterity" runat="server" 
                    ReadOnly="True"
                    MaxLength="2" />
                <asp:Button ID="DexterityPlus" runat="server" 
                    Text="+" 
                    OnClick="DexterityPlus_Click" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="RaceList" EventName="SelectedIndexChanged"/>
            </Triggers>
        </asp:UpdatePanel>
        <asp:Button ID="CreateCharacter" runat="server" 
            Text="Skapa karaktär"
            OnClick="CreateCharacter_Click" />
    </asp:Panel>
</asp:Content>