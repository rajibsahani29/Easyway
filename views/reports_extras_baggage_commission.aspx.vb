﻿Imports System.Data
Imports Easyway.BE
Imports Easyway.DL

Partial Class reports_extras_baggage_commission
    Inherits System.Web.UI.Page

    Protected objFunction As New clsCommon()
    Protected objPaymentCalculation As clsPaymentCalculation = New clsPaymentCalculation

    ''' <summary>
    ''' Load event of the page
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not objFunction.ValidateLogin() Then
                Response.Redirect("~/login.aspx")
            End If

            If Not Page.IsPostBack Then

                Trace.Warn("Session = " + Session("feedback_call"))
                If Session("feedback_call") <> "" And objFunction.ReturnInteger(Session("feedback_call")) = 1 Then
                    Me.Master.show_feedback_layer(objFunction.ReturnInteger(Session("feedback_call")), objFunction.ReturnString(Session("error_msg")))
                ElseIf Session("feedback_call") <> "" And objFunction.ReturnInteger(Session("feedback_call")) = 2 Then
                    Me.Master.show_feedback_layer(objFunction.ReturnInteger(Session("feedback_call")), objFunction.ReturnString(Session("error_msg")))
                End If
                Session("feedback_call") = "0"
                Session("error_msg") = ""
                Trace.Warn("End Session = " + Session("feedback_call"))

            End If

        Catch ex As Exception
            HttpContext.Current.Trace.Warn("Exception message:  ::", ex.Message)
            HttpContext.Current.Trace.Warn("Error Stack Trace ::", ex.StackTrace)
        End Try

    End Sub

    ''' <summary>
    ''' This function is used to bind gridview
    ''' </summary>
    Protected Sub BindGridview()

        Try
            Dim intCompanyId = objFunction.ReturnInteger(Session("CompanyId"))

            Dim strSearchByFromDate As String = ""
            If TB_Date_From.Text <> "" Then
                Dim dt As DateTime = (If(TB_Date_From.Text <> "", Convert.ToDateTime(TB_Date_From.Text), DateTime.MinValue))
                strSearchByFromDate = dt.ToString("MM-dd-yyyy")
            End If

            Dim strSearchByToDate As String = ""
            If TB_Date_To.Text <> "" Then
                Dim dt As DateTime = (If(TB_Date_To.Text <> "", Convert.ToDateTime(TB_Date_To.Text), DateTime.MinValue))
                strSearchByToDate = dt.ToString("MM-dd-yyyy")
            End If

            Dim strSearchByBaggage As String = ""
            Dim strSearchByExtra As String = ""

            If objFunction.ReturnInteger(Radio_Baggage_Extra.SelectedItem.Value) = 0 Then
                strSearchByBaggage = "True"
            End If

            If objFunction.ReturnInteger(Radio_Baggage_Extra.SelectedItem.Value) = 1 Then
                strSearchByExtra = "True"
            End If
            
            Dim dstReportExtraBaggageCommision As DataSet = (New clsDLBookingReport()).GetReportExtraBaggageCommision(intCompanyId, strSearchByFromDate, strSearchByToDate, strSearchByBaggage, strSearchByExtra)
            GRID_Extras_Baggage_Commission.DataSource = dstReportExtraBaggageCommision
            GRID_Extras_Baggage_Commission.DataBind()

            Dim dblTotalCommision As Double = 0
            For i As Integer = 0 To GRID_Extras_Baggage_Commission.Rows.Count - 1
                Dim gRow As GridViewRow = GRID_Extras_Baggage_Commission.Rows(i)
                Dim lblCommission As Label = DirectCast(gRow.FindControl("LABEL_Commission"), Label)
                dblTotalCommision += objFunction.ReturnDouble(lblCommission.Text)
            Next

            spnTotalCommision.Attributes.Add("style", "display:block;")
            LABEL_Total_CC_Commission.Text = "£" + dblTotalCommision.ToString("0.00")

        Catch ex As Exception
            HttpContext.Current.Trace.Warn("Exception message:  ::", ex.Message)
            HttpContext.Current.Trace.Warn("Error Stack Trace ::", ex.StackTrace)
        End Try

    End Sub

    ''' <summary>
    ''' PageIndexChanging event of the GRID_Extras_Baggage_Commission
    ''' </summary>
    Protected Sub GRID_Extras_Baggage_Commission_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)

        Try
            GRID_Extras_Baggage_Commission.PageIndex = e.NewPageIndex
            BindGridview()
        Catch ex As Exception
            HttpContext.Current.Trace.Warn("Exception message:  ::", ex.Message)
            HttpContext.Current.Trace.Warn("Error Stack Trace ::", ex.StackTrace)
        End Try

    End Sub

    ''' <summary>
    ''' SelectedIndexChanged event of the GRID_Extras_Baggage_Commission
    ''' </summary>
    Protected Sub GRID_Extras_Baggage_Commission_SelectedIndexChanged(ByVal sender As Object, ByVal e As GridViewSelectEventArgs)
        Try
            'Trace.Warn("Value = " + objFunction.ReturnString(GRID_Extras_Baggage_Commission.DataKeys(e.NewSelectedIndex).Value))
            Session("BookingId") = objFunction.ReturnString(GRID_Extras_Baggage_Commission.DataKeys(e.NewSelectedIndex).Value)
            Response.Redirect("bookings_view.aspx")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn("Exception message:  ::", ex.Message)
            HttpContext.Current.Trace.Warn("Error Stack Trace ::", ex.StackTrace)
        End Try
    End Sub

    ''' <summary>
    ''' Clieck event of button to get record
    ''' </summary>
    Protected Sub BUT__Show_All_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT__Show_All.Click
        Try
            BindGridview()
        Catch ex As Exception
            HttpContext.Current.Trace.Warn("Exception message:  ::", ex.Message)
            HttpContext.Current.Trace.Warn("Error Stack Trace ::", ex.StackTrace)
        End Try
    End Sub

    ''' <summary>
    ''' This function is used to set date value
    ''' </summary>
    Public Function SetDateVal(ByVal value As Object) As String
        Try
            If objFunction.ReturnString(value) <> "" Then
                Dim dt As DateTime = Convert.ToDateTime(value)
                Return dt.ToString("MM/dd/yyyy")
            Else
                Return ""
            End If
        Catch ex As Exception
            HttpContext.Current.Trace.Warn("Exception message:  ::", ex.Message)
            HttpContext.Current.Trace.Warn("Error Stack Trace ::", ex.StackTrace)
        End Try
        Return ""
    End Function

End Class