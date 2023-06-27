$(document).ready(() => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7046/messageHub")
        .withAutomaticReconnect()
        .build();

    connection.start();
    
    let connectId='';
    connection.on("getIds", connectionId => {
        connectId=connectionId
        $("#ConnectionId").html(`<p>Connection id --- ${connectionId}</p>`);
        $("#ConnectionId").css("background-color","yellow");
        $("#ConnectionId").css("color","black");
    });

    $("#btn_choose").click(() => {
        let groupName=$('input[name="group"]:checked').val();
       connection.invoke("AddToGroup",connectId,groupName).catch(error => console.log(error));
    });

    $("#btn_send").click(() => {
        let message = $("#text_inp").val();
        let connectIds=$("#conId").val().split(",");
       let groupName=$('input[name="group"]:checked').val();
       connection.invoke("SendMessage",connectIds,groupName,message).catch(error => console.log(error));
    //    connection.invoke("SendMessage", message).catch(error => console.log(error));
    });
    
    let recieveMessage = '';
    connection.on("recieveMessage", message => {
        recieveMessage = message;
        $("#messageDiv").html(`<p>${recieveMessage}</p>`);
        $("#messageDiv").css("background-color","blue");
        $("#messageDiv").css("color","white");
    });

})