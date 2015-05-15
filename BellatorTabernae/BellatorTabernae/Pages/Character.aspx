<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Shared/Site.Master" CodeBehind="Character.aspx.cs" Inherits="BellatorTabernae.Pages.Character" EnableViewState="true" %>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h2>Karaktärssida</h2>
    <asp:Panel ID="CharacterPanel" runat="server" 
        Visible="False">
        <asp:Image ID="CharacterImage" runat="server" />
        <asp:Literal ID="CharacterName" runat="server" />
        <asp:Panel ID="CharacterInfo" runat="server">
            <asp:Literal ID="CharacterRace" runat="server" />
            <asp:Literal ID="CharacterLevel" runat="server" />
            <asp:Literal ID="CharacterExperience" runat="server" />
            <asp:Literal ID="CharacterHealth" runat="server" />
            <asp:Literal ID="CharacterStanima" runat="server" />
        </asp:Panel>
        <asp:Panel ID="CharacterStats" runat="server">
            <asp:Literal ID="CharacterStrength" runat="server" />
            <asp:Literal ID="CharacterSpeed" runat="server" />
            <asp:Literal ID="CharacterAgility" runat="server" />
            <asp:Literal ID="CharacterDexterity" runat="server" />
        </asp:Panel>
        <asp:Button ID="RemoveCharacter" runat="server"
            Text="Ta bort karaktär" 
            OnClick="RemoveCharacter_Click" />
        <!-- TO DO: Add Character Inventory -->
        <asp:Panel ID="CharacterBiografyPanel" runat="server">
            <h3>Biografi</h3>
            <asp:Literal ID="CharacterBiografyLiteral" runat="server" />
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
    <asp:Panel ID="NewCharacterPanel" runat="server" Visible="False">
        <asp:ScriptManager EnablePartialRendering="true"
            ID="NewCharacterScriptManager" runat="server"></asp:ScriptManager>
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
                    EnableViewState="True" />
                <asp:Label ID="RaceDesc" runat="server"
                    AssociatedControlID="RaceList" />
                <asp:Label ID="PointsLeft" runat="server" />
                <asp:RequiredFieldValidator ID="HealthRequired" runat="server"
                     ErrorMessage="Karaktären måste ha hälsa!"
                     ControlToValidate="Health" 
                     Display="None" />
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
                <asp:RequiredFieldValidator ID="StanimaRequired" runat="server"
                     ErrorMessage="Karaktären måste ha uthållighet!"
                     ControlToValidate="Stanima" 
                     Display="None" />
                <asp:Label ID="StanimaLabel" runat="server" Text="Uthållighetspoäng: " AssociatedControlID="Stanima" />
                <asp:Button ID="StanimaMinus" runat="server" 
                    Text="-" 
                    OnClick="StanimaMinus_Click" />
                <asp:TextBox ID="Stanima" runat="server" 
                    ReadOnly="True" 
                    MaxLength="2" />
                <asp:Button ID="StanimaPlus" runat="server" 
                    Text="+" OnClick="StanimaPlus_Click" />
                <asp:RequiredFieldValidator ID="StrengthRequired" runat="server"
                     ErrorMessage="Karaktären måste ha styrka!"
                     ControlToValidate="Strength" 
                     Display="None" />
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
                <asp:RequiredFieldValidator ID="SpeedRequired" runat="server"
                     ErrorMessage="Karaktären måste ha snabbhet!"
                     ControlToValidate="Speed" 
                     Display="None" />
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
                <asp:RequiredFieldValidator ID="AgilityRequired" runat="server"
                     ErrorMessage="Karaktären måste ha träffsäkerhet!"
                     ControlToValidate="Agility" 
                     Display="None" />
                <asp:Label ID="AgilityLabel" runat="server" Text="Träffsäkerhet: " AssociatedControlID="Agility" />
                <asp:Button ID="AgilityMinus" runat="server" 
                    Text="-" 
                    OnClick="AgilityMinus_Click" />
                <asp:TextBox ID="Agility" runat="server" 
                    ReadOnly="True"
                    MaxLength="2" />
                <asp:Button ID="AgilityPlus" runat="server" 
                    Text="+" OnClick="AgilityPlus_Click" />
                <asp:RequiredFieldValidator ID="DexterityRequired" runat="server"
                     ErrorMessage="Karaktären måste ha undvika!"
                     ControlToValidate="Dexterity" 
                     Display="None" />
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
</asp:Content>