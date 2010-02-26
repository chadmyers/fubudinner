<%@ Page Language="C#" AutoEventWireup="true" Inherits="FubuDinner.Web.Actions.Shared.Masthead" %>

		<div id="hm-masthead">
		<%if (Model.ShowSearch) { //Show Search Box %>
	    <div id="searchBox">
        <div class="search-text">Enter your location  or <strong><%= this.LinkTo<ViewDinners>().Text("View All Upcoming Dinners")%></strong>.</div>
				<input id="Location" name="Location" type="text" />
				<input id="search" type="submit" value="Search" />
	    </div>
	    <% } %>
		</div>