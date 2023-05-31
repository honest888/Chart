Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports System
Imports System.Text
Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Configuration
Imports System.Data.OleDb

Public Class log
    Inherits System.Web.UI.Page
    Dim ipsql As String
    Dim usrsql As String
    Dim pswsql As String
    Dim dbsql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        read_conn()

        Dim connection As New OleDb.OleDbConnection("Provider=IBMDA400;" &
                               "Data Source=" & ipsql & ";" &
                               "Force Translate=0;" &
                               "Default Collection=" & dbsql & ";" &
                               "User ID=" & usrsql & ";" &
                               "Password=" & pswsql)


        If Request.Url.ToString.Contains("?") Then
            Dim tb1 As New DataTable()

            Dim VARTRASP As String = ""

            VARTRASP = Request.Params("P").ToString()

            Dim VARTRASP_ARRAY As String() = VARTRASP.Split(New Char() {","})

            connection.Open()

            Dim adapter1 As New OleDbDataAdapter("SELECT * from PRODU00F where RPCDM='" & VARTRASP_ARRAY(0) & "' order by RPDT asc, RPOR asc", connection)
            adapter1.Fill(tb1)

            connection.Close()

            If tb1.Rows.Count > 0 Then
                GridView1.DataSource = tb1
                GridView1.DataBind()

            End If

        End If
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

End Class