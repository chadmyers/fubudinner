<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" Inherits="FubuDinner.Web.Actions.Accounts.LogOn" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Log On
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Log On</h2>
    <p>
        Please enter your username and password. <%= this.LinkTo<RegisterModel>().Text("Register") %> if you don't have an account.
    </p>
    <!-- Html.ValidationSummary() -->

    <%= this.FormFor<LogOnModelForm>() %>
        <%= this.InputFor(m=>m.ReturnUrl).Attr("type", "hidden") %>
        <div>
            <fieldset>
                <legend>Account Information</legend>
                <p>
                    <label for="username">Username:</label>
                    <%= this.InputFor(m=>m.Username) %>
                </p>
                <p>
                    <label for="password">Password:</label>
                    <%= this.InputFor(m=>m.Password) %>
                </p>
                <p>
                    <%= this.InputFor(m=>m.rememberMe) %>
                    <label class="inline" for="rememberMe">Remember me?</label>
                </p>
                <p>                    
                    <input type="submit" value="Log On" />
                </p>
            </fieldset>
        </div>
    <%= this.EndForm() %>
</asp:Content>