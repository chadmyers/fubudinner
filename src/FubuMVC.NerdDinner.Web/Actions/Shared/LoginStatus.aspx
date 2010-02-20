<%@ Page Language="C#" AutoEventWireup="true" Inherits="FubuDinner.Web.Actions.Shared.LoginStatus" %>

<% if (Model.IsAuthenticated) { %>
        Welcome <b><%= Server.HtmlEncode(Model.UserName) %></b>!
        [ <%= this.LinkTo<LogOffModel>().Text("Log Off") %> ]
<% } else { %> 
        [ <%= this.LinkTo(new LogOnModel{ReturnUrl = Model.RawUrl }).Text("Log On") %> ]
<% } %>