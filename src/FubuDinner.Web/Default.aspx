<%@ Page Language="C#" AutoEventWireup="true"  %>

<script runat="server">
    // NOTE: This file exists solely to support F5-run support from Visual Studio
    // If you don't use F5-run for testing web projects (i.e. you use IIS and attach for debugging)
    // then you can safely delete this file without consequence.
    protected override void OnInit(EventArgs e)
    {
        var context = HttpContext.Current;
        context.RewritePath(Request.ApplicationPath);

        var module = context.ApplicationInstance.Modules["UrlRoutingModule"] as UrlRoutingModule;
        module.PostResolveRequestCache(new HttpContextWrapper(HttpContext.Current));
        module.PostMapRequestHandler(new HttpContextWrapper(HttpContext.Current));

        context.Handler.ProcessRequest(context);
    }
</script>