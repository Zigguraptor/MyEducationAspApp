"use strict";

var messageHistoryContainer = document.getElementById("messagesList");
messageHistoryContainer.scrollTop = messageHistoryContainer.scrollHeight;

var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/chat").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var p = document.createElement("p");
    p.textContent = user + ": " + message;
    var messageList = document.getElementById("messagesList");

    if (messageList.scrollTop + messageList.clientHeight === messageList.scrollHeight) {
        messageList.appendChild(p);
        messageList.scrollTop = messageList.scrollHeight;
    } else {
        messageList.appendChild(p);
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    if (user.length < 3) {
        alert("Имя должно быть больше 3 символов!");
        return;
    }
    if (message.length < 3) {
        alert("Сообщение должно быть больше 3 символов!");
        return;
    }
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
