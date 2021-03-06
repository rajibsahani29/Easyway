<%@ Page Language="VB" MasterPageFile="../main.master" AutoEventWireup="false" CodeFile="reports_cc_commision.aspx.vb" Inherits="reports_cc_commision"  ValidateRequest="false"%>
<%@ MasterType virtualpath="~/main.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        
   <h1 class="page-title" style="text-align:center;">Credit Card Commission</h1>
   
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

                                <div class="form-action clearfix " style="float:right;">
                                <asp:Button ID="BUT__Show_All" UseSubmitBehavior="true" runat="server" Text="Show All" />
                          
</div></div>      
                    
 

		</div>
     <br />
         <div class="container-fluid invoice-container" id="printablediv"   style="position:relative;">
 
<div class="pull-right btn-group btn-group-sm hidden-print">
                <a href="javascript:printDiv('printablediv')"  style="color:#000;font-weight:bold;" class="btn btn-default"><i class="fa fa-print"></i> Print</a>
                
            </div><br />
<div id="DIV_Agent_Route_Date" runat="server">
<%--<table id="reports" >

     <tr>
        <th colspan="9">Total Commission&nbsp;&#61;&nbsp;<asp:Label ID="LABEL_Total_CC_Commission" runat="server" Text="�1235.25"></asp:Label> </th>
    </tr>

   
    <tr style ="background-color:#dafbfb;">

        <td>
Booking Ref:
</td>
        <td>
Total Amount
</td>
        <td>
CC Commission
</td>
    </tr>
        <tr>

        <td>
<asp:Label ID="LABEL_Booking_Ref" runat="server" Text="JB19061756"></asp:Label>
</td>
        <td>
<asp:Label ID="LABEL_total_cost_of_booking" runat="server" Text="�123.12"></asp:Label>
</td>
        <td>
<asp:Label ID="LABEL_CC_Commission" runat="server" Text="�32.50"></asp:Label>
</td>
    </tr>

</table> --%>
    <span id="spnTotalCommision" runat="server" style="display:none;">
        <h4>Total Commission&nbsp;&#61;&nbsp;<asp:Label ID="LABEL_Total_CC_Commission" runat="server" Text="�0.00"></asp:Label></h4>
    </span>
    <asp:GridView ID="GRID_CC_Commision" Width="98%" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Horizontal"
        AllowPaging="true" pagesize="2000" AutoGenerateColumns="false" DataKeyNames="booking_id" 
        emptydatatext="No Records Found" ShowHeaderWhenEmpty="True" 
        OnSelectedIndexChanging="GRID_CC_Commision_SelectedIndexChanged" OnPageIndexChanging="GRID_CC_Commision_PageIndexChanging">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <EditRowStyle BackColor="#999999" />
        <HeaderStyle BackColor="#ABC4DC" Font-Bold="True" Font-Size="Large"  ForeColor="White" Height="25px" />
        <Columns>
            <asp:BoundField DataField="unique_id" HeaderText="Booking Ref" ReadOnly="true" />
            <asp:TemplateField HeaderText="Total Amount">
                <ItemTemplate>
                    <asp:Label ID="LABEL_TotalAmount" runat="server" Text='<%# objFunction.ReturnDouble(objPaymentCalculation.GetTotalAmountPayable(Eval("booking_id"))).toString("0.00") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CC">
                <ItemTemplate>
                    <asp:Label ID="LABEL_CcCharge" runat="server" Text='<%# Eval("cc_charge") %>'/>                
                </ItemTemplate>
            </asp:TemplateField>
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
    </section>

 </div>        
    
</asp:Content>
