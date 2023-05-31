<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="gantt.aspx.vb" Inherits="Pico_Web.gantt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
        <style type="text/css">
        .divgantt {
  position: relative;
  margin: 3px;
  height: 80vh;
  width: 97vw;

}
        </style>

    <form id="form1" runat="server">
        <div>

           <!-- Importare la libreria jQuery -->
<script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
<!-- Importare la libreria DHTMLX Gantt tramite CDN -->
<link href="https://cdn.dhtmlx.com/gantt/edge/dhtmlxgantt.css" rel="stylesheet" type="text/css" />
<script src="https://cdn.dhtmlx.com/gantt/edge/dhtmlxgantt.js"></script>
<div class="divgantt" id="ganttChart"></div>
<script type="text/javascript">
    // Recuperare i nomi dei macchinari dal database
    var machineNames = [];

    $.ajax({
        type: "POST",
        url: "gantt.aspx/GetMachineNames",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            // Convertire la stringa JSON in un array di oggetti
            machineNames = JSON.parse(response.d);
            // Verifica che i nomi delle macchine siano inclusi nell'array
            if (machineNames && machineNames.length > 0) {
                // Inizializzare il grafico Gantt
                gantt.init("ganttChart", new Date(), new Date(2022, 11, 31));
                // Configurare il nome della sezione "machine" nella finestra di dialogo del task
                gantt.locale.labels.section_machine = "brim";
                // Configurare la sezione "machine" nella finestra di dialogo del task
                gantt.config.lightbox.sections = [
                    { name: "description", height: 38, map_to: "text", type: "textarea", focus: true },
                    { name: "machine", height: 30, map_to: "machineId", type: "select", options: machineNames }
                ];
                // Recuperare i dati dal database
                $.ajax({
                    type: "POST",
                    url: "gantt.aspx/GetData",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var data = JSON.parse(response.d);
                        // Impostare i dati sul grafico Gantt
                        if (data && data.tasks && data.tasks.length > 0) {
                            gantt.parse(data);
                        } else {
                            console.log("Nessun dato disponibile");
                        }
                    }
                });
            } else {
                console.log("Nessun nome di macchina disponibile");
            }
        }
    });

    // Configurare la funzionalità di drag and drop
    gantt.attachEvent("onAfterTaskUpdate", function (id, task) {
        // Inviare i dati aggiornati al server
        $.ajax({
            type: "POST",
            url: "gantt.aspx/UpdateData",
            data: "{taskId:'" + id + "',newStart:'" + task.start_date + "',newEnd:'" + task.duration + "',newMachine:'" + task.machineId + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                console.log("Task aggiornato con successo");
            }
        });
    });
</script>

        </div>
    </form>
</body>
</html>
