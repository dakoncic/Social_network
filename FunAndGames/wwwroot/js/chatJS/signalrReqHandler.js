var connection = new signalR.HubConnectionBuilder()
    .withUrl('/chatHub')
    .build();

connection.on('ReceiveMessage', addMessageToChat);
connection.on('ReceiveOpenChatWindow', OpenChatWindowForEachUser);

connection.start()
    .catch(error => {
        console.error(error.message);
    });

function sendMessageToHub(username, text, chatwindowdata1, chatwindowdata2) {
    connection.invoke('SendMessage', username, text, chatwindowdata1, chatwindowdata2);
}

function openChatWindowToHub(id1, id2, username) {
    connection.invoke('OpenChatWindow', id1, id2, username);
}