$(document).ready(function () {
    //nakon svakog osvježavanja stranice odma provjeri, a inače provjeravaj svakih 5 sekundi dali ima online usera
    if ($('#xx').text().length != 14) {
        heartbeat();
    }
    else {
        setHeartbeat();
    }
    //setHeartbeat();

});

function setHeartbeat() {
    // ako korisnik nije ulogiran, dužina teksta je 14 *provjerit autorizaciju na drugačji način*
    if ($('#xx').text().length != 14) {
        setTimeout("heartbeat()", 5000); //5 seconds
    }
    else {
        setTimeout("setHeartbeat()", 5000);
    }
}
function heartbeat() {


    var currentuseridx = $('#xx').text();
    var usernamex = $('#yy').text();
    $.ajax({
        url: 'https://localhost:44391/Home/SetHeartbeat',
        method: 'POST',
        data: {
            currentuserid: currentuseridx,
            username: usernamex
        },
        success: function () {
            setHeartbeat(); //provjeri opet za 5 sekundi
            whoIsAlive();   //i odma pokaži tko je živ
        },

        error: function () {
            alert('error')
        }
    });

}



function whoIsAlive() {
    $.ajax({
        url: 'https://localhost:44391/Home/GetAllOnlineUsers',
        method: 'GET',
        contentType: 'application/json;charset=utf-8',
        dataType: 'JSON',
        success: function (data) {
            var obj = JSON.parse(data);

            //ovo miče sve online korisnike
            const elements = document.getElementsByClassName("personOnline");
            while (elements.length > 0) elements[0].remove();

            //dodajemo sve iz početka
            for (i = 0; i < obj.length; i++) {
                var newDiv = document.createElement('div');
                newDiv.innerHTML = '<i class="fas fa-circle" style="color:lightgreen"></i>' + obj[i].UserName;
                newDiv.className = 'personOnline';
                newDiv.setAttribute('data-id', obj[i].Id);
                newDiv.setAttribute('data-username', obj[i].UserName);
                var allOnlineUsers = document.getElementById('onlineUsers');
                allOnlineUsers.appendChild(newDiv);
            }

            //kada kliknem na nekog, otvori meni prozor
            //ovo refaktorirat, izvuć van da radi za dinamično dodane elemente
            jQuery('.personOnline').click(function () {

                OpenChatWindowForEachUser($(this).attr('data-id'), $('#xx').text(), $(this).attr('data-username'))
            });
        },

        error: function () {
            alert('error')
        }
    });
}

function OpenChatWindowForEachUser(id1, id2, hisname) {

    //provjeri da se ne otvori meni prozor ako ja nemam veze sa tim
    if (id1 != $('#xx').text() && id2 != $('#xx').text()) {
        return;
    }
    //JS nebi trebao provjeravati kome treba slati nego SignalR


    //provjerava dali već postoji otvoren prozor
    var chatWindows = document.getElementsByClassName('chatWindow');

    for (i = 0; i < chatWindows.length; i++) {
        //refaktorirat if
        if (id1 == $(chatWindows[i]).attr('data-0') || id1 == $(chatWindows[i]).attr('data-1')) {
            if (id2 == $(chatWindows[i]).attr('data-0') || id2 == $(chatWindows[i]).attr('data-1')) {
                //ako postoji par, onda nemoj stvarat novi prozor
                return;
            }
        }
    }

    //stvori prozor
    var newChatWindow = document.createElement('div');
    newChatWindow.className = 'chatWindow';
    newChatWindow.style.display = 'grid';
    newChatWindow.setAttribute('data-' + [0], id1);
    newChatWindow.setAttribute('data-' + [1], id2);

    var newChatWindowHeader = document.createElement('div');
    newChatWindowHeader.className = 'chatWindowHeader';

    var newChatWindowHeaderProfilePic = document.createElement('div');
    newChatWindowHeaderProfilePic.className = 'chatWindowHeaderProfilePic';
    newChatWindowHeader.appendChild(newChatWindowHeaderProfilePic);

    var newChatWindowHeaderUserName = document.createElement('div');
    newChatWindowHeaderUserName.className = 'chatWindowHeaderUserName';
    newChatWindowHeaderUserName.innerHTML = hisname; 
    newChatWindowHeader.appendChild(newChatWindowHeaderUserName);

    var newChatWindowHeaderVideoChat = document.createElement('div');
    newChatWindowHeaderVideoChat.className = 'chatWindowHeaderVideoChat';
    newChatWindowHeader.appendChild(newChatWindowHeaderVideoChat);

    var newChatWindowHeaderVoiceCall = document.createElement('div');
    newChatWindowHeaderVoiceCall.className = 'chatWindowHeaderVoiceCall';
    newChatWindowHeader.appendChild(newChatWindowHeaderVoiceCall);

    var newChatWindowHeaderOptions = document.createElement('div');
    newChatWindowHeaderOptions.className = 'chatWindowHeaderOptions';
    newChatWindowHeader.appendChild(newChatWindowHeaderOptions);

    var closeChat = document.createElement('div');
    closeChat.className = 'chatWindowHeaderCloseChat';
    closeChat.id = 'closeChat';
    closeChat.innerHTML = '<i class="fas fa-window-close fa-lg"></i>';
    newChatWindowHeader.appendChild(closeChat);

    newChatWindow.appendChild(newChatWindowHeader);

    var newChatWindowMessages = document.createElement('div');
    newChatWindowMessages.className = 'chatWindowMessages';
    newChatWindow.appendChild(newChatWindowMessages);

    var newChatWindowMyMessage = document.createElement('div');
    newChatWindowMyMessage.className = 'chatWindowMyMessage';
    var textArea = document.createElement('textarea');
    textArea.className = 'form-control messageToSend';
    textArea.rows = '1';
    textArea.style.width = '100%';
    textArea.style.resize = 'none';
    textArea.placeholder = 'Type a message..';
    textArea.id = 'messageToSend';

    newChatWindowMyMessage.appendChild(textArea);
    newChatWindow.appendChild(newChatWindowMyMessage);

    var chatWindowContainer = document.getElementById('chatWindowContainer');
    chatWindowContainer.appendChild(newChatWindow);

}
