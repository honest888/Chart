<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="chart.aspx.vb" Inherits="Pico_Web.chart"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Statistiche Macchina</title>
          <link rel="icon" type="image/x-icon" href="/img/logo.ico" />


<%--    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src=" https://github.com/chartjs/Chart.js/blob/master/src/plugins/plugin.filler/index.js"></script>--%>

<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
<script src="https://cdn.jsdelivr.net/npm/hammerjs@2.0.8"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/chartjs-plugin-zoom/1.2.1/chartjs-plugin-zoom.min.js"></script>

     <script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
        <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />


    <style type="text/css">
        .text-center {
  position: relative;
  margin: 3px;
  height: 80vh;
  width: 97vw;

}
        .ui-dialog-titlebar-close {
   /* visibility: hidden;*/
}
.button {
  background-color: #00b8d4;
  border-radius: 28px;
  border: 1px solid #017e91;
  display: inline-block;
  cursor: pointer;
  color: #ffffff;
  font-family: Georgia;
  font-size: 15px;
  padding: 8px 15px;
  text-decoration: none;
  text-shadow: 0px 1px 0px #0039a6;
}

.button:hover {
  background-color: #017e91;
}

.button:active {
  position: relative;
  top: 1px;
  background-color: #0052cc;
}

.button2 {
  background-color: #ff5a1d;
  border-radius: 28px;
  border: 1px solid #ff4703;
  display: inline-block;
  cursor: pointer;
  color: #ffffff;
  font-family: Georgia;
  font-size: 15px;
  padding: 8px 15px;
  text-decoration: none;
  text-shadow: 0px 1px 0px #0039a6;
}

.button2:hover {
  background-color: #ff4703;
}

.button2:active {
  position: relative;
  top: 1px;
  background-color: #ff6c37;
}

body {
  font-family: Georgia, serif !important;
    font-size: 16px !important;
}

.display-2{
  font-family: Georgia, serif !important;
      font-size: 22px !important;

}
#loading {
  position: fixed;
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 100%;
  top: 0;
  left: 0;
  opacity: 0.7;
  background-color: #fff;
  z-index: 99;
}

#loading-image {
  z-index: 100;
}

    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:Timer ID="Timer1" runat="server"></asp:Timer>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
</asp:ScriptManager>
        <div>            

                    <h2 class="intestazione">Statistiche Macchina</h2>
                        <asp:Table ID="Table1" runat="server">
                            <asp:TableRow><asp:TableCell><asp:DropDownList runat="server" ID="drpdwnlstMC" Height="20px" Width="230px" AutoPostBack="true"></asp:DropDownList></asp:TableCell><asp:TableCell>&nbsp;&nbsp;Data DAL: <asp:UpdatePanel runat="Server" ID="UpdatePanel1" RenderMode="Inline">
       <ContentTemplate>
 <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label> </ContentTemplate>
</asp:UpdatePanel></asp:TableCell><asp:TableCell>
                               <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="img/calendar.png" style="width:auto; height:40px;" ToolTip="Calendario"/>&nbsp;&nbsp;</asp:TableCell><asp:TableCell>Ora DAL: </asp:TableCell><asp:TableCell>
                                   <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>&nbsp;&nbsp;</asp:TableCell><asp:TableCell>-</asp:TableCell>
                                <asp:TableCell>&nbsp;&nbsp;Data AL: <asp:UpdatePanel runat="Server" ID="UpdatePanel3" RenderMode="Inline">
       <ContentTemplate>
 <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label> </ContentTemplate>
</asp:UpdatePanel></asp:TableCell><asp:TableCell>
                               <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="img/calendar.png" style="width:auto; height:40px;" ToolTip="Calendario"/></asp:TableCell><asp:TableCell>&nbsp;&nbsp;</asp:TableCell><asp:TableCell>Ora AL: </asp:TableCell><asp:TableCell>
                                   <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList></asp:TableCell>
                                <asp:TableCell>&nbsp;&nbsp;-&nbsp;&nbsp;
                                  Entità:</asp:TableCell><asp:TableCell><asp:UpdatePanel runat="Server" ID="UpdatePanel2" RenderMode="Inline">
       <ContentTemplate>
 <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox> </ContentTemplate>
</asp:UpdatePanel> &nbsp;&nbsp;</asp:TableCell><asp:TableCell><asp:Button ID="Button1" runat="server" Text="Carica" CssClass="button"/></asp:TableCell><asp:TableCell>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button6" runat="server" Text="Oggi con Refresh automatico" CssClass="button2"/></asp:TableCell></asp:TableRow>
</asp:Table>
<script type = "text/javascript">
    $(document).ready(function () {

        $("[id$=TextBox1]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/chart.aspx/GetEnt") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('-')[0],
                                val: item.split('-')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("[id$=hfaardis]").val(i.item.val);
            },
            minLength: 1
        });

    }); 
        </script>

<div class="text-center">
<h1 class="display-2" style="margin:5px;">
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label></h1>
<canvas id="IdGrafico"></canvas>
</div>
           <script>
         
               function PopUpModel() {
                   $("#childdialog").dialog("open");
                   $("#childdialog2").dialog("close");

               }
               function PopUpModel_close() {
                   $("#childdialog").dialog("close");
                   $("#childdialog2").dialog("close");

               }

               $(function () {
                   $("#childdialog2").dialog({
                       autoOpen: false,
                       height: 350,
                       width: 340,
                       modal: false,
                       title: "Calendario DAL",
                       open: function (type, data) { $(this).parent().appendTo("form"); }
                   });

               });

               $(function () {
                   $("#childdialog").dialog({
                       autoOpen: false,
                       height: 350,
                       width: 340,
                       modal: false,
                       title: "Calendario AL",
                       open: function (type, data) { $(this).parent().appendTo("form"); }
                   });

               });
               function PopUpModel2() {
                   $("#childdialog2").dialog("open");
                   $("#childdialog").dialog("close");
               }
               function PopUpModel2_close() {
                   $("#childdialog2").dialog("close");
                   $("#childdialog").dialog("close");

               }


           </script>

            <script type="text/javascript"> 
              

                function code1() {
                    var javaVariable1 = <%= _Result1 %>
                    var javaVariable2 = <%= _Result2 %>
                    var javaVariable3 = <%= _Result3 %>
                    var javaVariable4 = <%= _Result4 %>
                    var javaVariable5 = <%= _Result5 %>
                    var javaVariable6 = <%= _Result6 %>


                    document.getElementById("IdGrafico").width = window.innerWidth*0.98;
                    document.getElementById("IdGrafico").height = window.innerHeight*0.8;

                    const ctx = document.getElementById('IdGrafico');
                   
                    new Chart(ctx, {
                        data: {
                            datasets: [{
                                type: 'line',
                                label: 'Velocità',
                                data: javaVariable2,
                                radius: 0,
                                borderWidth: 1,
                                stepped: true,
                                yAxisID: 'y',
                                xAxisID: 'x1', // specify which x-axis to use
                                borderColor: 'rgba(39, 166, 245, 0.8)',
                                backgroundColor: 'rgba(39, 166, 245, 0.8)',
                                
                            }, {
                                    type: 'line',
                                    label: 'Fermo Macchina',
                                    data: javaVariable3,
                                    radius: 0,
                                    borderWidth: 1,
                                    stepped: true,
                                    yAxisID: 'y1',
                                    xAxisID: 'x1', // specify which x-axis to use
                                    borderColor: 'rgba(243, 62, 62, 0.8)',
                                    backgroundColor: 'rgba(243, 62, 62, 0.8)',
                                fill: true,
                                },
                                {
                                    type: 'line',
                                    label: 'Levata',
                                    data: javaVariable4,
                                    radius: 0,
                                    borderWidth: 1,
                                    stepped: true,
                                    yAxisID: 'y1',
                                    xAxisID: 'x1', // specify which x-axis to use
                                    borderColor: 'rgba(74, 171, 73, 0.8)',
                                    backgroundColor: 'rgba(74, 171, 73, 0.8)',
                                    fill: true,
                                },
                                {
                                    type: 'line',
                                    label: 'Rolla',
                                    data: javaVariable5,
                                    radius: 0,
                                    borderWidth: 1,
                                    stepped: true,
                                    yAxisID: 'y1',
                                    xAxisID: 'x1', // specify which x-axis to use
                                    borderColor: 'rgba(63, 81, 219, 0.8)',
                                    backgroundColor: 'rgba(63, 81, 219, 0.8)',

                                    fill: true,
                                },
                                {
                                    type: 'line',
                                    label: 'Rastrelliera',
                                    data: javaVariable6,
                                    radius: 0,
                                    borderWidth: 1,
                                    stepped: true,
                                    yAxisID: 'y1',
                                    xAxisID: 'x1', // specify which x-axis to use
                                    borderColor: 'rgba(191, 191, 191, 0.8)',
                                    backgroundColor: 'rgba(191, 191, 191, 0.8)',

                                    fill: true,
                                }],
                        },
                        options: {
                            responsive: false,
                            interaction: {
                                mode: 'index',
                                intersect: false,
                            },
                            stacked: false,
                            plugins: {
                                title: {
                                    display: false,
                                    text: 'Chart.js Line Chart - Multi Axis'
                                },
                                zoom: {
                                    zoom: {
                                        wheel: {
                                            enabled: true,
                                        },
                                        pinch: {
                                            enabled: true
                                        },
                                        mode: 'x',
                                    },
                                    pan: {
                                        enabled: true,
                                        mode: 'x',
                                    },
                                }                                
                            },
                            scales: {
                                y: {
                                    type: 'linear',
                                    display: true,
                                    position: 'left',
                                    ticks: {
                                        stepSize: 50,
                                    },
                                },
                                y1: {
                                    type: 'linear',
                                    offset: false,
                                    display: true,
                                    position: 'right',

                                    // set min and max values for y1 axis
                                    min: 0,
                                    max: 1,

                                    ticks: {
                                        callback: function (value) {
                                            if (value === 0 || value === 0.20) {
                                                return value === 0 ? 'OFF' : 'ON';
                                            }
                                        },
                                        zeroLineWidth: 2,
                                        zeroLineColor: '#000',
                                    },
                                    
                                    // grid line settings
                                    grid: {
                                        drawOnChartArea: false, // only want the grid lines for one axis to show up
                                    },
                                },
                               
                                x1: {
                                        labels: javaVariable1,

                                    ticks: {
                                        autoSkip: true,
                                        stepSize: 600,
                                        //callback: function (value1, index, values) {
                                        //    return (index % 600 === 0) ? javaVariable1[index] : null;
                                        //},
                                    }
                                },
                              
                            },
                           
                        }
                    });

                };


            </script>
            <br />

                   </div>
                  <div id="childdialog2" style="display:none">

<asp:UpdatePanel runat="Server" ID="up1" RenderMode="Inline">
       <ContentTemplate>
              <asp:Calendar runat="server" ID="Calendar1" />
       </ContentTemplate>
</asp:UpdatePanel>

              <br />
              <asp:Button ID="Button3" runat="server" Text="Oggi" />&nbsp;&nbsp;&nbsp;<asp:Button ID="Button2" runat="server" Text="Nessuna Data" />

     </div>
                         <div id="childdialog" style="display:none">

<asp:UpdatePanel runat="Server" ID="UpdatePanel4" RenderMode="Inline">
       <ContentTemplate>
              <asp:Calendar runat="server" ID="Calendar2" />
       </ContentTemplate>
</asp:UpdatePanel>

              <br />
              <asp:Button ID="Button4" runat="server" Text="Oggi" />&nbsp;&nbsp;&nbsp;<asp:Button ID="Button5" runat="server" Text="Nessuna Data" />

     </div>
        <script>
            $(window).on('load', function () {
                $('#loading').hide();
            })
            $('#Button1').on('click', function () {
                $('#loading').show();
            })
        </script>
        <div id="loading" runat="server">
  <img id="loading-image" src="img/load.gif" alt="Loading..." />
</div>
      
    </form>
</body>
</html>
