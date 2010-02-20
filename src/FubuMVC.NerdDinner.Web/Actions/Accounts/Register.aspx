<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" Inherits="FubuDinner.Web.Actions.Accounts.Register" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Register
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create a New Account</h2>
    <p>
        Use the form below to create a new account. 
    </p>
    <p>
        Passwords are required to be a minimum of <%= Model.PasswordLength %> characters in length.
    </p>
    <!-- Html.ValidationSummary() -->

    <%= this.FormFor<RegisterForm>()%>
        <div>
            <fieldset>
                <legend>Account Information</legend>
                <p>
                    <label for="username">Username:</label>
                    <%= this.InputFor(m=>m.Username) %>
                </p>
                <p>
                    <label for="email">Email:</label>
                    <%= this.InputFor(m=>m.Email) %>
                </p>
                <p>
                    <label for="password">Password:</label>
                    <%= this.InputFor(m=>m.Password) %>
                </p>
                <p>
                    <label for="confirmPassword">Confirm password:</label>
                    <%= this.InputFor(m=>m.ConfirmPassword) %>
                </p>
                <p>
                    <input type="submit" value="Register" />
                </p>
            </fieldset>
        </div>
    <%= this.EndForm() %>
</asp:Content>
