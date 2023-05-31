<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" Inherits="Pico_Web.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Dashboard Configurazione Macchina</title>
      <link rel="icon" type="image/x-icon" href="/img/logo.ico" />

</head>
<body>
    <form id="form1" runat="server">
        <h2>Gestione Pico Service</h2>
        <div>
            <asp:Table ID="Table1" runat="server">
                <asp:TableRow><asp:TableCell>Macchina:</asp:TableCell><asp:TableCell><asp:DropDownList runat="server" ID="drpdwnlstMC"></asp:DropDownList></asp:TableCell>
                    <asp:TableCell><asp:Button ID="Button2" runat="server" Text="Aggiungi Nuova Macchina" /></asp:TableCell>
                    <asp:TableCell><asp:Button ID="Button1" runat="server" Text="Modifica" /></asp:TableCell>
                    <asp:TableCell><asp:Button ID="Button5" runat="server" Text="Elimina" /></asp:TableCell>
                    <asp:TableCell><asp:Button ID="Button3" runat="server" Text="Vedi Log" /></asp:TableCell>
                    <asp:TableCell><asp:Button ID="Button7" runat="server" Text="Impostazioni SMTP" /></asp:TableCell>
                </asp:TableRow>

            </asp:Table>
             <asp:Label ID="Label2" runat="server" Text="" Visible="false"></asp:Label>
        </div>
        <div id="corpo" runat="server">
            <br />
            <h3>Dati Macchina</h3>
                        <asp:Table ID="Table2" runat="server">
                            <asp:TableRow><asp:TableCell>ID unico macchina: </asp:TableCell><asp:TableCell> <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></asp:TableCell></asp:TableRow><asp:TableRow><asp:TableCell>Alias macchina: </asp:TableCell><asp:TableCell> <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></asp:TableCell></asp:TableRow>

                        </asp:Table>
                                    <asp:Table ID="Table3" runat="server">
                                        <asp:TableHeaderRow><asp:TableHeaderCell>Ingresso</asp:TableHeaderCell><asp:TableHeaderCell>Stato</asp:TableHeaderCell><asp:TableHeaderCell>Alias Ingresso</asp:TableHeaderCell><asp:TableHeaderCell>Colore Semaforo</asp:TableHeaderCell><asp:TableHeaderCell>Log Mail</asp:TableHeaderCell><asp:TableHeaderCell>Tempo in min Alert mail</asp:TableHeaderCell></asp:TableHeaderRow>
                                        <asp:TableRow><asp:TableCell>Ingresso1: </asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"></asp:DropDownList></asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList13" runat="server"></asp:DropDownList></asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true"></asp:DropDownList></asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList18" runat="server"></asp:DropDownList></asp:TableCell></asp:TableRow>
                                        <asp:TableRow><asp:TableCell>Ingresso2: </asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="true"></asp:DropDownList></asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList14" runat="server"></asp:DropDownList></asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="true"></asp:DropDownList></asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList19" runat="server"></asp:DropDownList></asp:TableCell></asp:TableRow>
                                        <asp:TableRow><asp:TableCell>Ingresso3: </asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="true"></asp:DropDownList></asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList15" runat="server"></asp:DropDownList></asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="true"></asp:DropDownList></asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList20" runat="server"></asp:DropDownList></asp:TableCell></asp:TableRow>
                                        <asp:TableRow><asp:TableCell>Ingresso4: </asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="true"></asp:DropDownList></asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList16" runat="server"></asp:DropDownList></asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList8" runat="server" AutoPostBack="true"></asp:DropDownList></asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList21" runat="server"></asp:DropDownList></asp:TableCell></asp:TableRow>
                                        <asp:TableRow><asp:TableCell>Ingresso5: </asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList9" runat="server" AutoPostBack="true"></asp:DropDownList></asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox11" runat="server"></asp:TextBox></asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList17" runat="server"></asp:DropDownList></asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList10" runat="server" AutoPostBack="true"></asp:DropDownList></asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList22" runat="server"></asp:DropDownList></asp:TableCell></asp:TableRow>
                                        <asp:TableRow><asp:TableCell>Ingresso_START: </asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList11" runat="server"></asp:DropDownList></asp:TableCell></asp:TableRow>
                                        <asp:TableRow><asp:TableCell>Uscita_Consenso_START: </asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList12" runat="server"></asp:DropDownList></asp:TableCell></asp:TableRow>
                                        <asp:TableRow><asp:TableCell>Tollezanza % variaz velocità: </asp:TableCell><asp:TableCell><asp:DropDownList ID="DropDownList23" runat="server"></asp:DropDownList></asp:TableCell></asp:TableRow>
                                        <asp:TableRow><asp:TableCell>Produzione m/min a 100rpm (Scrivi I in caso sia montato un inverter): </asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox17" runat="server" Width="50"></asp:TextBox></asp:TableCell></asp:TableRow>
                                        <asp:TableRow><asp:TableCell>Max prod. m/min al massimo degli Hz dell'inverter (Scrivi S in caso sia montato un sensore): </asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox10" runat="server" Width="50"></asp:TextBox></asp:TableCell></asp:TableRow>
                                        <asp:TableRow><asp:TableCell>Tempo in secondi polling lettura velocità: </asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox13" runat="server" Width="50"></asp:TextBox></asp:TableCell></asp:TableRow>
                                        <asp:TableRow><asp:TableCell>Soglia m/min oltre alla quale registra i valori: </asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox14" runat="server" Width="50"></asp:TextBox></asp:TableCell></asp:TableRow>
                                        <asp:TableRow><asp:TableCell>Intervallo in sec aggiornamento statistiche: </asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox16" runat="server" Width="50"></asp:TextBox></asp:TableCell></asp:TableRow>
                                        <asp:TableRow><asp:TableCell>Abilitazione portale statistiche: </asp:TableCell><asp:TableCell><asp:CheckBox ID="CheckBox1" runat="server" /></asp:TableCell></asp:TableRow>
                                        <asp:TableRow><asp:TableCell>Flag da scrivere nel record in caso di postazione Track: </asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox15" runat="server" Width="50"></asp:TextBox></asp:TableCell></asp:TableRow>

                                        </asp:Table>
            <br />

            <asp:Button ID="Button4" runat="server" Text="Salva" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="Button6" runat="server" Text="Annulla" />

        </div>
                <div id="divmail" runat="server">
                        <asp:Table ID="Table4" runat="server">
                            <asp:TableRow><asp:TableCell>IP Server Mail:</asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox></asp:TableCell></asp:TableRow>
                            <asp:TableRow><asp:TableCell>Ind. mail Mittente:</asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox4" runat="server" Width="250px"></asp:TextBox></asp:TableCell></asp:TableRow>
                            <asp:TableRow><asp:TableCell>Psw Mail Mittente:</asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox6" runat="server" Width="250px"></asp:TextBox></asp:TableCell></asp:TableRow>
                            <asp:TableRow><asp:TableCell>Porta SMTP:</asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox8" runat="server" Width="250px"></asp:TextBox></asp:TableCell></asp:TableRow>
                            <asp:TableRow><asp:TableCell>Ind. mail Destinatario:</asp:TableCell><asp:TableCell><asp:TextBox ID="TextBox12" runat="server" Width="250px"></asp:TextBox></asp:TableCell></asp:TableRow>

</asp:Table>
                    <br />
            <asp:Button ID="Button8" runat="server" Text="Salva" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="Button9" runat="server" Text="Annulla" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="Button10" runat="server" Text="Elimina configurazione" />
</div>
    </form>
</body>
</html>
