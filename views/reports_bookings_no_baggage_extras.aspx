<%@ Page Language="VB" MasterPageFile="../main.master" AutoEventWireup="false" CodeFile="reports_bookings_no_baggage_extras.aspx.vb" Inherits="reports_bookings_no_baggage_extras"  ValidateRequest="false"%>
<%@ MasterType virtualpath="~/main.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <h1 class="page-title" style="text-align:center;">Bookings with no Baggage/Extras</h1>
   
    <div class="container_12 clearfix leading">
            

 <section class="grid_12">


				<div class="form has-validation">


              
                              <div class="clearfix">
                                  <asp:Label ID="LABEL_Date_From" runat="server" class="form-label" Text="Start Date"></asp:Label>
                              
                                  <div class="form-input">
                                <asp:TextBox ID="TB_Date_From" class="date" type="date" name="date" placeholder="mm/dd/yyyy"   runat="server"></asp:TextBox>

                                    

                          </div> </div>
               
                              <div class="clearfix">
                                  <asp:Label ID="LABEL_Date_To" runat="server" class="form-label" Text="End Date"></asp:Label>
                              
                                  <div class="form-input">
                                <asp:TextBox ID="TB_Date_To" class="date" type="date" name="date" placeholder="mm/dd/yyyy"   runat="server"></asp:TextBox>

                                    

                          </div> </div>

                     <div class="clearfix">
                                 
                             <asp:Label ID="LABEL_Baggage_Extra" runat="server" class="form-label" Text="Baggage or Extras"></asp:Label>

                         <div class="form-input">
                               <asp:RadioButtonList ID="Radio_Baggage_Extra" runat="server">
                               <asp:ListItem Text="Baggage" Value="0"></asp:ListItem>
                               <asp:ListItem Text="Extras" Value="1"></asp:ListItem>
                         </asp:RadioButtonList>

                          </div>
                                 </div>

                      
               
                     <div class="clearfix">

                                <div class="form-action clearfix " style="float:right;">
                                <asp:Button ID="BUT__Show_All" UseSubmitBehavior="true" runat="server" Text="Show All" />
                          
</div></div>      
                    
 

		</div>
     <br />
         <div class="container-fluid invoice-container" id="printablediv"   style="position:relative;">
 
<div class="pull-right btn-group btn-group-sm hidden-print">
                <a href="javascript:printDiv('printablediv')"  style="color:#000;font-weight:bold;" class="btn btn-default"><i class="fa fa-print"></i> Print</a>
                
            </div>
<div id="DIV_bookings_no_baggage_extras" runat="server">
<div style="overflow-x:auto;">
    <asp:GridView ID="GRID_Booking_No_Baggage_Extras" Width="98%" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Horizontal"
        AllowPaging="true" pagesize="2000" AutoGenerateColumns="false" DataKeyNames="id" 
        emptydatatext="No Records Found" ShowHeaderWhenEmpty="True" 
        OnSelectedIndexChanging="GRID_Booking_No_Baggage_Extras_SelectedIndexChanged" OnPageIndexChanging="GRID_Booking_No_Baggage_Extras_PageIndexChanging">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <EditRowStyle BackColor="#999999" />
        <HeaderStyle BackColor="#ABC4DC" Font-Bold="True" Font-Size="Large"  ForeColor="White" Height="25px" />
        <Columns>
            <asp:BoundField DataField="AgentName" HeaderText="Agent" ReadOnly="true" />
            <asp:TemplateField HeaderText="Date Created">
                <ItemTemplate>
                    <asp:Label ID="LABEL_DateCreated" runat="server" Text='<%# SetDateVal(Eval("datecreated")) %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start Date">
                <ItemTemplate>
                    <asp:Label ID="LABEL_StartDate" runat="server" Text='<%# SetDateVal(Eval("checkin_earliest")) %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RouteName" HeaderText="Route" ReadOnly="true" />
            <asp:BoundField DataField="ClientName" HeaderText="Client" ReadOnly="true" />
            <asp:BoundField DataField="unique_id" HeaderText="Booking Ref" ReadOnly="true" />
            <asp:CommandField ShowSelectButton="true" />
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" Font-Size="Small" Height="50px"/>
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>

    </div>


</div>
 
</div>
    </section>

 </div>        
    

</asp:Content>
