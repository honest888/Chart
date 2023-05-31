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

Public Class index
    Inherits System.Web.UI.Page
    Dim ipsql As String
    Dim usrsql As String
    Dim pswsql As String
    Dim dbsql As String
    Dim tb1 As New DataTable()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        read_conn()

        If Not IsPostBack Then
            corpo.Visible = False
            divmail.Visible = False
            load_mc_list()
            'dropdwlist ingressi attivi o disattivi
            DropDownList1.Items.Add("Attivo")
            DropDownList3.Items.Add("Attivo")
            DropDownList5.Items.Add("Attivo")
            DropDownList7.Items.Add("Attivo")
            DropDownList9.Items.Add("Attivo")
            DropDownList11.Items.Add("Attivo")
            DropDownList12.Items.Add("Attivo")
            DropDownList1.Items.Add("Disattivo")
            DropDownList3.Items.Add("Disattivo")
            DropDownList5.Items.Add("Disattivo")
            DropDownList7.Items.Add("Disattivo")
            DropDownList9.Items.Add("Disattivo")
            DropDownList11.Items.Add("Disattivo")
            DropDownList12.Items.Add("Disattivo")

            'dropdwnlist log mail
            DropDownList2.Items.Add("Attivo")
            DropDownList4.Items.Add("Attivo")
            DropDownList6.Items.Add("Attivo")
            DropDownList8.Items.Add("Attivo")
            DropDownList10.Items.Add("Attivo")
            DropDownList2.Items.Add("Disattivo")
            DropDownList4.Items.Add("Disattivo")
            DropDownList6.Items.Add("Disattivo")
            DropDownList8.Items.Add("Disattivo")
            DropDownList10.Items.Add("Disattivo")

            'dropdownlist colori ingressi
            Dim enumColor As ConsoleColor = New ConsoleColor

            Dim Colors As Array = [Enum].GetValues(enumColor.GetType)

            For Each clr As Object In Colors
                DropDownList13.Items.Add(clr.ToString)
                DropDownList14.Items.Add(clr.ToString)
                DropDownList15.Items.Add(clr.ToString)
                DropDownList16.Items.Add(clr.ToString)
                DropDownList17.Items.Add(clr.ToString)

            Next

            Dim i As Integer = 0
            Do While i <= 120
                DropDownList18.Items.Add(i)
                DropDownList19.Items.Add(i)
                DropDownList20.Items.Add(i)
                DropDownList21.Items.Add(i)
                DropDownList22.Items.Add(i)
                i += 1
            Loop

            Dim k As Integer = 0
            Do While k <= 100
                DropDownList23.Items.Add(k)
                k += 1
            Loop



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

    Public Sub load_mc_list()
        tb1.Clear()
        sel_tb("SELECT DISTINCT key from CNFMACCH where key like 'M,%'", tb1)
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim connection As New OleDb.OleDbConnection("Provider=IBMDA400;" &
                              "Data Source=" & ipsql & ";" &
                              "Force Translate=0;" &
                              "Default Collection=" & dbsql & ";" &
                              "User ID=" & usrsql & ";" &
                              "Password=" & pswsql)

        If drpdwnlstMC.Text <> "" Then
            Label1.Text = drpdwnlstMC.Text.ToString.Substring(0, 3)
            TextBox2.Text = drpdwnlstMC.Text.ToString.Substring(4, drpdwnlstMC.Text.ToString.Length - 4)

            Label2.Text = "M"
            corpo.Visible = True
            Button3.Visible = False
            Button2.Visible = False
            Button7.Visible = False
            divmail.Visible = False


            Dim tbreadmodif As New DataTable()
            tbreadmodif.Columns.Add("d0")
            tbreadmodif.Columns.Add("d1")
            tbreadmodif.Columns.Add("d2")
            tbreadmodif.Columns.Add("d3")
            tbreadmodif.Columns.Add("d4")
            tbreadmodif.Columns.Add("d5")

            Dim tbreadmodif_TEMP As New DataTable()


            connection.Open()
            Dim adapter0 As New OleDbDataAdapter("SELECT DATI from CNFMACCH where key = 'M," & drpdwnlstMC.Text & "'", connection)
            adapter0.Fill(tbreadmodif_TEMP)
            connection.Close()

            For Each row1 In tbreadmodif_TEMP.Rows
                Dim words As String() = row1(0).Split(New Char() {","})

                If words.Length > 2 Then
                    tbreadmodif.Rows.Add(words(0), words(1), words(2), words(3), words(4), words(5))
                ElseIf words.Length = 2 Then
                    tbreadmodif.Rows.Add(words(0), words(1), "", "", "", "")

                End If

            Next


            For Each row0 In tbreadmodif.Rows
                If row0(0) = "1" Then
                    DropDownList1.Text = row0(1)
                    TextBox3.Text = row0(2)
                    DropDownList13.Text = row0(3)
                    DropDownList2.Text = row0(4)
                    DropDownList18.Text = row0(5)

                    If DropDownList1.Text = "Disattivo" Then
                        TextBox3.Visible = False
                        DropDownList13.Visible = False
                        DropDownList2.Visible = False
                        DropDownList18.Visible = False
                    End If

                    If DropDownList2.Text = "Disattivo" Then
                        DropDownList18.Visible = False
                    End If

                ElseIf row0(0) = "2" Then
                    DropDownList3.Text = row0(1)
                    TextBox5.Text = row0(2)
                    DropDownList14.Text = row0(3)
                    DropDownList4.Text = row0(4)
                    DropDownList19.Text = row0(5)

                    If DropDownList3.Text = "Disattivo" Then
                        TextBox5.Visible = False
                        DropDownList14.Visible = False
                        DropDownList4.Visible = False
                        DropDownList19.Visible = False
                    End If

                    If DropDownList4.Text = "Disattivo" Then
                        DropDownList19.Visible = False
                    End If


                ElseIf row0(0) = "3" Then
                    DropDownList5.Text = row0(1)
                    TextBox7.Text = row0(2)
                    DropDownList15.Text = row0(3)
                    DropDownList6.Text = row0(4)
                    DropDownList20.Text = row0(5)

                    If DropDownList5.Text = "Disattivo" Then
                        TextBox7.Visible = False
                        DropDownList15.Visible = False
                        DropDownList6.Visible = False
                        DropDownList20.Visible = False
                    End If

                    If DropDownList6.Text = "Disattivo" Then
                        DropDownList20.Visible = False
                    End If

                ElseIf row0(0) = "4" Then
                    DropDownList7.Text = row0(1)
                    TextBox9.Text = row0(2)
                    DropDownList16.Text = row0(3)
                    DropDownList8.Text = row0(4)
                    DropDownList21.Text = row0(5)

                    If DropDownList7.Text = "Disattivo" Then
                        TextBox9.Visible = False
                        DropDownList16.Visible = False
                        DropDownList8.Visible = False
                        DropDownList21.Visible = False
                    End If

                    If DropDownList8.Text = "Disattivo" Then
                        DropDownList21.Visible = False
                    End If

                ElseIf row0(0) = "5" Then
                    DropDownList9.Text = row0(1)
                    TextBox11.Text = row0(2)
                    DropDownList17.Text = row0(3)
                    DropDownList10.Text = row0(4)
                    DropDownList22.Text = row0(5)

                    If DropDownList9.Text = "Disattivo" Then
                        TextBox11.Visible = False
                        DropDownList17.Visible = False
                        DropDownList10.Visible = False
                        DropDownList22.Visible = False
                    End If

                    If DropDownList10.Text = "Disattivo" Then
                        DropDownList22.Visible = False
                    End If

                ElseIf row0(0) = "SI" Then
                    DropDownList11.Text = row0(1)

                ElseIf row0(0) = "SU" Then
                    DropDownList12.Text = row0(1)

                ElseIf row0(0) = "RPM" Then
                    DropDownList23.Text = row0(1)

                ElseIf row0(0) = "CONV" Then
                    TextBox10.Text = row0(1)

                ElseIf row0(0) = "CONVS" Then
                    TextBox17.Text = row0(1)

                ElseIf row0(0) = "POLLINGSEC" Then
                    TextBox13.Text = row0(1)

                ElseIf row0(0) = "SOGLIASCRITT" Then
                    TextBox14.Text = row0(1)

                ElseIf row0(0) = "FLAGTRACK" Then
                    TextBox15.Text = row0(1)

                ElseIf row0(0) = "MINAGGSTAT" Then
                    TextBox16.Text = row0(1)

                ElseIf row0(0) = "PORTSTAT" Then

                    If row0(1).ToString = "S" Then
                        CheckBox1.Checked = True
                    Else
                        CheckBox1.Checked = False
                    End If
                End If

            Next


        Else
            Response.Write("<script>alert('Nessuna Macchina Selezionata');</script>")

        End If
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim tl1 As Integer = 0


        If DropDownList1.Text = "Attivo" And TextBox3.Text = "" Then
            tl1 += 1
            Response.Write("<script>alert('Ingresso1 senza alias');</script>")
        End If

        If DropDownList3.Text = "Attivo" And TextBox5.Text = "" Then
            tl1 += 1
            Response.Write("<script>alert('Ingresso2 senza alias');</script>")
        End If

        If DropDownList5.Text = "Attivo" And TextBox7.Text = "" Then
            tl1 += 1
            Response.Write("<script>alert('Ingresso3 senza alias');</script>")
        End If

        If DropDownList7.Text = "Attivo" And TextBox9.Text = "" Then
            tl1 += 1
            Response.Write("<script>alert('Ingresso4 senza alias');</script>")
        End If

        If DropDownList9.Text = "Attivo" And TextBox11.Text = "" Then
            tl1 += 1
            Response.Write("<script>alert('Ingresso5 senza alias');</script>")
        End If

        If TextBox2.Text = "" Then
            tl1 += 1
            Response.Write("<script>alert('Alias macchina mancante');</script>")

        End If

        If TextBox16.Text < "15" And TextBox16.Text <> "0" And TextBox16.Text <> "" Then
            tl1 += 1
            Response.Write("<script>alert('Tempo Aggiornamento statistiche troppo basso. Minimo 15 sec');</script>")

        End If

        If tl1 = "0" Then
            If Label2.Text = "M" Then

                sql_execution("DELETE FROM CNFMACCH WHERE KEY LIKE 'M," & Label1.Text & "%'")

            End If

            Dim in1 As String = "1," & DropDownList1.Text & "," & TextBox3.Text & "," & DropDownList13.Text & "," & DropDownList2.Text & "," & DropDownList18.Text
            Dim in2 As String = "2," & DropDownList3.Text & "," & TextBox5.Text & "," & DropDownList14.Text & "," & DropDownList4.Text & "," & DropDownList19.Text
            Dim in3 As String = "3," & DropDownList5.Text & "," & TextBox7.Text & "," & DropDownList15.Text & "," & DropDownList6.Text & "," & DropDownList20.Text
            Dim in4 As String = "4," & DropDownList7.Text & "," & TextBox9.Text & "," & DropDownList16.Text & "," & DropDownList8.Text & "," & DropDownList21.Text
            Dim in5 As String = "5," & DropDownList9.Text & "," & TextBox11.Text & "," & DropDownList17.Text & "," & DropDownList10.Text & "," & DropDownList22.Text
            Dim inSI As String = "SI," & DropDownList11.Text
            Dim inSU As String = "SU," & DropDownList12.Text
            Dim inRPM As String = "RPM," & DropDownList23.Text
            Dim inconv As String = "CONV," & TextBox10.Text.Replace(",", ".")
            Dim POLLINGSEC As String = "POLLINGSEC," & TextBox13.Text.Replace(",", ".")
            Dim SOGLIASCRITT As String = "SOGLIASCRITT," & TextBox14.Text.Replace(",", ".")
            Dim FLAGTRACK As String = "FLAGTRACK," & TextBox15.Text.Replace(",", ".")
            Dim MINAGGSTAT As String = "MINAGGSTAT," & TextBox16.Text.Replace(",", ".")
            Dim inconvS As String = "CONVS," & TextBox17.Text.Replace(",", ".")

            Dim PORTSTAT As String
            If CheckBox1.Checked = True Then
                PORTSTAT = "PORTSTAT,S"
            Else
                PORTSTAT = "PORTSTAT,N"

            End If


            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('M," & Label1.Text & "," & TextBox2.Text & "','" & in1 & "')")
            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('M," & Label1.Text & "," & TextBox2.Text & "','" & in2 & "')")
            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('M," & Label1.Text & "," & TextBox2.Text & "','" & in3 & "')")
            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('M," & Label1.Text & "," & TextBox2.Text & "','" & in4 & "')")
            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('M," & Label1.Text & "," & TextBox2.Text & "','" & in5 & "')")
            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('M," & Label1.Text & "," & TextBox2.Text & "','" & inSI & "')")
            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('M," & Label1.Text & "," & TextBox2.Text & "','" & inSU & "')")
            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('M," & Label1.Text & "," & TextBox2.Text & "','" & inRPM & "')")
            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('M," & Label1.Text & "," & TextBox2.Text & "','" & inconv & "')")
            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('M," & Label1.Text & "," & TextBox2.Text & "','" & POLLINGSEC & "')")
            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('M," & Label1.Text & "," & TextBox2.Text & "','" & SOGLIASCRITT & "')")
            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('M," & Label1.Text & "," & TextBox2.Text & "','" & FLAGTRACK & "')")
            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('M," & Label1.Text & "," & TextBox2.Text & "','" & MINAGGSTAT & "')")
            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('M," & Label1.Text & "," & TextBox2.Text & "','" & PORTSTAT & "')")
            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('M," & Label1.Text & "," & TextBox2.Text & "','" & inconvS & "')")

            Response.Redirect(Request.RawUrl)
        End If

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim connection As New OleDb.OleDbConnection("Provider=IBMDA400;" &
                              "Data Source=" & ipsql & ";" &
                              "Force Translate=0;" &
                              "Default Collection=" & dbsql & ";" &
                              "User ID=" & usrsql & ";" &
                              "Password=" & pswsql)

        Dim tbid As New DataTable()
        corpo.Visible = True
        Label2.Text = "N"

        Dim r As Random = New Random

        Dim i As Integer = 0

        Do While i <= 1
            Dim newid As Integer = r.Next(maxValue:=999, minValue:=111)
            connection.Open()
            Dim adapter0 As New OleDbDataAdapter("SELECT key from CNFMACCH where key like 'M," & newid & "'", connection)
            adapter0.Fill(tbid)
            connection.Close()

            If tbid.Rows.Count = 0 Then
                Label1.Text = newid
                i += 1
            Else
                i = 0
            End If
        Loop

    End Sub

    Public Sub sql_execution(sqlcmd As String)


        Dim connection As New OleDb.OleDbConnection("Provider=IBMDA400;" &
                               "Data Source=" & ipsql & ";" &
                               "Force Translate=0;" &
                               "Default Collection=" & dbsql & ";" &
                               "User ID=" & usrsql & ";" &
                               "Password=" & pswsql)
        Dim cmd As New OleDbCommand
        cmd.Connection = connection
        connection.Open()
        cmd.CommandText = sqlcmd

        cmd.ExecuteNonQuery()
        connection.Close()
    End Sub

    Function mailset()
        Dim connection As New OleDb.OleDbConnection("Provider=IBMDA400;" &
                     "Data Source=" & ipsql & ";" &
                     "Force Translate=0;" &
                     "Default Collection=" & dbsql & ";" &
                     "User ID=" & usrsql & ";" &
                     "Password=" & pswsql)

        Dim tbmail As New DataTable()

        connection.Open()
        Dim adapter0 As New OleDbDataAdapter("SELECT DATI from CNFMACCH where key = 'MAIL'", connection)
        adapter0.Fill(tbmail)
        connection.Close()

        Return tbmail.Rows.Count

    End Function

    Private Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        If DropDownList2.Text = "Attivo" Then
            If mailset() <> 0 Then
                DropDownList18.Visible = True
            Else
                DropDownList2.Text = "Disattivo"
                Response.Write("<script>alert('SMTP non ancora configurato');</script>")
            End If

        Else
            DropDownList18.Visible = False
            DropDownList18.Text = "0"
        End If

    End Sub
    Private Sub DropDownList4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList4.SelectedIndexChanged
        If DropDownList4.Text = "Attivo" Then
            If mailset() <> 0 Then
                DropDownList19.Visible = True
            Else
                DropDownList4.Text = "Disattivo"
                Response.Write("<script>alert('SMTP non ancora configurato');</script>")
            End If
        Else
            DropDownList19.Visible = False
            DropDownList19.Text = "0"
        End If

    End Sub
    Private Sub DropDownList6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList6.SelectedIndexChanged
        If DropDownList6.Text = "Attivo" Then
            If mailset() <> 0 Then
                DropDownList20.Visible = True
            Else
                DropDownList6.Text = "Disattivo"
                Response.Write("<script>alert('SMTP non ancora configurato');</script>")
            End If
        Else
            DropDownList20.Visible = False
            DropDownList20.Text = "0"
        End If

    End Sub
    Private Sub DropDownList8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList8.SelectedIndexChanged
        If DropDownList8.Text = "Attivo" Then
            If mailset() <> 0 Then
                DropDownList21.Visible = True
            Else
                DropDownList8.Text = "Disattivo"
                Response.Write("<script>alert('SMTP non ancora configurato');</script>")
            End If
        Else
            DropDownList21.Visible = False
            DropDownList21.Text = "0"
        End If

    End Sub
    Private Sub DropDownList10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList10.SelectedIndexChanged
        If DropDownList10.Text = "Attivo" Then
            If mailset() <> 0 Then
                DropDownList22.Visible = True
            Else
                DropDownList10.Text = "Disattivo"
                Response.Write("<script>alert('SMTP non ancora configurato');</script>")
            End If
        Else
            DropDownList22.Visible = False
            DropDownList22.Text = "0"
        End If

    End Sub

    Private Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.Text = "Disattivo" Then
            TextBox3.Visible = False
            DropDownList13.Visible = False
            DropDownList2.Visible = False
            DropDownList18.Visible = False

            TextBox3.Text = ""
            DropDownList13.Text = "Black"
            DropDownList2.Text = "Disattivo"
            DropDownList18.Text = "0"
        Else
            TextBox3.Visible = True
            DropDownList13.Visible = True
            DropDownList2.Visible = True

            If DropDownList2.Text = "Attivo" Then
                DropDownList18.Visible = True
            Else
                DropDownList18.Visible = False
                DropDownList18.Text = "0"
            End If

        End If
    End Sub

    Private Sub DropDownList3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList3.SelectedIndexChanged
        If DropDownList3.Text = "Disattivo" Then
            TextBox5.Visible = False
            DropDownList14.Visible = False
            DropDownList4.Visible = False
            DropDownList19.Visible = False

            TextBox5.Text = ""
            DropDownList14.Text = "Black"
            DropDownList4.Text = "Disattivo"
            DropDownList19.Text = "0"
        Else
            TextBox5.Visible = True
            DropDownList14.Visible = True
            DropDownList4.Visible = True

            If DropDownList4.Text = "Attivo" Then
                DropDownList19.Visible = True
            Else
                DropDownList19.Visible = False
                DropDownList19.Text = "0"
            End If
        End If
    End Sub

    Private Sub DropDownList5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList5.SelectedIndexChanged
        If DropDownList5.Text = "Disattivo" Then
            TextBox7.Visible = False
            DropDownList15.Visible = False
            DropDownList6.Visible = False
            DropDownList20.Visible = False

            TextBox7.Text = ""
            DropDownList15.Text = "Black"
            DropDownList6.Text = "Disattivo"
            DropDownList20.Text = "0"
        Else
            TextBox7.Visible = True
            DropDownList15.Visible = True
            DropDownList6.Visible = True

            If DropDownList6.Text = "Attivo" Then
                DropDownList20.Visible = True
            Else
                DropDownList20.Visible = False
                DropDownList20.Text = "0"
            End If
        End If
    End Sub

    Private Sub DropDownList7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList7.SelectedIndexChanged
        If DropDownList7.Text = "Disattivo" Then
            TextBox9.Visible = False
            DropDownList16.Visible = False
            DropDownList8.Visible = False
            DropDownList21.Visible = False

            TextBox9.Text = ""
            DropDownList16.Text = "Black"
            DropDownList8.Text = "Disattivo"
            DropDownList21.Text = "0"
        Else
            TextBox9.Visible = True
            DropDownList16.Visible = True
            DropDownList8.Visible = True

            If DropDownList8.Text = "Attivo" Then
                DropDownList21.Visible = True
            Else
                DropDownList21.Visible = False
                DropDownList21.Text = "0"
            End If
        End If
    End Sub

    Private Sub DropDownList9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList9.SelectedIndexChanged
        If DropDownList9.Text = "Disattivo" Then
            TextBox11.Visible = False
            DropDownList17.Visible = False
            DropDownList10.Visible = False
            DropDownList22.Visible = False

            TextBox11.Text = ""
            DropDownList17.Text = "Black"
            DropDownList10.Text = "Disattivo"
            DropDownList22.Text = "0"
        Else
            TextBox11.Visible = True
            DropDownList17.Visible = True
            DropDownList10.Visible = True

            If DropDownList10.Text = "Attivo" Then
                DropDownList22.Visible = True
            Else
                DropDownList22.Visible = False
                DropDownList22.Text = "0"
            End If
        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        sql_execution("DELETE FROM CNFMACCH WHERE KEY LIKE 'M," & drpdwnlstMC.Text.ToString.Substring(0, 3) & "%'")
        Response.Redirect(Request.RawUrl)

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Response.Redirect(Request.RawUrl)
    End Sub

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        Dim connection As New OleDb.OleDbConnection("Provider=IBMDA400;" &
                             "Data Source=" & ipsql & ";" &
                             "Force Translate=0;" &
                             "Default Collection=" & dbsql & ";" &
                             "User ID=" & usrsql & ";" &
                             "Password=" & pswsql)

        Dim tbmail As New DataTable()

        connection.Open()
        Dim adapter0 As New OleDbDataAdapter("SELECT DATI from CNFMACCH where key = 'MAIL'", connection)
        adapter0.Fill(tbmail)
        connection.Close()

        For Each row1 In tbmail.Rows
            Dim words As String() = row1(0).Split(New Char() {","})

            TextBox1.Text = words(0)
            TextBox4.Text = words(1)
            TextBox6.Text = words(2)
            TextBox8.Text = words(3)
            TextBox12.Text = words(4)

        Next

        divmail.Visible = True

    End Sub

    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        If TextBox1.Text <> "" And TextBox4.Text <> "" And TextBox6.Text <> "" And TextBox8.Text <> "" And TextBox12.Text <> "" Then

            sql_execution("DELETE FROM CNFMACCH WHERE KEY = 'MAIL'")

            Dim strmail As String = TextBox1.Text & "," & TextBox4.Text & "," & TextBox6.Text & "," & TextBox8.Text & "," & TextBox12.Text
            sql_execution("INSERT INTO CNFMACCH (KEY, DATI) " & "VALUES('MAIL','" & strmail & "')")

            Response.Redirect(Request.RawUrl)

        Else

            Response.Write("<script>alert('Campi mancanti');</script>")

        End If
    End Sub

    Protected Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Response.Redirect(Request.RawUrl)

    End Sub

    Protected Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        sql_execution("DELETE FROM CNFMACCH WHERE KEY= 'MAIL'")
        Response.Redirect(Request.RawUrl)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim VARTRASP_ARRAY As String() = drpdwnlstMC.Text.Split(New Char() {","})
        Context.Response.Write("<script language='javascript'>window.open('log.aspx?P=" & VARTRASP_ARRAY(0) & "','_newtab');</script>")

    End Sub

End Class