<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Attend_Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="jumbotron">
        <h4>eSSL Attendance System</h4>

        <div class="row">
            <div class="col-md-6">
                <p>
                    <asp:RadioButton ID="rd_ByMonth" runat="server" class="btn btn-default" GroupName="SelectType" Text="By Day" />
                </p>

            </div>
           
            <div class="col-md-6">
                <p>
                    <asp:RadioButton ID="rd_ByCustom" runat="server" class="btn btn-default" GroupName="SelectType" Text="Custom Date" Checked="True"/>
                </p>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
               <p class="lead">
                   <asp:Label ID="Label1" runat="server" Text="Enter Date : "></asp:Label>
                    &nbsp;<asp:TextBox ID="txt_Date" runat="server" TextMode="Date" class="btn btn-default"></asp:TextBox>
                   
                </p>
            </div>
            
            <div class="col-md-6">
                <p class="lead">
                    Start Date :
            <asp:TextBox ID="dt_From" runat="server" TextMode="Date" class="btn btn-default"></asp:TextBox>
                </p>
                <p class="lead">
                    End Date :
            <asp:TextBox ID="dt_To" runat="server" TextMode="Date" class="btn btn-default"></asp:TextBox>
                </p>
                <p class="lead">
                    <asp:Label ID="Label3" runat="server" Text="Employee Name : "></asp:Label>
                    &nbsp;<asp:DropDownList ID="dp_EmpName" runat="server" class="btn btn-default" OnLoad="dp_EmpName_Load"></asp:DropDownList>
                </p>
            </div>
        </div>
        </div>
       
    <div class="col-md-12">
        <p class="lead">
            <asp:Button ID="bttn_Submit" runat="server" class="btn btn-default" Text="Submit" OnClick="bttn_Submit_Click" />
        </p>
    </div>
    <div class="col-md-12">
        <p class="lead">
            <asp:Label ID="DisxLab" runat="server" Text="" class="btn btn-default"></asp:Label>
            
        </p>
        <p class="lead">
            <asp:GridView ID="GridDix" runat="server" class="btn btn-default" OnPageIndexChanging="OnPageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="Both" PageSize="20" AllowPaging="True" HorizontalAlign="Center">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>

                <EditRowStyle BackColor="#999999"></EditRowStyle>

                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>

                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

                <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>

                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>

                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>

                <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>

                <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>

                <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
            </asp:GridView>
        </p>
    </div>

    

</asp:Content>
