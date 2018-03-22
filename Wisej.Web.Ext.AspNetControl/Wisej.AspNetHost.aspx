<%@ Page Language="C#" 
    AutoEventWireup="false" 
    ViewStateMode="Enabled" %>

<%@ Register Assembly="System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" Namespace="System.Windows.Forms" TagPrefix="WinForms" %>

<!DOCTYPE html>

<html style="height: 100%; width: 100%; padding: 0px;">
<head runat="server">
    <title></title>
</head>
<body style="height: 100%; width: 100%; padding: 0px;">
    <form id="form" runat="server" style="height: 100%; width: 100%; padding: 0px;">
        <asp:ScriptManager ID="ScriptManager" runat="server">
        </asp:ScriptManager>
    </form>

    <script runat="server">

        protected override void OnPreInit(EventArgs e)
        {
            Wisej.Web.Application.RestoreSession(Context);

            var wrapper = this.Wrapper;
            if (wrapper != null)
                wrapper.OnPagePreInitCallback(this, this.form);

            base.OnPreInit(e);
        }

        protected override object LoadPageStateFromPersistenceMedium()
        {
            var wrapper = this.Wrapper;
            return wrapper != null && wrapper.UseSessionViewState
                ? wrapper.ViewState
                : base.LoadPageStateFromPersistenceMedium();
        }

        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            var wrapper = this.Wrapper;
            if (wrapper != null && wrapper.UseSessionViewState)
                wrapper.ViewState = viewState;
            else
                base.SavePageStateToPersistenceMedium(viewState);
        }

        private  Wisej.Web.Ext.AspNetControl.AspNetWrapperBase Wrapper
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    string id = HttpContext.Current.Request["_cid"];
                    return Wisej.Web.Application.FindComponent(o => o.Id == id) as Wisej.Web.Ext.AspNetControl.AspNetWrapperBase;
                }
                throw new InvalidOperationException("Invalid HttpContext.");
            }
        }

    </script>
</body>
</html>
