<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Shared/Site.Master" CodeBehind="Default.aspx.cs" Inherits="BellatorTabernae.Pages.Default" EnableViewState="False" %>

<asp:Content ContentPlaceHolderID="TitlePlaceHolder" runat="server">
    Bellator Tabernae
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Scripts/frontpage.js") %>'></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <asp:Panel ID="FrontPageMainPanel" runat="server" DefaultButton="NewUser">
        <asp:Panel ID="NewUserPanel" runat="server">
            <asp:Label ID="NewUserLabel" runat="server" 
                Text="Ny användare?" 
                AssociatedControlID="NewUser" />
            <asp:Button ID="NewUser" runat="server" 
                Text="Börja spela!" 
                OnClick="NewUser_Click"
                CausesValidation="False" />
        </asp:Panel>
        <asp:Panel ID="LoginPanel" runat="server" DefaultButton="Login">
            <asp:RequiredFieldValidator ID="UsernameRequired" runat="server" 
                ErrorMessage="Du har glömt ange användarnamn!"
                ControlToValidate="Username"
                Display="None" />
            <asp:Label ID="UsernameLabel" runat="server" 
                Text="Användarnamn" 
                AssociatedControlID="Username" />
            <asp:TextBox ID="Username" runat="server" 
                MaxLength="50" />
            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                ErrorMessage="Du har glömt ange lösenord!" 
                ControlToValidate="Password" 
                Display="None" />
            <asp:Label ID="PasswordLabel" runat="server" 
                Text="Lösenord" 
                AssociatedControlID="Password" />
            <asp:TextBox ID="Password" runat="server" 
                TextMode="Password" />
            <asp:Button ID="Login" runat="server" 
                Text="Logga in" 
                OnClick="Login_Click" />
        </asp:Panel>
    </asp:Panel>
    <h2>Välkommen till tavernan!</h2>
    <p>
        Kom in och sätt dig vet ja, här finns plats för alla! Vem är du, vad är din historia? 
        Till Bellator Tabernae, strids tavernornas stad, kommer alla möjliga varelser. Vissa
        försöker bli ämnet på allas läppar medan andra letar stora rikedommar. Det finns till
        och med de som bara strider för egen underhållning. Varför är just du här? Oavsett 
        varför du är här, så är du ju här nu!
    </p>
    <h3>Ditt äventyr börjar här</h3>
    <p>
        Bellator Tabernae är ett textbaserat webbläsarspel som fungerar lika bra på din
        desktop som på din mobil. Skapa en karaktär och strid din väg till toppen av
        topplistan. På vägen möter du många fiender och vinner guld som du kan köpa ny
        och bättre utrustning för. Vad väntar du på? Ditt äventyr börjar här och nu!
    </p>
</asp:Content>
