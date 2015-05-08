<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Shared/Site.Master" CodeBehind="Default.aspx.cs" Inherits="BellatorTabernae.Pages.Default" EnableViewState="False" %>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <asp:Panel ID="MsgPanel" runat="server" 
        Visible="False">
        <asp:Label ID="SiteMsg" runat="server" />
    </asp:Panel>
    <asp:Panel ID="FrontPageMainPanel" runat="server">
        <asp:Panel ID="NewUserPanel" runat="server">
            <asp:Label ID="NewUserLabel" runat="server" 
                Text="Ny användare?" 
                AssociatedControlID="NewUser" />
            <asp:Button ID="NewUser" runat="server" 
                Text="Börja spela!" 
                OnClick="NewUser_Click"
                CausesValidation="False" />
        </asp:Panel>
        <asp:Panel ID="LoginPanel" runat="server">
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
    <h2>Lorem ipsum</h2>
    <p>Sit amet, consectetuer adipiscing ehit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat</p>
    <h3>Ut wisi enim ad</h3>
    <p>Minim venium quis nostrud exerci tation ullamcorper suscipit lobortis.</p>
</asp:Content>
