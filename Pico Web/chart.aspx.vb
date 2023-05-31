Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports System
Imports System.Text
Imports System.Web.Services
Imports System.Configuration
Imports System.Data.OleDb
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Net
Imports System.Collections
Imports FusionCharts.Charts
Imports System.Web.Script.Serialization
Imports System.Web.UI.DataVisualization.Charting
Imports System.Collections.Generic
Imports FirstDayOfWeek = Microsoft.VisualBasic.FirstDayOfWeek
Imports System.Drawing
Imports System.Globalization
Imports System.Dynamic
Imports Newtonsoft.Json.Linq
Imports Microsoft.CSharp
Imports System.Dynamic.Runtime
Imports System.Reflection.Emit
Imports IBM.Data.DB2.iSeries


Public Class chart
    Inherits System.Web.UI.Page
    Dim ipsql As String
    Dim usrsql As String
    Dim pswsql As String
    Dim dbsql As String
    Public Property _Result1 As String
    Public Property _Result2 As String
    Public Property _Result3 As String
    Public Property _Result4 As String
    Public Property _Result5 As String
    Public Property _Result6 As String


    <WebMethod()>
    Public Shared Function GetEnt(prefix As String) As String()
        '------LEGGO CONFIGURAZIONE PER CONNESSIONE MYSQL------------
        Dim ipsql As String
        Dim usrsql As String
        Dim pswsql As String
        Dim dbsql As String
        Dim dtstart As String

        '------LEGGO CONFIGURAZIONE PER CONNESSIONE MYSQL------------

        Dim objReader As New StreamReader("c:\condb2_ara.txt")
        Dim sLine As String = ""
        Dim arrText As New ArrayList()

        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                arrText.Add(sLine)
            End If
        Loop Until sLine Is Nothing
        objReader.Close()

        For Each sLine In arrText


            Console.WriteLine(sLine)


            Dim words As String() = sLine.Split(New Char() {","})

            ipsql = words(0)
            usrsql = words(1)
            pswsql = words(2)
            dbsql = words(3)

        Next

        Dim connection As New OleDb.OleDbConnection("Provider=IBMDA400;" &
                               "Data Source=" & ipsql & ";" &
                               "Force Translate=0;" &
                               "Default Collection=" & dbsql & ";" &
                               "User ID=" & usrsql & ";" &
                               "Password=" & pswsql)

        Dim customers As New List(Of String)()

        connection.Open()
        Dim table1 As New DataTable()
        Dim adapter1 As New OleDb.OleDbDataAdapter("SELECT DISTINCT RPENT FROM PRODU00F WHERE RPENT LIKE '%" & prefix.ToUpper & "%' ORDER BY RPENT ASC", connection)

        adapter1.Fill(table1)

        For Each row In table1.Rows
            customers.Add(row(0))
        Next
        connection.Close()

        Return customers.ToArray()
    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        read_conn()
        If Not IsPostBack Then
            load_mc_list()
            Label2.Text = Date.Now.ToString("yyyy/MM/dd")
            Label3.Text = Date.Now.ToString("yyyy/MM/dd")

            Dim dt As New DataTable()
            dt.Columns.Add("Ora", GetType(String))
            dt.Rows.Add("")
            For i As Integer = 0 To 23
                For j As Integer = 0 To 1
                    Dim time As String = String.Format("{0:00}:{1:00}:00", i, j * 30)
                    dt.Rows.Add(time)
                Next
            Next

            DropDownList1.DataSource = dt
            DropDownList1.DataTextField = "Ora"
            DropDownList1.DataValueField = "Ora"
            DropDownList1.DataBind()

            DropDownList2.DataSource = dt
            DropDownList2.DataTextField = "Ora"
            DropDownList2.DataValueField = "Ora"
            DropDownList2.DataBind()

            DropDownList1.Text = ""
            DropDownList2.Text = ""

        End If


    End Sub

    Public Sub load_mc_list()
        Dim tb1 As New DataTable()
        tb1.Clear()
        sel_tb("SELECT DISTINCT key from CNFMACCH where key like 'M,%' AND DATI='PORTSTAT,S'", tb1)
        For Each row0 In tb1.Rows
            drpdwnlstMC.Items.Add(row0(0).ToString.Substring(2, row0(0).Length - 2))

        Next
    End Sub

    Public Sub sel_tb(strselect As String, tb As DataTable)

        Dim connection As New OleDb.OleDbConnection("Provider=IBMDA400;" &
                               "Data Source=" & ipsql & ";" &
                               "Force Translate=0;" &
                               "Default Collection=" & dbsql & ";" &
                               "User ID=" & usrsql & ";" &
                               "Password=" & pswsql)
        connection.Open()

        tb.Clear()

        Dim adapter0 As New OleDbDataAdapter(strselect, connection)
        adapter0.Fill(tb)

        connection.Close()
    End Sub

    Public Sub read_conn()

        '------LEGGO CONFIGURAZIONE PER CONNESSIONE MYSQL------------

        Dim objReader As New StreamReader("c:\condb2_ara.txt")
        Dim sLine As String = ""
        Dim arrText As New ArrayList()

        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                arrText.Add(sLine)
            End If
        Loop Until sLine Is Nothing
        objReader.Close()

        For Each sLine In arrText


            Console.WriteLine(sLine)


            Dim words As String() = sLine.Split(New Char() {","})

            ipsql = words(0)
            usrsql = words(1)
            pswsql = words(2)
            dbsql = words(3)

        Next

    End Sub
    Public Class Configtable
        Public Property name As String
        Public Property type As String
        Public Property format As String

    End Class
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Enabled = False
        loadchartdata()
    End Sub

    Public Sub loadchartdata()
        If drpdwnlstMC.Text <> "" Then
            If TextBox1.Text <> "" Or Label2.Text <> "" Then
                Dim tl1 = "0"
                Dim entgprec As String = ""
                Dim velprec As String = ""
                Dim fermmcprec As String = ""
                Dim levprec As String = ""
                Dim rollaprec As String = ""
                Dim rastrprec As String = ""

                Dim datadal As String = ""
                Dim dataal As String = ""
                Dim oradal As String = ""
                Dim oraal As String = ""
                Dim dalal As String = ""

                If Label2.Text.Replace("/", "") = "" Then
                    datadal = "RPDT like '%%'"
                Else
                    datadal = "RPDT >= '" & Label2.Text.Replace("/", "")
                End If

                If Label3.Text.Replace("/", "") = "" Then
                    dataal = "RPDT like '%%'"
                Else
                    dataal = "RPDT <= '" & Label3.Text.Replace("/", "")
                End If

                If DropDownList1.Text.Replace(":", "") = "" Then
                    oradal = "RPOR like '%%'"
                Else
                    oradal = "RPOR >= '" & DropDownList1.Text.Replace(":", "")
                End If

                If DropDownList2.Text.Replace(":", "") = "" Then
                    oraal = "RPOR like '%%'"
                Else
                    oraal = "RPOR <= '" & DropDownList2.Text.Replace(":", "")
                End If

                If DropDownList2.Text.Replace(":", "") <> "" And DropDownList1.Text.Replace(":", "") <> "" And Label3.Text.Replace("/", "") <> "" And Label2.Text.Replace("/", "") <> "" Then
                    dalal = "and ((RPDT = '" & Label2.Text.Replace("/", "") & "' and " & oradal & "') or (RPDT = '" & Label3.Text.Replace("/", "") & "' and " & oraal & "')) "


                End If



                Dim idmachine As String = drpdwnlstMC.Text.ToString.Substring(0, 3)
                Dim namemachine As String = drpdwnlstMC.Text.ToString.Substring(4, drpdwnlstMC.Text.ToString.Length - 4)
                Label1.Text = namemachine
                Label1.Visible = True

                Dim x1time As DateTime = New DateTime(1, 1, 1, 0, 0, 0)

                Dim Course As ArrayList = New ArrayList()
                Dim Course2 As ArrayList = New ArrayList()
                Dim Course3 As ArrayList = New ArrayList()
                Dim Course4 As ArrayList = New ArrayList()
                Dim Course5 As ArrayList = New ArrayList()
                Dim Course6 As ArrayList = New ArrayList()
                Dim varfermmc As Decimal = 0
                Dim varlevata As Decimal = 0
                Dim varrolla As Decimal = 0
                Dim varrastr As Decimal = 0
                Dim newDate As String = ""
                If Label2.Text <> "" Then
                    Dim currentDate As DateTime = DateTime.ParseExact(Label2.Text, "yyyy/MM/dd", CultureInfo.InvariantCulture)
                    Dim newDate0 As DateTime = currentDate.AddDays(-1)
                    newDate = newDate0.ToString("yyyyMMdd")
                End If
                If DropDownList1.Text = "" Then
                    Using cn As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=IBMDA400;" & "Data Source=" & ipsql & ";" & "Force Translate=0;" & "Default Collection=" & dbsql & ";" & "User ID=" & usrsql & ";" & "Password=" & pswsql)
                        Using cm As OleDbCommand = New OleDbCommand("SELECT rpvel,rpent from PRODU00F where  RPCDM='" & idmachine & "' and RPDT like '%" & newDate & "%' and rpent like '%" & TextBox1.Text & "%' and rpstop='0' order by RPDT desc, RPOR desc fetch first 1 rows only", cn)
                            cn.Open()
                            Dim reader As OleDbDataReader = cm.ExecuteReader()
                            While reader.Read()
                                entgprec = reader("rpent").ToString.PadLeft(6, "0")
                                velprec = reader("rpvel").ToString.Replace(",", ".")

                            End While
                        End Using
                    End Using

                    Using cn As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=IBMDA400;" & "Data Source=" & ipsql & ";" & "Force Translate=0;" & "Default Collection=" & dbsql & ";" & "User ID=" & usrsql & ";" & "Password=" & pswsql)
                        Using cm As OleDbCommand = New OleDbCommand("SELECT rpstat from PRODU00F where RPCDM='" & idmachine & "' and RPDT like '%" & newDate & "%' and rpent like '%" & TextBox1.Text & "%' and rpstop='1' order by RPDT desc, RPOR desc fetch first 1 rows only", cn)
                            cn.Open()
                            Dim reader As OleDbDataReader = cm.ExecuteReader()
                            While reader.Read()

                                If reader("rpstat").ToString = "1" Then
                                    fermmcprec = "0.1"
                                Else
                                    fermmcprec = "0"
                                End If

                            End While
                        End Using
                    End Using

                    Using cn As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=IBMDA400;" & "Data Source=" & ipsql & ";" & "Force Translate=0;" & "Default Collection=" & dbsql & ";" & "User ID=" & usrsql & ";" & "Password=" & pswsql)
                        Using cm As OleDbCommand = New OleDbCommand("SELECT rpstat from PRODU00F where RPCDM='" & idmachine & "' and RPDT like '%" & newDate & "%' and rpent like '%" & TextBox1.Text & "%' and rpstop='2' order by RPDT desc, RPOR desc fetch first 1 rows only", cn)
                            cn.Open()
                            Dim reader As OleDbDataReader = cm.ExecuteReader()
                            While reader.Read()

                                If reader("rpstat").ToString = "1" Then
                                    levprec = "0.12"
                                Else
                                    levprec = "0"
                                End If

                            End While
                        End Using
                    End Using

                    Using cn As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=IBMDA400;" & "Data Source=" & ipsql & ";" & "Force Translate=0;" & "Default Collection=" & dbsql & ";" & "User ID=" & usrsql & ";" & "Password=" & pswsql)
                        Using cm As OleDbCommand = New OleDbCommand("SELECT rpstat from PRODU00F where RPCDM='" & idmachine & "' and RPDT like '%" & newDate & "%' and rpent like '%" & TextBox1.Text & "%' and rpstop='3' order by RPDT desc, RPOR desc fetch first 1 rows only", cn)
                            cn.Open()
                            Dim reader As OleDbDataReader = cm.ExecuteReader()
                            While reader.Read()

                                If reader("rpstat").ToString = "1" Then
                                    rollaprec = "0.14"
                                Else
                                    rollaprec = "0"
                                End If

                            End While
                        End Using
                    End Using

                    Using cn As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=IBMDA400;" & "Data Source=" & ipsql & ";" & "Force Translate=0;" & "Default Collection=" & dbsql & ";" & "User ID=" & usrsql & ";" & "Password=" & pswsql)
                        Using cm As OleDbCommand = New OleDbCommand("SELECT rpstat from PRODU00F where RPCDM='" & idmachine & "' and RPDT like '%" & newDate & "%' and rpent like '%" & TextBox1.Text & "%' and rpstop='4' order by RPDT desc, RPOR desc fetch first 1 rows only", cn)
                            cn.Open()
                            Dim reader As OleDbDataReader = cm.ExecuteReader()
                            While reader.Read()

                                If reader("rpstat").ToString = "1" Then
                                    rastrprec = "0.16"
                                Else
                                    rastrprec = "0"
                                End If

                            End While
                        End Using
                    End Using

                End If


                Using cn As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=IBMDA400;" & "Data Source=" & ipsql & ";" & "Force Translate=0;" & "Default Collection=" & dbsql & ";" & "User ID=" & usrsql & ";" & "Password=" & pswsql)
                    Using cm As OleDbCommand = New OleDbCommand("Select * from PRODU00F where RPCDM='" & idmachine & "' and " & datadal & "' and " & dataal & "' " & dalal & " and rpent like '%" & TextBox1.Text & "%' order by RPDT asc, RPOR asc", cn)
                        cn.Open()
                        Dim reader As OleDbDataReader = cm.ExecuteReader()
                        While reader.Read()
                            Dim tempox As String = reader("rpor").ToString.PadLeft(6, "0")
                            Dim datax As String = reader("rpdt").ToString.Substring(0, 4) & "/" & reader("rpdt").ToString.Substring(4, 2) & "/" & reader("rpdt").ToString.Substring(6, 2)

                            If DropDownList1.Text <> "" And tl1 = "0" Then
                                x1time = tempox.Substring(0, 2) & ":" & tempox.Substring(2, 2) & ":" & tempox.Substring(4, 2)
                                tl1 = "1"
                            End If

                            If x1time > "23:59:59" Then
                                x1time = New DateTime(1, 1, 1, 0, 0, 0)
                            End If

                            Do While x1time < tempox.Substring(0, 2) & ":" & tempox.Substring(2, 2) & ":" & tempox.Substring(4, 2)
                                Course.Add(datax & " - " & x1time & " - " & entgprec)
                                x1time = x1time.AddSeconds(1)
                                Course2.Add(velprec)
                                Course3.Add(varfermmc)
                                Course4.Add(varlevata)
                                Course5.Add(varrolla)
                                Course6.Add(varrastr)

                            Loop

                            Course.Add(datax & " - " & tempox.Substring(0, 2) & ":" & tempox.Substring(2, 2) & ":" & tempox.Substring(4, 2) & " - " & reader("rpent").ToString)
                            entgprec = reader("rpent").ToString
                            x1time = x1time.AddSeconds(1)

                            Course2.Add(reader("rpvel").ToString.Replace(",", "."))
                            velprec = (reader("rpvel").ToString.Replace(",", "."))

                            Select Case reader("rpstop").ToString
                                Case "0" 'Velocità
                                    varfermmc = 0
                                    varlevata = 0
                                    varrolla = 0
                                    varrastr = 0
                                    Course3.Add(varfermmc)
                                    Course4.Add(varlevata)
                                    Course5.Add(varrolla)
                                    Course6.Add(varrastr)

                                Case "1" 'Fermo Macchina
                                    If reader("rpstat").ToString = "1" Then
                                        varfermmc = 0.1
                                    Else
                                        varfermmc = 0
                                    End If

                                    Course3.Add(varfermmc)
                                    Course4.Add(varlevata)
                                    Course5.Add(varrolla)
                                    Course6.Add(varrastr)

                                Case "2" 'Levata
                                    If reader("rpstat").ToString = "1" Then
                                        varlevata = 0.12
                                    Else
                                        varlevata = 0
                                    End If

                                    Course3.Add(varfermmc)
                                    Course4.Add(varlevata)
                                    Course5.Add(varrolla)
                                    Course6.Add(varrastr)

                                Case "3" 'Rolla
                                    If reader("rpstat").ToString = "1" Then
                                        varrolla = 0.14
                                    Else
                                        varrolla = 0
                                    End If

                                    Course3.Add(varfermmc)
                                    Course4.Add(varlevata)
                                    Course5.Add(varrolla)
                                    Course6.Add(varrastr)

                                Case "4" 'Rastrelliera
                                    If reader("rpstat").ToString = "1" Then
                                        varrastr = 0.16
                                    Else
                                        varrastr = 0
                                    End If

                                    Course3.Add(varfermmc)
                                    Course4.Add(varlevata)
                                    Course5.Add(varrolla)
                                    Course6.Add(varrastr)
                            End Select

                        End While
                    End Using
                End Using

                Dim serializer As New JavaScriptSerializer()
                serializer.MaxJsonLength = Integer.MaxValue
                _Result1 = serializer.Serialize(Course)

                '_Result1 = New Web.Script.Serialization.JavaScriptSerializer().Serialize(Course)
                _Result2 = New Web.Script.Serialization.JavaScriptSerializer().Serialize(Course2)
                _Result3 = New Web.Script.Serialization.JavaScriptSerializer().Serialize(Course3)
                _Result4 = New Web.Script.Serialization.JavaScriptSerializer().Serialize(Course4)
                _Result5 = New Web.Script.Serialization.JavaScriptSerializer().Serialize(Course5)
                _Result6 = New Web.Script.Serialization.JavaScriptSerializer().Serialize(Course6)

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "CallMyFunction", "code1();", True)
            Else
                Response.Write("<script>alert('Seleziona almeno un filtro, Data o Entità');</script>")

            End If

        Else
            Response.Write("<script>alert('Nessuna Macchina Selezionata');</script>")

        End If
    End Sub


    Private Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", " $(function () {PopUpModel2();});", True)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", " $(function () {PopUpModel2_close();});", True)
        Calendar1.SelectedDate = Now.Date
        Label2.Text = Calendar1.SelectedDate.ToString("yyyy/MM/dd")
    End Sub
    Private Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged

        Dim settcal As String = DatePart(DateInterval.WeekOfYear, Calendar1.SelectedDate.Date, FirstDayOfWeek.Monday, FirstWeekOfYear.System)

        Label2.Text = Calendar1.SelectedDate.ToString("yyyy/MM/dd")

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", " $(function () {PopUpModel_close();});", True)

    End Sub

    Protected Sub gv2_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If (e.Row.RowType = DataControlRowType.Header) Then
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right

        End If

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub

    Private Sub drpdwnlstMC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpdwnlstMC.SelectedIndexChanged
        Label1.Text = ""
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label2.Text = ""
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Label3.Text = ""

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Calendar2.SelectedDate = Now.Date
        Label3.Text = Calendar1.SelectedDate.ToString("yyyy/MM/dd")
    End Sub

    Private Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", " $(function () {PopUpModel();});", True)

    End Sub

    Private Sub Calendar2_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar2.SelectionChanged

        Dim settcal As String = DatePart(DateInterval.WeekOfYear, Calendar2.SelectedDate.Date, FirstDayOfWeek.Monday, FirstWeekOfYear.System)

        Label3.Text = Calendar2.SelectedDate.ToString("yyyy/MM/dd")

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "key", " $(function () {PopUpModel_close();});", True)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Label2.Text = Date.Now.ToString("yyyy/MM/dd")
        Label3.Text = Date.Now.ToString("yyyy/MM/dd")
        DropDownList1.Text = ""
        DropDownList2.Text = ""
        loadchartdata()
        Timer1.Interval = 300000
        Timer1.Enabled = True

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False

        Label2.Text = Date.Now.ToString("yyyy/MM/dd")
        Label3.Text = Date.Now.ToString("yyyy/MM/dd")
        DropDownList1.Text = ""
        DropDownList2.Text = ""
        loadchartdata()

        Timer1.Enabled = True

    End Sub
End Class