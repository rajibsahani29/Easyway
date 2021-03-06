﻿Imports System.Data
Imports Easyway.BE
Imports Easyway.DL

Partial Class accom_link
    Inherits System.Web.UI.Page

    Dim objFunction As New clsCommon()
    Dim objBEAccomodationStage As clsBEAccomodationStage = New clsBEAccomodationStage
    Dim objDLAccomodationStage As clsDLAccomodationStage = New clsDLAccomodationStage

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

                Dim intCompanyId = objFunction.ReturnInteger(Session("CompanyId"))
                Dim dstAccomodation As DataSet = (New clsDLAccomodation()).GetAccommodationDetailFillInDD(intCompanyId)
                objFunction.FillDropDownByDataSet(DROP_Accommodation, dstAccomodation)
                DROP_Accommodation.Items.Insert(0, New ListItem("Select Accommodation", ""))

                Dim dstStage As DataSet = (New clsDLStages()).GetStagesFillInDD(intCompanyId)
                objFunction.FillDropDownByDataSet(DROP_Stage, dstStage)
                DROP_Stage.Items.Insert(0, New ListItem("Select Stage", ""))

                BindGridview()

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
            Dim dstAccomodationStage As New DataSet()
            dstAccomodationStage = (New clsDLAccomodationStage()).GetAccomodationStage(intCompanyId)
            GridView1.DataSource = dstAccomodationStage
            GridView1.DataBind()
        Catch ex As Exception
            HttpContext.Current.Trace.Warn("Exception message:  ::", ex.Message)
            HttpContext.Current.Trace.Warn("Error Stack Trace ::", ex.StackTrace)
        End Try

    End Sub

    ''' <summary>
    ''' OnRowDataBound event of the GridView1
    ''' </summary>
    Protected Sub GridView1_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lb As LinkButton = DirectCast(e.Row.Cells(3).Controls(0), LinkButton)
                If lb IsNot Nothing Then
                    If lb.Text = "Delete" Then
                        lb.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to delete this record ?')")
                    End If
                End If
            End If
        Catch ex As Exception
            HttpContext.Current.Trace.Warn("Exception message:  ::", ex.Message)
            HttpContext.Current.Trace.Warn("Error Stack Trace ::", ex.StackTrace)
        End Try

    End Sub

    ''' <summary>
    ''' PageIndexChanging event of the GridView1
    ''' </summary>
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)

        Try
            GridView1.PageIndex = e.NewPageIndex
            BindGridview()
        Catch ex As Exception
            HttpContext.Current.Trace.Warn("Exception message:  ::", ex.Message)
            HttpContext.Current.Trace.Warn("Error Stack Trace ::", ex.StackTrace)
        End Try

    End Sub

    ''' <summary>
    ''' RowDeleting event of the GridView1
    ''' </summary>
    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)

        Try
            objBEAccomodationStage.AccomodationStageId = objFunction.ReturnInteger(objFunction.ReturnString(GridView1.DataKeys(e.RowIndex).Values("id")))
            Dim intAffectedRow As Integer = objDLAccomodationStage.DeleteAccomodationStage(objBEAccomodationStage)
            If intAffectedRow > 0 Then
                'Add Activity - Start
                Dim objBEActivity As New clsBEActivity

                objBEActivity.Descx = objFunction.ReturnString(GridView1.DataKeys(e.RowIndex).Values("AccomName")) + " had been deleted by " + objFunction.ReturnString(Session("LoginUserFirstName"))
                objBEActivity.StaffId = objFunction.ReturnInteger(Session("LoginUserId"))

                Dim objDLActivity As New clsDLActivity
                objDLActivity.AddActivity(objBEActivity)
                'Add Activity - End
                Session("feedback_call") = "1"
                Session("error_msg") = "Route has been deleted"
            Else
                Session("feedback_call") = "2"
                Session("error_msg") = "There was a system error. If this error persists please contact technical support."
            End If
            GridView1.EditIndex = -1
            'BindGridview()
            Response.Redirect("accom_link.aspx", False)
        Catch ex As Exception
            HttpContext.Current.Trace.Warn("Exception message:  ::", ex.Message)
            HttpContext.Current.Trace.Warn("Error Stack Trace ::", ex.StackTrace)
        End Try

    End Sub

    ''' <summary>
    ''' This event is used to add route stage details
    ''' </summary>
    Protected Sub BUT_Add_New_Link_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BUT_Add_New_Link.Click

        Try
            Dim intCompanyId = objFunction.ReturnInteger(Session("CompanyId"))
            objBEAccomodationStage.AccomId = objFunction.ReturnInteger(DROP_Accommodation.SelectedItem.Value)
            objBEAccomodationStage.StageId = objFunction.ReturnInteger(DROP_Stage.SelectedItem.Value)
            objBEAccomodationStage.CompanyId = intCompanyId

            Dim dstCheckDuplicateRecord As DataSet = objDLAccomodationStage.CheckDuplicateRecord(objBEAccomodationStage, intCompanyId)

            If objFunction.CheckDataSet(dstCheckDuplicateRecord) Then
                Session("feedback_call") = "2"
                Session("error_msg") = "WARNING - Record was NOT added - entry already exists"
            Else
                Dim intAffectedRow As Integer = objDLAccomodationStage.AddAccomodationStage(objBEAccomodationStage)
                If intAffectedRow > 0 Then
                    'Add Activity - Start
                    Dim objBEActivity As New clsBEActivity
                    objBEActivity.Descx = objFunction.ReturnString(DROP_Accommodation.SelectedItem.Text) + " had been linked to " + objFunction.ReturnString(DROP_Stage.SelectedItem.Text) + " by " + objFunction.ReturnString(Session("LoginUserFirstName"))
                    objBEActivity.StaffId = objFunction.ReturnInteger(Session("LoginUserId"))

                    Dim objDLActivity As New clsDLActivity
                    objDLActivity.AddActivity(objBEActivity)
                    'Add Activity - End
                    Session("feedback_call") = "1"
                    Session("error_msg") = "New route has been added"
                Else
                    Session("feedback_call") = "2"
                    Session("error_msg") = "There was a system error. If this error persists please contact technical support."
                End If
            End If
            Response.Redirect("accom_link.aspx", False)
        Catch ex As Exception
            HttpContext.Current.Trace.Warn("Exception message:  ::", ex.Message)
            HttpContext.Current.Trace.Warn("Error Stack Trace ::", ex.StackTrace)
        End Try

    End Sub
End Class





