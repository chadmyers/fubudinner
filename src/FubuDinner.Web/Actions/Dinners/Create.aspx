<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" Inherits="FubuDinner.Web.Actions.Dinners.Create" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Host a Nerd Dinner
</asp:Content>

<asp:Content ID="Create" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Host a Dinner</h2>

    <% this.Partial(new DinnerFormModel{Dinner = Model.Dinner}); %>

</asp:Content>