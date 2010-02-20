<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" Inherits="FubuDinner.Web.Actions.Accounts.ChangePassword" %>

<asp:Content ID="title" ContentPlaceHolderID="TitleContent" runat="server">
    Change Password
</asp:Content>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Change Password</h2>
    <p>
        Use the form below to change your password. 
    </p>
    <p>
        New passwords are required to be a minimum of <%= Model.PasswordLength %> characters in length.
    </p>
    <!-- Html.ValidationSummary() -->

    <%= this.FormFor<ChangePasswordForm>() %>
        <div>
            <fieldset>
                <legend>Account Information</legend>
                <p>
                    <label for="currentPassword">Current password:</label>
                    <%= this.InputFor(m=>m.CurrentPassword) %>
                </p>
                <p>
                    <label for="newPassword">New password:</label>
                    <%= this.InputFor(m=>m.NewPassword) %>
                </p>
                <p>
                    <label for="confirmPassword">Confirm new password:</label>
                    <%= this.InputFor(m=>m.ConfirmPassword) %>
                </p>
                <p>
                    <input type="submit" value="Change Password" />
                </p>
            </fieldset>
        </div>
    <%= this.EndForm() %>
</asp:Content>
