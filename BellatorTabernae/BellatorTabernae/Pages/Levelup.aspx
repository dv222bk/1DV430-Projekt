<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Shared/Site.Master" CodeBehind="Levelup.aspx.cs" Inherits="BellatorTabernae.Pages.Levelup" EnableViewState="false" %>

<asp:Content ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    Level upp! - Bellator Tabernae
</asp:Content>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h2>Level upp!</h2>
    <p>Grattis! Genom digert stridande har din karaktär blivit bättre!</p>
    <p>Vad har din karaktär lärt sig? Kom ihåg: Du behöver inte spendera alla poäng nu om du inte vill!</p>
    <asp:ScriptManager ID="CharacterLevelUpScriptManager" runat="server"
        EnablePartialRendering="true" />
    <asp:UpdatePanel ID="CharacterLevelUpUpdatePanel" runat="server">
        <ContentTemplate>
            <asp:Label ID="PointsLeft" runat="server" />
            <asp:RequiredFieldValidator ID="HealthRequired" runat="server"
                    ErrorMessage="Karaktären måste ha hälsa!"
                    ControlToValidate="Health" 
                    Display="None" />
            <asp:Label ID="HealthLabel" runat="server" Text="Livspoäng: " AssociatedControlID="Health" />
            <div class="statControls">
                <asp:Button ID="HealthMinus" runat="server" 
                    Text="-" 
                    OnClick="HealthMinus_Click" />
                <asp:TextBox ID="Health" runat="server" 
                    ReadOnly="True" 
                    MaxLength="5" />
                <asp:Button ID="HealthPlus" runat="server" 
                    Text="+" 
                    OnClick="HealthPlus_Click" />
            </div>
            <asp:RequiredFieldValidator ID="StanimaRequired" runat="server"
                    ErrorMessage="Karaktären måste ha uthållighet!"
                    ControlToValidate="Stanima" 
                    Display="None" />
            <asp:Label ID="StanimaLabel" runat="server" Text="Uthållighetspoäng: " AssociatedControlID="Stanima" />
            <div class="statControls">
                <asp:Button ID="StanimaMinus" runat="server" 
                    Text="-" 
                    OnClick="StanimaMinus_Click" />
                <asp:TextBox ID="Stanima" runat="server" 
                    ReadOnly="True" 
                    MaxLength="5" />
                <asp:Button ID="StanimaPlus" runat="server" 
                    Text="+" OnClick="StanimaPlus_Click" />
            </div>
            <asp:RequiredFieldValidator ID="StrengthRequired" runat="server"
                    ErrorMessage="Karaktären måste ha styrka!"
                    ControlToValidate="Strength" 
                    Display="None" />
            <asp:Label ID="StrengthLabel" runat="server" Text="Styrka: " AssociatedControlID="Strength" />
            <div class="statControls">
                <asp:Button ID="StrengthMinus" runat="server" 
                    Text="-" 
                    OnClick="StrengthMinus_Click" />
                <asp:TextBox ID="Strength" runat="server" 
                    ReadOnly="True" 
                    MaxLength="3" />
                <asp:Button ID="StrengthPlus" runat="server" 
                    Text="+" 
                    OnClick="StrengthPlus_Click" />
            </div>
            <asp:RequiredFieldValidator ID="SpeedRequired" runat="server"
                    ErrorMessage="Karaktären måste ha snabbhet!"
                    ControlToValidate="Speed" 
                    Display="None" />
            <asp:Label ID="SpeedLabel" runat="server" Text="Snabbhet: " AssociatedControlID="Speed" />
            <div class="statControls">
                <asp:Button ID="SpeedMinus" runat="server" 
                    Text="-" 
                    OnClick="SpeedMinus_Click" />
                <asp:TextBox ID="Speed" runat="server" 
                    ReadOnly="True"
                    MaxLength="3" />
                <asp:Button ID="SpeedPlus" runat="server" 
                    Text="+" 
                    OnClick="SpeedPlus_Click" />
            </div>
            <asp:RequiredFieldValidator ID="AgilityRequired" runat="server"
                    ErrorMessage="Karaktären måste ha träffsäkerhet!"
                    ControlToValidate="Agility" 
                    Display="None" />
            <asp:Label ID="AgilityLabel" runat="server" Text="Träffsäkerhet: " AssociatedControlID="Agility" />
            <div class="statControls">
                <asp:Button ID="AgilityMinus" runat="server" 
                    Text="-" 
                    OnClick="AgilityMinus_Click" />
                <asp:TextBox ID="Agility" runat="server" 
                    ReadOnly="True"
                    MaxLength="3" />
                <asp:Button ID="AgilityPlus" runat="server" 
                    Text="+" OnClick="AgilityPlus_Click" />
            </div>
            <asp:RequiredFieldValidator ID="DexterityRequired" runat="server"
                    ErrorMessage="Karaktären måste ha undvika!"
                    ControlToValidate="Dexterity" 
                    Display="None" />
            <asp:Label ID="DexterityLabel" runat="server" Text="Undvika: " AssociatedControlID="Dexterity" />
            <div class="statControls">
                <asp:Button ID="DexterityMinus" runat="server" 
                    Text="-" 
                    OnClick="DexterityMinus_Click" />
                <asp:TextBox ID="Dexterity" runat="server" 
                    ReadOnly="True"
                    MaxLength="3" />
                <asp:Button ID="DexterityPlus" runat="server" 
                    Text="+" 
                    OnClick="DexterityPlus_Click" />
            </div>
        </ContentTemplate>
        <Triggers>
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
    <asp:Button ID="SpendPoints" runat="server" 
            Text="Spendera poäng"
            OnClick="LevelUp_Click" />
</asp:Content>