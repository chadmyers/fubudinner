<%@ Page Language="C#" AutoEventWireup="true" Inherits="FubuDinner.Web.Actions.Dinners.DinnerForm" %>

<!-- Html.ValidationSummary("Please correct the errors and try again.") -->

<%= this.FormFor(new CreateDinnerForm()) %>
    <fieldset>

        <div id="dinnerDiv">

        <p>
            <label for="Title">Dinner Title:</label>
            <%= this.InputFor(m=>m.Dinner.Title) %>
        </p>
        <p>
            <label for="EventDate">Event Date:</label>
            <%= this.InputFor(m=>m.Dinner.EventDate) %>
        </p>
        <p>
            <label for="Description">Description:</label>
            <%= this.InputFor(m => m.Dinner.Description)%>
        </p>
        <p>
            <label for="Address">Address:</label>
            <%= this.InputFor(m => m.Dinner.Address)%>
        </p>
        <p>
            <label for="Country">Country:</label>
            TODO
            <!-- Html.DropDownList("Country", Model.Countries) -->
            
        </p>
        <p>
            <label for="ContactPhone">Contact Info:</label>
            <%= this.InputFor(m => m.Dinner.ContactPhone)%>
        </p>
        <p>
            <%= this.InputFor(m => m.Dinner.Latitude) %>
            <%= this.InputFor(m => m.Dinner.Longitude) %>
        </p>                 
        <p>
            <input type="submit" value="Save" />
        </p>
            
        </div>
        
        <div id="mapDiv">    
            <!-- Html.RenderPartial("Map", Model.Dinner); -->
        </div> 
            
    </fieldset>

    <script type="text/javascript">
    //<![CDATA[
        $(document).ready(function() {
            $("#Address").blur(function(evt) {
                //If it's time to look for an address, 
                // clear out the Lat and Lon
                $("#Latitude").val("0");
                $("#Longitude").val("0");

                var address = jQuery.trim($("#Address").val());
                if (address.length < 1)
                    return;

                NerdDinner.FindAddressOnMap(address);
            });
        });
    //]]>
    </script>

<%= this.EndForm() %>
