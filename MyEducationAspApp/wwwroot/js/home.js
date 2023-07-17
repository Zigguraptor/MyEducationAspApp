const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/status")
    .build();

connection.on("ReceiveStatus", function (freeRam) {
    if (!freeRam || freeRam.trim() === "") {
        freeRam = "Статус недоступен";
    }
    document.getElementById("status-info").innerText = freeRam;
});

connection.start().catch(function (err) {
    console.error(err.toString());
    document.getElementById("status-info").innerText = "Статус недоступен";
    document.getElementById("status-info").classList.add("text-danger");
});
