$(document).ready(() => {
    let connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7046/chatHub")
        .withAutomaticReconnect()
        .build();
    connection.start();

    $("#btn_nick").click(() => {
        let nickname = $("#username").val();
        connection.invoke("GetNickname", nickname).catch(error => console.log(error));
    })

    connection.on("ClientJoined", nickname => {
        $("#join_info").html(`${nickname} joined to chat`);
        $("#join_info").removeClass("d-none");
        // $("#join_info").show().delay(2000).hide();
        $("#join_info").show()

        setTimeout(function () {
            $("#join_info").hide();
        }, 3000);
    });


    connection.on("clients", clients => {

        let data = `<li class="list-group-item" aria-current="true" style="background-color: blue;color: white;">List of Users</li>
        <li class="list-group-item user_item" aria-current="true">Hami</li>`;
        clients.forEach(client => {
            data += `<li class="list-group-item user_item">${client.nickname}</li>`;
        });
        $(".usersList").html(data);
        $(".user_item").click((e) => {
            e.target.classList.toggle("active");
        })
        grups()
    });

    connection.on("groups", groups => {
        let data = '<option selected disabled>Select group to join</option>';
        $.each(groups, (index, group) => {
            data += `<option value="${group.groupname}">${group.groupname}</option>`
        })
        $("#groupList").html(data);
    });

    $("#sendMessage").click(() => {
        let userMessage = $("#msjArea").val();
        const clientName = $(".user_item.active").first().html();
        connection.invoke("SendMessage", userMessage, clientName).catch((error) => console.log(error));
        let data = $('.messagesList').html();
        data +=
            `<li class="list-group-item my-1 d-flex flex-column" aria-current="true">
            <div>
            <h5 id="you" style="float: right;">You</h5>
           
            </div>
            <div>
            <p id="answer" style="background-color: azure;">${userMessage}</p>
            </div>
         </li>`;

        $('.messagesList').html(data);
    });

    connection.on("getMessage", (message, clientName) => {
        let data = $('.messagesList').html();
        data +=
            `<li class="list-group-item my-1 d-flex flex-column" aria-current="true">
            <div>
              <h5 id="sender" style="float: left;">${clientName}</h5>
           
            </div>
            <div>
            <p id="answer" style="background-color: azure;">${message}</p>
            </div>
         </li>`;

        $('.messagesList').html(data);
    });

    $('#btn_group').click(() => {
        let groupname = $('#groupname').val();
        connection.invoke("AddGroup", groupname).catch((error) => console.log(error));
    })

    connection.on("group", groupname => {
        $("#group_info").html(`${groupname} created`);
        $("#group_info").removeClass("d-none");
        $("#group_info").show()

        setTimeout(function () {
            $("#group_info").hide();
        }, 3000);
    });

    connection.on("groups", groups => {
        let data = '<option selected disabled>Select group to join</option>';
        $.each(groups, (index, group) => {
            data += `<option value="${group.groupname}">${group.groupname}</option>`
        })
        $("#groupList").html(data);
    });

    $("#btn_select").click(() => {
        let groupNames = $('.selectGroup').val();
        connection.invoke("AddClientToGroup", groupNames);
    });

    $("#btn_showUsers").click(() => {
        let groupNames = $('.selectGroup').val();
        connection.invoke("GetGroupClients", groupNames[0]);
        connection.on("getUsersInGroup", clients => {
            $(".infoUsers").removeClass("d-none");
            let data = `<p class="thisUser text-center">Users in this group:</p> 
        <ul>`;
            $.each(clients, (index, client) => {
                data += `<li>${client.nickname}</li>`
            })
            data += `</ul>`;
            $(".infoUsers").html(data);
        })
    });


    $("#btn_showAllUsers").click(() => {
        let groupNames = $('.selectGroup').val();
        let clientList = [];
        let count = 0;
        $.each(groupNames, (index, groupName) => {
            connection.invoke("GetGroupClients", groupName);
        });
        connection.on("getUsersInGroup", clients => {
            $.each(clients, (index, client) => {
                clientList.push(client);
            });
            $(".infoUsers").removeClass("d-none");
            let data = `<p class="thisUser text-center">All Users in Groups:</p> 
            <ul>`;
            $.each(clientList, (index, client) => {
                data += `<li>${client.nickname}</li>`
            })
            data += `</ul>`;
            $(".infoUsers").html(data);
        });
    });
});