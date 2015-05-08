<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Shared/Site.Master" CodeBehind="CreateUser.aspx.cs" Inherits="BellatorTabernae.Pages.CreateUser" %>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h2>Ny användare</h2>
    <asp:RequiredFieldValidator ID="UsernameRequired" runat="server" 
        ErrorMessage="Du har glömt ange användarnamn!"
        ControlToValidate="Username"
        Display="None" />
    <asp:RangeValidator ID="UserNameRangeValidator" runat="server"
         ErrorMessage="Användarnamnet måste bestå av mellan 1 och 50 tecken!" 
        MinimumValue="1" 
        MaximumValue="50" />
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
    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" 
        ErrorMessage="Du har glömt ange din epost!" 
        ControlToValidate="Email" 
        Display="None" />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ErrorMessage="Din epost verkar inte vara korrekt!"
        ControlToValidate="Email"
        ValidationExpression=".+@.+.\..+"
        Display="None" />
    <asp:Label ID="EmailLabel" runat="server" 
        Text="Epost" 
        AssociatedControlID="Email" />
    <asp:TextBox ID="Email" runat="server"
        MaxLength="254" />
    <asp:Button ID="Create_User" runat="server" 
        Text="Skapa Användare" 
        OnClick="Create_User_Click" />
</asp:Content>