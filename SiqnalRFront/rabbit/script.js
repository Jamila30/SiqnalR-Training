$(function () {

    let connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7046/rabbitHub")
        .withAutomaticReconnect()
        .build();
    connection.start();

    $("#btn_send").click(() => {
        let email = $("#email").val();
        let message = $("#message").val();
        $.post("https://localhost:7046/api/producer",{
            Email: email,
            Message: message
        }, () => {

        });
    });
    connection.on("recieveMessage", message => {
        $("#messageBox").html(`<p> ${message}</p>`)
    })
});