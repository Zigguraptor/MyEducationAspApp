const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/status")
    .build();

connection.on("ReceiveStatus", function (freeRam) {

    document.getElementById("ram-info").innerText = "Свободная RAM: " + freeRam;
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
